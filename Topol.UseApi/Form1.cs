using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ComLog.WinForms.Utils;
using Common.Dto;
using Common.Dto.Model;
using Common.Dto.Model.NewApi;
using Common.Dto.Model.Packet;
using log4net;
using log4net.Repository.Hierarchy;
using Newtonsoft.Json;
using PriceCommon.Enums;
using PriceCommon.Utils;
//using Tesseract;
using Topol.UseApi.Data.Common;
using Topol.UseApi.Forms;
using Topol.UseApi.Interfaces;
using Topol.UseApi.Interfaces.Common;
using Topol.UseApi.Ninject;
using Topol.UseApi.Properties;

namespace Topol.UseApi
{
    public partial class Form1 : Form
    {
        private readonly IDataMаnager _dataManager;
        private readonly List<SearchItemHeaderDto> _listSearchItem = new List<SearchItemHeaderDto>();
        private DataTable _datatableSearchItem;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private bool _enableResultButtons;
        private bool EnableResultButtons
        {
            get { return _enableResultButtons; }
            set
            {
                _enableResultButtons = value;
                btnSaveInternetResults.Enabled = value;
                btnInvertSelected.Enabled = value;
                btnDeleteSelected.Enabled = value;
                btnMove.Enabled = value;
            }
        }
        private BindingSource SearchItemsBindingSource { get; }
        private BindingSource ContentItemsBindingSource { get; }
        private BindingSource MaybeItemsBindingSource { get; }
        private BindingSource Okpd2ItemsBindingSource { get; }
        private readonly BackgroundWorker _bw = new BackgroundWorker();
        private readonly BackgroundWorker _bwSellerCount = new BackgroundWorker();
        private string[] _packetLines;

        #region rectangle on image
        private Point _rectStartPoint;
        private Rectangle _rect;
        private Rectangle _prevRect;
        private readonly Brush _selectionBrush = new SolidBrush(Color.FromArgb(128, 72, 145, 220));
        //private readonly TesseractEngine _engine;

        #endregion //rectangle on image

        private class SearchItemStatusItem
        {
            public string Text { get; set; }
            public TaskStatus? TaskStatus { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
            Text = $"{Text} (версия {ProductVersion})";
            cmbElasticIndexName.SelectedIndex = 0;
            cmbNorm.SelectedIndex = 0;
            _dataManager = new DataMаnager();

            MaybeItemsBindingSource = new BindingSource();
            Okpd2ItemsBindingSource = new BindingSource();

            SearchItemsBindingSource = new BindingSource();
            SearchItemsBindingSource.CurrentChanged += PacketItemsOnCurrentChanged;

            ContentItemsBindingSource = new BindingSource();
            bindingNavigator1.BindingSource = ContentItemsBindingSource;

            dgvPacketItems.FilterStringChanged += dgvPacketItems_FilterStringChanged;
            dgvPacketItems.SortStringChanged += dgvPacketItems_SortStringChanged;

            dgvContentItems.FilterStringChanged += dgvContentItems_FilterStringChanged;
            dgvContentItems.SortStringChanged += dgvContentItems_SortStringChanged;

            dgvMaybe.FilterStringChanged += dgvMaybe_FilterStringChanged;
            dgvMaybe.SortStringChanged += dgvMaybe_SortStringChanged;

            dgvOkpd2.FilterStringChanged += dgvOkpd2_FilterStringChanged;
            dgvOkpd2.SortStringChanged += dgvOkpd2_SortStringChanged;

            dgvContentItems.CellContentDoubleClick += dgv_CellContentDoubleClick;
            dgvContentItems.CellContentClick += dgvContentItems_CellContentClick;
            dgvContentItems.DataError += dgvContentItems_DataError;
            ContentItemsBindingSource.CurrentChanged += ContentItemsOnCurrentChanged;


            dgvMaybe.CellContentDoubleClick += dgv_CellContentDoubleClick;

            _bw.WorkerSupportsCancellation = false;
            _bw.WorkerReportsProgress = true;
            _bw.DoWork += bw_DoWork;
            _bw.ProgressChanged += bw_ProgressChanged;

            _bwSellerCount.WorkerSupportsCancellation = false;
            _bwSellerCount.WorkerReportsProgress = true;
            _bwSellerCount.DoWork += bwSellerCount_DoWork;
            _bwSellerCount.ProgressChanged += bwSellerCount_ProgressChanged;
            _bwSellerCount.RunWorkerAsync();


            var baseApi = ConfigurationManager.AppSettings["BaseApi"];
            linkLabel1.Text = $@"Описание API - {baseApi}";

            cbSearchItemStatus.DataSource = new[] {
                new SearchItemStatusItem { Text="Любое", TaskStatus=null},
                new SearchItemStatusItem { Text=Utils.GetDescription(TaskStatus.InProcess), TaskStatus=TaskStatus.InProcess},
                new SearchItemStatusItem { Text=Utils.GetDescription(TaskStatus.Checked), TaskStatus=TaskStatus.Checked},
                new SearchItemStatusItem { Text=Utils.GetDescription(TaskStatus.Ok), TaskStatus=TaskStatus.Ok},
                new SearchItemStatusItem { Text=Utils.GetDescription(TaskStatus.BreakByTimeout), TaskStatus=TaskStatus.BreakByTimeout},
                new SearchItemStatusItem { Text=Utils.GetDescription(TaskStatus.Break), TaskStatus=TaskStatus.Break},
                new SearchItemStatusItem { Text=Utils.GetDescription(TaskStatus.Error), TaskStatus=TaskStatus.Error}
                };
            cbSearchItemStatus.DisplayMember = "Text";
            cbSearchItemStatus.SelectedIndex = 0;

            linkLabelUrl.LinkClicked += linkLabelUrl_LinkClicked;
            linkLabelScreenshot.LinkClicked += linkLabelUrl_LinkClicked;

            btnCallMaybe.Click += btnCallMaybe_Click;
            btnOkpd2.Click += btnOkpd2_Click;
            btnLoad.Click += btnLoad_Click;
            btnSearchPacket.Click += btnSearchPacket_Click;
            btnPacketText.Click += btnPacketText_Click;
            btnRefreshSearchItems.Click += btnRefreshSearchItems_Click;
            btnClearSearchItems.Click += btnClearSearchItems_Click;
            btnSaveInternetResults.Click += btnSaveInternetResults_Click;
            btnInvertSelected.Click += btnInvertSelected_Click;
            btnDeleteSelected.Click += btnDeleteSelected_Click;


            btnSearchItemBreak.Click += btnSearchItemBreak_Click;
            btnSearchItemDelete.Click += btnSearchItemDelete_Click;
            btnSearchItemChecked.Click += btnSearchItemChecked_Click;

            btnSetPriceChecked.Click += btnSetPriceChecked_Click;
            btnSkipPrice.Click += btnSkipPrice_Click;
            btnDeletePrice.Click += btnDeletePrice_Click;
            btnSetPrice.Click += btnSetPrice_Click;

            tabControl1.TabPages.Remove(tabPage3);
            tabControl1.TabPages.Remove(tabPage2);

            ClearContentView();

            panel21.Click += panel21_Click;
            pbWebshot.Click += panel21_Click;
            pbWebshot.MouseDown += pbWebshot_MouseDown;
            pbWebshot.MouseMove += pbWebshot_MouseMove;
            pbWebshot.Paint += pbWebshot_Paint;
            pbWebshot.MouseUp += pbWebshot_MouseUp;

            btnMove.Click += btnMove_Click;
            btnSplit.Click += btnSplit_Click;
            btnExcel.Click += btnExcel_Click;

            //_engine = new TesseractEngine(@"./tessdata", "rus", EngineMode.Default);


            cmbPriority.Items.Clear();
            cmbPriority.Items.AddRange(new[] { "Нормальный", "Высокий", "Максимальный" });
            priorityList = new[] { "Normal", "High", "Max" };
            cmbPriority.SelectedIndex = 0;

            tabPage4.Enter += tabPage4_Enter;

        }

