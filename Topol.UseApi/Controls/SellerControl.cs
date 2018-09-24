using System;
using System.Windows.Forms;
using Common.Dto.Model.FindCompany;
using Topol.UseApi.Interfaces;
using Topol.UseApi.Interfaces.Common;
using Topol.UseApi.Interfaces.Data;
using Topol.UseApi.Presenters;

namespace Topol.UseApi.Controls
{
    public partial class SellerControl : UserControl, ISellerView
    {
        private readonly IPresenter _presenter;
        private DateTime? _closed;
        private bool _isEventHandlerSets;

        public SellerControl(ISellerDataManager sellerDataMаnager, IDataMаnager dataMаnager)
        {
            InitializeComponent();
            _presenter = new SellerPresenter(this, sellerDataMаnager, dataMаnager);
        }

        #region ISellerView

        public int Id
        {
            get
            {
                if (string.IsNullOrEmpty(tbId.Text)) return 0;
                return int.TryParse(tbId.Text, out var id) ? id : 0;
            }
            set => tbId.Text = value.ToString();
        }
        public string SellerName
        {
            get => tbName.Text;
            set => tbName.Text = value;
        }
        public string Host
        {
            get => tbHost.Text;
            set => tbHost.Text = value;
        }
        public string Inn
        {
            get => tbInn.Text;
            set => tbInn.Text = value;
        }
        #endregion //ISellerView

        #region IRefreshedView

        public void RefreshItems()
        {
            dgvItems.DataSource = _presenter.BindingSource;
            // hide columns
            var column = dgvItems.Columns[nameof(FindCompanyDto.Id)];
            if (column != null) column.Visible = false;

            column = dgvItems.Columns[nameof(FindCompanyDto.Host)];
            if (column != null) column.HeaderText = @"Хост";

            column = dgvItems.Columns[nameof(FindCompanyDto.Inn)];
            if (column != null) column.HeaderText = @"ИНН";

            column = dgvItems.Columns[nameof(FindCompanyDto.Name)];
            if (column != null) column.HeaderText = @"Продавец";
        }

        public void SetEventHandlers()
        {
            if (_isEventHandlerSets) return;
            _isEventHandlerSets = true;

            dgvItems.SelectionChanged += dgvItems_SelectionChanged;
            dgvItems.FilterStringChanged += dgvItems_FilterStringChanged;
            dgvItems.SortStringChanged += dgvItems_SortStringChanged;

            btnAddNew.Click += btnAddNew_Click;
            btnEdit.Click += btnEdit_Click;
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
            btnDelete.Click += btnDelete_Click;
        }

        #endregion //IRefreshedView

        #region IEnterMode

        public void EnterAddNewMode()
        {
            ClearInputFields();
            EnableInput();

            btnDelete.Enabled = false;
            btnCancel.Enabled = true;
            btnSave.Enabled = true;
            btnEdit.Enabled = false;
            btnAddNew.Enabled = false;
        }

        public void EnterEditMode()
        {
            EnableInput();

            btnDelete.Enabled = false;
            btnCancel.Enabled = true;
            btnSave.Enabled = true;
            btnEdit.Enabled = false;
            btnAddNew.Enabled = false;
        }

        public void EnterDetailsMode()
        {
            DisableInput();

            btnDelete.Enabled = true;
            btnCancel.Enabled = false;
            btnSave.Enabled = false;
            btnEdit.Enabled = true;
            btnAddNew.Enabled = true;
        }

        public void EnterReadMode()
        {
            ClearInputFields();
            DisableInput();

            btnDelete.Enabled = false;
            btnCancel.Enabled = false;
            btnSave.Enabled = false;
            btnEdit.Enabled = false;
            btnAddNew.Enabled = true;
        }

        public void EnterMultiSelectMode()
        {
            ClearInputFields();
            DisableInput();

            btnDelete.Enabled = false;
            btnCancel.Enabled = false;
            btnSave.Enabled = false;
            btnEdit.Enabled = false;
            btnAddNew.Enabled = true;
        }

        public void ClearInputFields()
        {
            tbId.Clear();
            tbName.Clear();
            tbHost.Clear();
            tbInn.Clear();
        }

        public void EnableInput()
        {
            //tbId.Enabled = true;
            tbName.Enabled = true;
            tbHost.Enabled = true;
            tbInn.Enabled = true;
        }

        public void DisableInput()
        {
            tbId.Enabled = false;
            tbName.Enabled = false;
            tbHost.Enabled = false;
            tbInn.Enabled = false;
        }

        #endregion //IEnterMode

        #region Event handlers

        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            _presenter.SetDetailData();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            _presenter.AddNew();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            _presenter.Edit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _presenter.Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _presenter.Cancel();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _presenter.Delete();
        }

        private void dgvItems_FilterStringChanged(object sender, EventArgs e)
        {
            _presenter.BindingSource.Filter = dgvItems.FilterString;
        }

        private void dgvItems_SortStringChanged(object sender, EventArgs e)
        {
            _presenter.BindingSource.Sort = dgvItems.SortString;
        }

        #endregion


    }
}