        public string[] priorityList { get; set; }


        private void ClearContentView()
        {
            linkLabelUrl.Text = "";
            linkLabelScreenshot.Text = "";
            label13.Text = "";
            label15.Text = "";
            lblPrice.Text = "";
            pbWebshot.Image = null;
            pbWebshot.InitialImage = null;
            _rect.Width = 0;
            _rect.Height = 0;
        }


        private void dgvContentItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }


        #region BackgroundWorker

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var searchItemHeaderDto = (SearchItemHeaderDto)e.UserState;
            var list = new List<SearchItemHeaderDto> { searchItemHeaderDto };
            AddPacketItemsToList(list);
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var cnt = 1;
            while (cnt > 0)
            {
                var tasks = _listSearchItem.Where(z => z.ProcessedAt == null).ToList();
                cnt = tasks.Count;
                foreach (var item in tasks)
                {
                    try
                    {
                        var searchItemHeaderDto = _dataManager.GetSearchItemStatus(item.Id).Result;
                        if (searchItemHeaderDto == null) continue;
                        ((BackgroundWorker)sender).ReportProgress(0, searchItemHeaderDto);
                        //else
                        //{
                        //    MessageBox.Show(@"Ошибка запроса.");
                        //}
                    }
                    catch (Exception exception)
                    {
                        Log.Error(exception);
                        Debug.WriteLine(exception);
                    }
                }
                Thread.Sleep(3000);
            }

        }

        #endregion //BackgroundWorker

        #region Grid Settings

        private void PacketGridColumnSettings()
        {
            var dgv = dgvPacketItems;

            // установить видимость полей
            var column = dgv.Columns[nameof(SearchItemHeaderDto.Id)];
            //if (column != null) column.Visible = false;
            column = dgv.Columns[nameof(SearchItemHeaderDto.InternetSessionId)];
            if (column != null) column.Visible = false;
            column = dgv.Columns[nameof(SearchItemDto.Content)];
            if (column != null) column.Visible = false;
            column = dgv.Columns[nameof(SearchItemDto.StartProcessed)];
            if (column != null) column.Visible = false;
            column = dgv.Columns[nameof(SearchItemDto.LastUpdate)];
            if (column != null) column.Visible = false;
            column = dgv.Columns[nameof(SearchItemDto.LastUpdateDateTime)];
            if (column != null) column.Visible = false;
            column = dgv.Columns[nameof(SearchItemDto.ProcessedAt)];
            if (column != null) column.Visible = false;
            column = dgv.Columns[nameof(SearchItemDto.Status)];
            if (column != null) column.Visible = false;
            column = dgv.Columns[nameof(SearchItemHeaderDto.JsonText)];
            if (column != null) column.Visible = false;

            column = dgv.Columns[nameof(SearchItemDto.Source)];
            if (column != null)
            {
                column.HeaderText = @"Источник";
            }
            column = dgv.Columns[nameof(SearchItemHeaderDto.Id)];
            if (column != null)
            {
                column.HeaderText = @"Идентификатор";
            }
            column = dgv.Columns[nameof(SearchItemDto.Name)];
            if (column != null)
            {
                column.HeaderText = @"Наименование ТРУ";
            }
            column = dgv.Columns[nameof(SearchItemHeaderDto.ExtId)];
            if (column != null)
            {
                column.HeaderText = @"Метка";
            }
            column = dgv.Columns[nameof(SearchItemHeaderDto.Normalizer)];
            if (column != null)
            {
                column.HeaderText = @"Нормализатор";
            }
            column = dgv.Columns[nameof(SearchItemHeaderDto.AddKeywords)];
            if (column != null)
            {
                column.HeaderText = @"Ключевые слова";
            }
            column = dgv.Columns[nameof(SearchItemHeaderDto.Priority)];
            if (column != null)
            {
                column.HeaderText = @"Приоритет запроса";
            }
            column = dgv.Columns[nameof(SearchItemDto.StartProcessedDateTime)];
            if (column != null)
            {
                column.HeaderText = @"Время старта";
                column.DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss";
            }
            column = dgv.Columns[nameof(SearchItemDto.LastUpdateDateTime)];
            if (column != null)
            {
                column.HeaderText = @"Время обновления";
                column.DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss";
            }
            column = dgv.Columns[nameof(SearchItemDto.ProcessedAtDateTime)];
            if (column != null)
            {
                column.HeaderText = @"Время финиша";
                column.DefaultCellStyle.Format = "dd.MM.yyyy HH:mm:ss";
            }
            column = dgv.Columns[nameof(SearchItemDto.StatusString)];
            if (column != null)
            {
                column.HeaderText = @"Состояние";
            }
            column = dgv.Columns[nameof(SearchItemDto.ContentCount)];
            if (column != null)
            {
                column.HeaderText = @"Найдено цен";
            }
        }

        private void ContentGridPriceVariants(DataGridView dgv)
        {
            const string cmbName = nameof(ContentDto.PriceVariant);
            var column = dgv.Columns[cmbName];
            if (column == null) return;
            var cmbName2 = $"{cmbName}2";
            column = dgv.Columns[cmbName2];
            if (column == null)
            {
                var cmb = new DataGridViewComboBoxColumn
                {
                    HeaderText = @"Select Data",
                    Name = cmbName2,
                    //DataSource = new string[] { "a", "b", "c" },
                    MaxDropDownItems = 7,
                    //DisplayMember = cmbName,
                    DataPropertyName = "PriceVariant"
                    //ValueMember =  cmbName
                };
                dgv.Columns.Add(cmb);
            }
            foreach (DataGridViewRow row in dgv.Rows)
            {
                var value = row.Cells[nameof(ContentDto.PriceVariants)].Value.ToString();
                if (string.IsNullOrEmpty(value)) continue;
                var comboBoxCell = row.Cells[cmbName2] as DataGridViewComboBoxCell;
                if (comboBoxCell == null) continue;
                var data = value.Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);


                //comboBoxCell.Items.AddRange(data);
                //comboBoxCell.Value = row.Cells[nameof(ContentDto.PriceVariant)].Value.ToString();
                comboBoxCell.DataSource = data;
                // comboBoxCell.ValueMember = cmbName;
                // comboBoxCell.DisplayMember = cmbName;

                comboBoxCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
            }
        }

        private void ContentGridColumnSettings(DataGridView dgv)
        {
            //ContentGridPriceVariants(dgv);

            // hide all columns
            foreach (DataGridViewColumn dgvColumn in dgv.Columns) dgvColumn.Visible = false;

            var column = dgv.Columns[nameof(ContentDto.Selected)];
            if (column != null)
            {
                column.Visible = true;
                column.SortMode = DataGridViewColumnSortMode.Automatic;
                column.HeaderText = @"Отмечен";
                column.Width = 60;
                column.DisplayIndex = 1;
            }
            column = dgv.Columns[nameof(ContentDto.PriceTypeString)];
            if (column != null)
            {
                column.Visible = true;
                column.SortMode = DataGridViewColumnSortMode.Automatic;
                column.HeaderText = @"Тип цены";
                column.Width = 60;
                column.ReadOnly = true;
                column.DisplayIndex = 2;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            column = dgv.Columns[nameof(ContentDto.Name)];
            if (column != null)
            {
                column.Visible = true;
                column.Width = 500;
                column.HeaderText = @"Наименование ТРУ";
                column.ReadOnly = true;
                column.DisplayIndex = 3;
            }
            column = dgv.Columns[nameof(ContentDto.Price)];
            if (column != null)
            {
                column.Visible = false;
                //column.HeaderText = @"Цена";
                //column.ReadOnly = true;
                //column.DisplayIndex = 4;
                //column.DefaultCellStyle.Format = "N2";
                //column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            column = dgv.Columns[nameof(ContentDto.Nprice)];
            if (column != null)
            {
                column.Visible = true;
                column.HeaderText = @"Цена";
                column.ReadOnly = true;
                column.DisplayIndex = 4;
                column.DefaultCellStyle.Format = "N2";
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            column = dgv.Columns[$"{nameof(ContentDto.PriceVariant)}2"];
            if (column != null)
            {
                column.Visible = true;
                column.HeaderText = @"Варианты цены";
                //column.ReadOnly = true;
                column.DisplayIndex = 5;
            }
            column = dgv.Columns[nameof(ContentDto.Uri)];
            if (column != null)
            {
                column.Visible = true;
                column.Width = 500;
                column.HeaderText = @"Ссылка на ТРУ";
                column.ReadOnly = true;
                column.DisplayIndex = 6;
            }
            column = dgv.Columns[nameof(ContentDto.Collected)];
            if (column != null)
            {
                column.Visible = true;
                column.HeaderText = @"Время сбора";
                column.DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                column.DisplayIndex = 7;
            }
            column = dgv.Columns[nameof(ContentExtDto.Id)];
            if (column != null)
            {
                column.Visible = true;
                column.HeaderText = @"Идентификатор";
            }
            column = dgv.Columns[nameof(ContentExtDto.PriceStatusString)];
            if (column != null)
            {
                column.Visible = true;
                column.HeaderText = @"Статус цены";
            }
            column = dgv.Columns[nameof(ContentExtDto.Seller)];
            if (column != null)
            {
                column.Visible = true;
                column.HeaderText = @"Продавец";
            }
            column = dgv.Columns[nameof(ContentExtDto.ProdStatusString)];
            if (column != null)
            {
                column.Visible = true;
                column.HeaderText = @"Статус наличия";
            }
        }

        #endregion //Grid Settings

        #region Event handlers

        private void btnExcel_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog { FileName = $"Topol.Api_{DateTime.Now:yyyyMMdd_HHmm}.xlsx" };
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            var sourceDataTable = (DataTable)ContentItemsBindingSource.DataSource;
            var view = new DataView(sourceDataTable, dgvContentItems.FilterString, dgvContentItems.SortString, DataViewRowState.CurrentRows);
            var dataTable = view.ToTable();
            CreateExcelFile.CreateExcelDocument(dataTable, saveFileDialog.FileName);
            if (File.Exists(saveFileDialog.FileName)) Process.Start(saveFileDialog.FileName);
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            SplitResults();
        }

        private async void SplitResults()
        {
            var current = (DataRowView)SearchItemsBindingSource.Current;
            if (current == null) return;
            var id = current.Row[nameof(SearchItemHeaderDto.Id)] as string;
            var name = current.Row[nameof(SearchItemHeaderDto.Name)] as string;
            var extId = current.Row[nameof(SearchItemHeaderDto.ExtId)] as string;

            var dataTable = (DataTable)ContentItemsBindingSource.DataSource;
            //var names = dataTable.AsEnumerable()
            //    .Select(s => s.Field<string>(nameof(ContentExtDto.Name)))
            //        .Aggregate((cur, next) => cur + "\r\n" + next);
            //    //.ToList();
            ////listName.Aggregate((cur, next) => cur + "," + next);
            var frm = new FormSplit
            {
                textBox =
                {
                    Text = dataTable.AsEnumerable()
                        .Select(s => s.Field<string>(nameof(ContentExtDto.Name)))
                        .Aggregate((cur, next) => cur + "\r\n" + next)
                }
            };
            frm.ButtonGoClick(null, null);
            frm.ShowDialog();
        }

        private void panel21_Click(object sender, EventArgs e)
        {
            panel21.Focus();
        }
        #region rectangle on image
        // Start Rectangle
        private void pbWebshot_MouseDown(object sender, MouseEventArgs e)
        {
            // Determine the initial rectangle coordinates...
            _rectStartPoint = e.Location;
            Invalidate();
        }
        // Draw Rectangle
        private void pbWebshot_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            var tempEndPoint = e.Location;
            _rect.Location = new Point(
                Math.Min(_rectStartPoint.X, tempEndPoint.X),
                Math.Min(_rectStartPoint.Y, tempEndPoint.Y));
            _rect.Size = new Size(
                Math.Abs(_rectStartPoint.X - tempEndPoint.X),
                Math.Abs(_rectStartPoint.Y - tempEndPoint.Y));
            pbWebshot.Invalidate();
        }
        // Draw Area
        private void pbWebshot_Paint(object sender, PaintEventArgs e)
        {
            // Draw the rectangle...
            if (pbWebshot.Image == null) return;
            if (_rect.Width > 0 && _rect.Height > 0)
            {
                e.Graphics.FillRectangle(_selectionBrush, _rect);
            }
        }
        private void pbWebshot_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (_rect.Contains(e.Location))
                {
                    Debug.WriteLine("Right click");
                }
            }
            SaveBitmapPart(pbWebshot.Image, _rect, @"c:\Temp\Bitmap\Test.tiff");
        }

        private void SaveBitmapPart(Image image, Rectangle rect, string pathToSave)
        {
            if (rect.Width <= 2 || rect.Height <= 2) return;
            if (rect == _prevRect) return;
            using (var bmp = new Bitmap(rect.Width, rect.Height))
            {
                using (var graphics = Graphics.FromImage(bmp))
                {
                    graphics.DrawImage(image, 0.0f, 0.0f, rect, GraphicsUnit.Pixel);
                }
                _prevRect = rect;

                //Tesseract OCR
                //using (var page = _engine.Process(bmp))
                //{
                //    var ocrText = page.GetText();
                //    cmbPrices.Text = ocrText;
                //    //cmbPrices.Text = GetOnlyPrice(ocrText);
                //}
            }
        }

        /*
        private static string GetOnlyPrice(string text)
        {
            //var regex = new Regex(@"[-+]?[0-9]*\.?[0-9]*");
            var regex = new Regex(@"[-+]?[0-9]*\W*[0-9]*");
            var matches = regex.Matches(text);
            return matches.Count == 0 ? "" : matches[0].Value;
        }
        */

        #endregion //rectangle on image
        private void btnMove_Click(object sender, EventArgs e)
        {
            MoveResults();
        }
        private async void MoveResults()
        {
            var current = (DataRowView)SearchItemsBindingSource.Current;
            if (current == null) return;
            var id = current.Row[nameof(SearchItemHeaderDto.Id)] as string;
            var name = current.Row[nameof(SearchItemHeaderDto.Name)] as string;
            var extId = current.Row[nameof(SearchItemHeaderDto.ExtId)] as string;

            var list = new List<ContentMoveDto>();
            var selectedCount = 0;
            var dgv = dgvContentItems;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!(bool)row.Cells[nameof(ContentExtDto.Selected)].Value) continue;
                //var value = (int)row.Cells[nameof(ContentDto.PriceType)].Value;
                //if (value != (int)PriceType.Check) continue;
                selectedCount++;
                list.Add(new ContentMoveDto
                {
                    Id = (int)row.Cells[nameof(ContentExtDto.Id)].Value,
                    ElasticId = row.Cells[nameof(ContentExtDto.ElasticId)].Value.ToString()
                });
            }
            if (selectedCount <= 0) return;
            var frm = new FormMoveSelected
            {
                tbName = { Text = name },
                tbExtId = { Text = extId }
            };
            if (frm.ShowDialog() != DialogResult.OK) return;
            if (frm.rbNew.Checked)
            {
                name = frm.tbName.Text;
                extId = frm.tbExtId.Text;
            }
            if (frm.rbExist.Checked)
            {
                id = frm.tbId.Text;
                name = "";
                extId = "";
            }
            await _dataManager.MoveResults(list, id, name, extId);
        }

        private void btnSaveInternetResults_Click(object sender, EventArgs e)
        {
            SaveInternetResults(dgvContentItems);
        }

        private async void SaveInternetResults(DataGridView dgv)
        {
            //ContentItemsBindingSource.Filter = $"{nameof(ContentDto.Name)} LIKE '%гранит%' AND {nameof(ContentDto.Name)} LIKE '%фрак%'";
            //return;
            var list = new List<BasicContentDto>();
            var selectedCount = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!(bool)row.Cells[nameof(ContentDto.Selected)].Value) continue;
                var value = (int)row.Cells[nameof(ContentDto.PriceType)].Value;
                if (value != (int)PriceType.Check) continue;
                selectedCount++;
                list.Add(new BasicContentDto
                {
                    Uri = row.Cells[nameof(ContentDto.Uri)].Value.ToString(),
                    Name = row.Cells[nameof(ContentDto.Name)].Value.ToString(),
                    Price = row.Cells[nameof(ContentDto.Price)].Value.ToString()
                });
            }
            if (selectedCount > 0)
            {
                await _dataManager.Post2InternetIndex(list);
            }
        }


        private void btnInvertSelected_Click(object sender, EventArgs e)
        {
            InvertSelected(dgvContentItems);
        }

        private void InvertSelected(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.Cells[nameof(ContentDto.Selected)].Value = !(bool)row.Cells[nameof(ContentDto.Selected)].Value;
            }
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            DeleteSelected(dgvContentItems);
        }

        private void btnSearchItemBreak_Click(object sender, EventArgs e)
        {
            SearchItemBreak();
        }

        private void btnSearchItemDelete_Click(object sender, EventArgs e)
        {
            SearchItemDelete();
        }

        private void btnSearchItemChecked_Click(object sender, EventArgs e)
        {
            SearchItemChecked();
        }

        private async void SearchItemBreak()
        {
            var current = (DataRowView)SearchItemsBindingSource.Current;
            if (current == null) return;
            const string idColumnName = nameof(SearchItemHeaderDto.Id);
            var id = current.Row[idColumnName] as string;
            var dataTable = (DataTable)SearchItemsBindingSource.DataSource;
            var dataRow = dataTable.Select($"{idColumnName}='{id}'").FirstOrDefault();
            if (dataRow == null || !await _dataManager.PostSearchItemBreak(id)) return;
            dataRow.SetField(nameof(SearchItemHeaderDto.Status), TaskStatus.Break);
            dataRow.SetField(nameof(SearchItemHeaderDto.StatusString), Utils.GetDescription(TaskStatus.Break));
        }
        private async void SearchItemDelete()
        {
            SearchItemBreak();
            var current = (DataRowView)SearchItemsBindingSource.Current;
            var oTaskStatus = current?.Row[nameof(SearchItemHeaderDto.Status)];
            if (oTaskStatus == null) return;
            var taskStatus = (TaskStatus)oTaskStatus;
            if (!(taskStatus == TaskStatus.Ok || taskStatus == TaskStatus.BreakByTimeout || taskStatus == TaskStatus.Break || taskStatus == TaskStatus.Error))
            {
                MessageBox.Show(@"Запрос не может быть удален");
                return;
            }

            var id = current.Row[nameof(SearchItemHeaderDto.Id)] as string;
            if (await _dataManager.DeleteSearchItem(id))
            {
                SearchItemsBindingSource.RemoveCurrent();
            }
            else
            {
                MessageBox.Show(@"Запрос не может быть удален");
            }
        }
        private async void SearchItemChecked()
        {
            var current = (DataRowView)SearchItemsBindingSource.Current;
            if (current == null) return;
            const string idColumnName = nameof(SearchItemHeaderDto.Id);
            var id = current.Row[idColumnName] as string;
            var dataTable = (DataTable)SearchItemsBindingSource.DataSource;
            var dataRow = dataTable.Select($"{idColumnName}='{id}'").FirstOrDefault();
            if (dataRow == null || !await _dataManager.PostSearchItemChecked(id)) return;
            dataRow.SetField(nameof(SearchItemHeaderDto.Status), TaskStatus.Checked);
            dataRow.SetField(nameof(SearchItemHeaderDto.StatusString), Utils.GetDescription(TaskStatus.Checked));
        }

        private void btnSetPriceChecked_Click(object sender, EventArgs e)
        {
            SetPriceChecked();
            ContentItemsBindingSource.MoveNext();
        }

        private async void SetPriceChecked()
        {
            var bindingSource = ContentItemsBindingSource;
            var current = (DataRowView)bindingSource.Current;
            var oPriceStatus = current?.Row[nameof(ContentExtDto.PriceStatus)];
            if (oPriceStatus == null) return;
            var priceStatus = (PriceStatus)oPriceStatus;
            if (priceStatus == PriceStatus.Checked) return;
            current.Row[nameof(ContentExtDto.PriceStatus)] = PriceStatus.Checked;
            current.Row[nameof(ContentExtDto.PriceStatusString)] = Utils.GetDescription(PriceStatus.Checked);
            //call api set price status checked
            var id = ((int)current.Row[nameof(ContentExtDto.Id)]).ToString();
            var elasticId = current.Row[nameof(ContentExtDto.ElasticId)] as string;
            await _dataManager.PostContentItemChecked(id, elasticId);
        }

        private async void SetPrice(string price)
        {
            var bindingSource = ContentItemsBindingSource;
            var current = (DataRowView)bindingSource.Current;
            if (current == null) return;
            const string idColumnName = nameof(ContentExtDto.Id);
            var id = (int)current.Row[idColumnName];
            var elasticId = current.Row[nameof(ContentExtDto.ElasticId)] as string;
            var dataTable = (DataTable)bindingSource.DataSource;
            var dataRow = dataTable.Select($"{idColumnName}='{id}'").FirstOrDefault();
            if (dataRow == null || !await _dataManager.PostContentItemPrice(id.ToString(), elasticId, price)) return;
            dataRow.SetField(nameof(ContentExtDto.Price), price);
            lblPrice.Text = price;
        }

        private static string ExtractPriceFromPrices(string str)
        {
            var sb = new StringBuilder();
            foreach (var c in str)
            {
                if (c == '.' || c == ',' || char.IsDigit(c)) sb.Append(c); else break;
            }
            return sb.ToString().Replace(',', '.');
        }

        private void btnSetPrice_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbPrices.Text)) return;
            var price = ExtractPriceFromPrices(cmbPrices.Text);
            if (string.IsNullOrEmpty(price)) return;
            SetPrice(price);
        }

        private void btnSkipPrice_Click(object sender, EventArgs e)
        {
            ContentItemsBindingSource.MoveNext();
        }

        private void btnDeletePrice_Click(object sender, EventArgs e)
        {
            if (ContentItemsBindingSource.Current != null) ContentItemsBindingSource.RemoveCurrent();
        }

        private void DeleteSelected(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if ((bool)row.Cells[nameof(ContentDto.Selected)].Value)
                {
                    dgv.Rows.Remove(row);
                }
            }
        }

        private void dgvContentItems_SortStringChanged(object sender, EventArgs e)
        {
            ContentItemsBindingSource.Sort = dgvContentItems.SortString;
            ContentGridColumnSettings(dgvContentItems);
        }

        private void dgvContentItems_FilterStringChanged(object sender, EventArgs e)
        {
            ContentItemsBindingSource.Filter = dgvContentItems.FilterString;
            ContentGridColumnSettings(dgvContentItems);
        }

        private void dgvPacketItems_SortStringChanged(object sender, EventArgs e)
        {
            SearchItemsBindingSource.Sort = dgvPacketItems.SortString;
            PacketGridColumnSettings();
        }

        private void dgvPacketItems_FilterStringChanged(object sender, EventArgs e)
        {
            SearchItemsBindingSource.Filter = dgvPacketItems.FilterString;
            PacketGridColumnSettings();
        }

        private void dgvMaybe_SortStringChanged(object sender, EventArgs e)
        {
            MaybeItemsBindingSource.Sort = dgvMaybe.SortString;
            ContentGridColumnSettings(dgvMaybe);
        }

        private void dgvMaybe_FilterStringChanged(object sender, EventArgs e)
        {
            MaybeItemsBindingSource.Filter = dgvMaybe.FilterString;
            ContentGridColumnSettings(dgvMaybe);
        }

        private void dgvOkpd2_SortStringChanged(object sender, EventArgs e)
        {
            Okpd2ItemsBindingSource.Sort = dgvOkpd2.SortString;
        }

        private void dgvOkpd2_FilterStringChanged(object sender, EventArgs e)
        {
            Okpd2ItemsBindingSource.Filter = dgvOkpd2.FilterString;
        }

        private void btnCallMaybe_Click(object sender, EventArgs e)
        {
            var mustRows = tbExact.Text.ToLower()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            //TODO: change ";" to common constant
            var must = string.Join(";", mustRows);
            var shouldRows = tbQuery.Text.ToLower()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var should = string.Join(";", shouldRows);
            var mustNotdRows = tbExclude.Text.ToLower()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var mustNot = string.Join(";", mustNotdRows);
            var source = cmbElasticIndexName.SelectedItem as string;
            SearchMaybe(must, should, mustNot, source);
        }

        private void btnOkpd2_Click(object sender, EventArgs e)
        {
            SearchOkpd2(tbOkpd2.Text.ToLower());
        }

        #endregion //Event handlers

        #region Api calls
        private async void SearchMaybe(string must, string should, string mustNot, string source)
        {
            var contentItems = await _dataManager.GetMaybe(must, should, mustNot, source);
            if (contentItems != null)
            {
                var dataTable = ConvertToDataTable(contentItems);
                MaybeItemsBindingSource.DataSource = dataTable;
                dgvMaybe.DataSource = MaybeItemsBindingSource;
                ContentGridColumnSettings(dgvMaybe);
            }
            else
            {
                MessageBox.Show(@"Ошибка запроса.");
            }
        }

        private async void SearchOkpd2(string text)
        {
            var contentItems = await _dataManager.GetOkpd2Reverse(text);
            if (contentItems != null)
            {
                var dataTable = ConvertToDataTable(contentItems);
                Okpd2ItemsBindingSource.DataSource = dataTable;
                dgvOkpd2.DataSource = Okpd2ItemsBindingSource;
            }
            else
            {
                MessageBox.Show(@"Ошибка запроса.");
            }
        }

        private async void SearchPacket(string json, string keywords)
        {
            var dto = JsonConvert.DeserializeObject<List<SearchItemParam>>(json);
            var searchItemHeaderDtos = await _dataManager.PostPacketAsync(dto, cmbElasticIndexName.SelectedItem as string, keywords);
            SearchItemsToForm(searchItemHeaderDtos);
        }

        private async void SearchByCondition()
        {
            var dto = new SearchItemCondition
            {
                DateFrom = dtpFrom.Value,
                DateTo = dtpTo.Value,
                Name = tbConditionName.Text,
                ExtId = tbConditionExtId.Text
            };
            if (cbSearchItemStatus.SelectedIndex > 0)
            {
                dto.Status = ((SearchItemStatusItem)cbSearchItemStatus.SelectedItem).TaskStatus;
            }
            var searchItemHeaderDtos = await _dataManager.GetByConditionAsync(dto);
            SearchItemsToForm(searchItemHeaderDtos);
            if (searchItemHeaderDtos.Count == 0) MessageBox.Show(@"Данные не найдены");
        }

        private async void SearchPacket(List<SearchItemParam> dto, string keywords)
        {
            var searchItemHeaderDtos = await _dataManager.PostPacketAsync(dto, cmbElasticIndexName.SelectedItem as string, keywords);
            SearchItemsToForm(searchItemHeaderDtos);
        }

        private void ClearSearchItems()
        {
            _listSearchItem.Clear();
            ((DataTable)SearchItemsBindingSource.DataSource)?.Clear();
            ((DataTable)ContentItemsBindingSource.DataSource)?.Clear();
        }

        private void SearchItemsToForm(List<SearchItemHeaderDto> searchItemHeaderDtos)
        {
            if (searchItemHeaderDtos != null)
            {
                AddPacketItemsToList(searchItemHeaderDtos);
                if (_bw.IsBusy != true)
                {
                    _bw.RunWorkerAsync();
                }
            }
            else
            {
                MessageBox.Show(@"Ошибка запроса.");
            }
        }

        #endregion //Api calls

        private void dgv_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = (DataGridView)sender;
            var dataGridViewColumn = dgv.Columns[nameof(ContentDto.Uri)];
            if (dataGridViewColumn == null || e.RowIndex < 0) return;
            var webAddressString = dgv.Rows[e.RowIndex].Cells[dataGridViewColumn.Index].Value.ToString();
            if (string.IsNullOrEmpty(webAddressString)) return;
            Process.Start(webAddressString);
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadPacketFromFile();
        }

        private void LoadPacketFromFile()
        {
            var openFileDialog = new OpenFileDialog(); if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            tbFileName.Text = openFileDialog.FileName;
            _packetLines = File.ReadAllLines(openFileDialog.FileName);
        }

        private void btnSearchPacket_Click(object sender, EventArgs e)
        {
            if (_packetLines == null)
            {
                MessageBox.Show(@"Пустой текст пакета");
            }
            else
            {
                var text = string.Join("", _packetLines);
                SearchPacket(text, tbKeywords.Text);
            }
        }

        private void btnPacketText_Click(object sender, EventArgs e)
        {
            var form = new FormPacketText { tbTruItems = { Lines = _packetLines } };
            if (form.ShowDialog() == DialogResult.OK) _packetLines = form.tbTruItems.Lines;
        }

        private void btnRefreshSearchItems_Click(object sender, EventArgs e)
        {
            SearchByCondition();
        }

        private void btnClearSearchItems_Click(object sender, EventArgs e)
        {
            ClearSearchItems();
        }

        private void AddPacketItemsToList(List<SearchItemHeaderDto> searchItemsList)
        {
            if (searchItemsList == null) return;
            foreach (var searchItemDto in searchItemsList)
            {
                var item = _listSearchItem.FirstOrDefault(z => z.Id.Equals(searchItemDto.Id) && z.Source.Equals(searchItemDto.Source));
                if (item == null)
                {
                    _listSearchItem.Add(searchItemDto);
                }
                else
                {
                    _listSearchItem.Remove(item);
                    _listSearchItem.Add(searchItemDto);
                }
            }
            List2Grid(ref _datatableSearchItem);
        }

        private void SearchItemsDataTable_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            var id = e.Row[nameof(SearchItemHeaderDto.Id)];
            Debug.WriteLine($"SearchItem Row_Deleted Event: id={id}");
            var item = _listSearchItem.FirstOrDefault(z => z.Id.Equals(id));
            if (item != null) _listSearchItem.Remove(item);
        }

        private async void ContentItemsDataTable_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            Debug.WriteLine($"Content Row_Deleted Event: id={e.Row[nameof(ContentExtDto.Id)]}");
            // delete content by api
            await _dataManager.DeleteContentItem($"{e.Row[nameof(ContentExtDto.Id)]}", $"{e.Row[nameof(ContentExtDto.ElasticId)]}");
        }

        private void List2Grid(ref DataTable dataTable)
        {
            if (dataTable == null)
            {
                dataTable = ConvertToDataTable(_listSearchItem);
                dataTable.RowDeleting += SearchItemsDataTable_RowDeleting;
                SearchItemsBindingSource.DataSource = dataTable;
                dgvPacketItems.DataSource = SearchItemsBindingSource;
                PacketGridColumnSettings();
            }
            else
            {
                var properties = TypeDescriptor.GetProperties(typeof(SearchItemHeaderDto));
                foreach (var searchItemDto in _listSearchItem)
                {
                    var foundRows = dataTable.Select($"{nameof(SearchItemHeaderDto.Id)}='{searchItemDto.Id}'");
                    if (foundRows.Any())
                    {
                        foreach (var dataRow in foundRows)
                        {
                            dataRow.SetField(nameof(SearchItemHeaderDto.StartProcessed), searchItemDto.StartProcessed);
                            dataRow.SetField(nameof(SearchItemHeaderDto.LastUpdate), searchItemDto.LastUpdate);
                            dataRow.SetField(nameof(SearchItemHeaderDto.ProcessedAt), searchItemDto.ProcessedAt);
                            dataRow.SetField(nameof(SearchItemHeaderDto.StartProcessedDateTime), searchItemDto.StartProcessedDateTime);
                            dataRow.SetField(nameof(SearchItemHeaderDto.LastUpdateDateTime), searchItemDto.LastUpdateDateTime);
                            dataRow.SetField(nameof(SearchItemHeaderDto.ProcessedAtDateTime), searchItemDto.ProcessedAtDateTime);
                            dataRow.SetField(nameof(SearchItemHeaderDto.Status), searchItemDto.Status);
                            dataRow.SetField(nameof(SearchItemHeaderDto.StatusString), searchItemDto.StatusString);

                            // refresh datagrid
                            if (dgvPacketItems.CurrentRow != null && dgvPacketItems.CurrentRow.Cells[nameof(SearchItemHeaderDto.Id)].Value.ToString() ==
                                dataRow.Field<string>(nameof(SearchItemHeaderDto.Id)))
                            {
                                if (dataRow.Field<int>(nameof(SearchItemHeaderDto.ContentCount)) != searchItemDto.ContentCount)
                                {
                                    PacketItemsOnCurrentChanged(null, null);
                                }

                            }
                            dataRow.SetField(nameof(SearchItemHeaderDto.ContentCount), searchItemDto.ContentCount);
                        }
                    }
                    else
                    {
                        var row = dataTable.NewRow();
                        foreach (PropertyDescriptor prop in properties)
                            row[prop.Name] = prop.GetValue(searchItemDto) ?? DBNull.Value;

                        dataTable.Rows.Add(row);
                    }
                }
            }
        }



        private static DataTable ConvertToDataTable<T>(IEnumerable<T> data)
        {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            try
            {
                foreach (PropertyDescriptor prop in properties)
                {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
                if (data == null) return table;
                foreach (var item in data)
                {
                    var row = table.NewRow();

                    foreach (PropertyDescriptor prop in properties)
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                    table.Rows.Add(row);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                throw;
            }
            return table;
        }

        private async void PacketItemsOnCurrentChanged(object sender, EventArgs e)
        {
            try
            {
                if (SearchItemsBindingSource.Current == null)
                {
                    ((DataTable)ContentItemsBindingSource.DataSource)?.Clear();
                    return;
                }
                var current = (DataRowView)SearchItemsBindingSource.Current;
                var key = current.Row[nameof(SearchItemHeaderDto.Id)] as string;
                //EnableResultButtons = (int)current.Row[nameof(SearchItemDto.Status)] == (int)TaskStatus.Ok && ((string)current.Row[nameof(SearchItemDto.Source)]).Contains("internet");
                EnableResultButtons = (int)current.Row[nameof(SearchItemDto.Status)] == (int)TaskStatus.Ok;
                EnableResultButtons = EnableResultButtons || (int)current.Row[nameof(SearchItemDto.Status)] == (int)TaskStatus.Break;
                EnableResultButtons = EnableResultButtons || (int)current.Row[nameof(SearchItemDto.Status)] == (int)TaskStatus.BreakByTimeout;
                //var contentItems = _listSearchItem.FirstOrDefault(z => z.Key.Equals(key))?.Content ?? new List<ContentDto>();
                var contentItems = await _dataManager.GetSearchItemContent(key);
                var dataTable = ConvertToDataTable(contentItems);
                dataTable.RowDeleting += ContentItemsDataTable_RowDeleting;
                ContentItemsBindingSource.DataSource = dataTable;
                dgvContentItems.DataSource = ContentItemsBindingSource;
                ContentGridColumnSettings(dgvContentItems);
                label13.Text = current.Row[nameof(SearchItemHeaderDto.Name)] as string;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                //throw;
            }
        }

        private void ContentItemsOnCurrentChanged(object sender, EventArgs e)
        {
            try
            {
                if (ContentItemsBindingSource.Current == null)
                {
                    ClearContentView();
                    return;
                }
                var current = (DataRowView)ContentItemsBindingSource.Current;
                var url = current.Row[nameof(ContentExtDto.Screenshot)] as string;
                _rect.Width = 0;
                _rect.Height = 0;
                if (string.IsNullOrEmpty(url))
                {
                    pbWebshot.Image = null;
                }
                else
                {
                    pbWebshot.LoadAsync(url);
                }
                lblPrice.Text = current.Row[nameof(ContentExtDto.Price)] as string;
                linkLabelUrl.Text = current.Row[nameof(ContentExtDto.Uri)] as string;
                linkLabelScreenshot.Text = current.Row[nameof(ContentExtDto.Screenshot)] as string;
                label15.Text = current.Row[nameof(ContentExtDto.Name)] as string;
                var priceVariants = current.Row[nameof(ContentExtDto.PriceVariants)] as string;
                cmbPrices.Items.Clear();
                cmbPrices.Text = "";
                if (!string.IsNullOrEmpty(priceVariants))
                {
                    var items = priceVariants.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    cmbPrices.Items.AddRange(items);
                    cmbPrices.SelectedIndex = 0;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private void CallApi4OneItemPacket_Click(object sender, EventArgs e)
        {
            byte options = 0;
            if (!cbSeparateRequest.Checked) options |= 8;
            if (cbNotExtractPrice.Checked) options |= 2;
            byte searchEngine = 0;
            if (cbYandex.Checked) searchEngine |= SearchItemParam.UseYandex;
            if (cbGoogle.Checked) searchEngine |= SearchItemParam.UseGoogle;
            var dto = new List<SearchItemParam>
            {
                new SearchItemParam
                {
                    Id = string.IsNullOrEmpty(tbSingleExtId.Text) ? Md5Logstah.GetDefaultId("", textBox2.Text) : tbSingleExtId.Text,
                    Name = textBox2.Text,
                    Norm = cmbNorm.SelectedItem as string,
                    Priority = priorityList[cmbPriority.SelectedIndex],
                    Options = options,
                    SearchEngine = searchEngine
                }
            };
            SearchPacket(dto, tbKeywords.Text);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var baseApi = ConfigurationManager.AppSettings["BaseApi"];

            try
            {
                Process.Start($"{baseApi}");
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                MessageBox.Show($@"Not started {baseApi}");
            }
        }

        private static void linkLabelUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var url = ((LinkLabel)sender).Text;
            Process.Start(url);
        }

        private void dgvContentItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            return;
            ((DataGridView)sender).CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //_engine.Dispose();
            Settings.Default.Save();
            Application.Exit();
        }

        private void lblSellerCount_Click(object sender, EventArgs e)
        {

        }

        private void bwSellerCount_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var sellerCount = _dataManager.GetSellerCount().Result;
                ((BackgroundWorker)sender).ReportProgress(0, sellerCount);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                Log.Error(exception);
            }
        }

        private void bwSellerCount_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                try
                {
                    var sellerCount = (int)e.UserState;
                    lblSellerCount.Text = $@"{sellerCount}";
                }
                catch (Exception exception)
                {
                    Log.Error(exception);
                }
            }
        }

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            var bankControl = CompositionRoot.Resolve<ISellerView>();
            var control = (Control) bankControl;
            control.Dock = DockStyle.Fill;
            tabPage4.Controls.Clear();
            tabPage4.Controls.Add(control);
        }
    }
}
