using System.Windows.Forms;
using PriceCommon.Model;

namespace ESsearchTest
{
    public static class GridUtils
    {
        public static void SetTablesColumns(DataGridView dgv)
        {
            dgv.RowHeadersWidth = 10;
            var dataGridViewColumn = dgv.Columns[nameof(Content.CollectedAt)];
            if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
            dataGridViewColumn = dgv.Columns[nameof(Content.Id)];
            if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
            dataGridViewColumn = dgv.Columns[nameof(History.Idc)];
            if (dataGridViewColumn != null) dataGridViewColumn.Visible = false;
            dataGridViewColumn = dgv.Columns[nameof(Content.Seller)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.Visible = false;
                dataGridViewColumn.HeaderText = @"Поставщик";
            }
            dataGridViewColumn = dgv.Columns[nameof(Content.Selected)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.SortMode = DataGridViewColumnSortMode.Automatic;
                dataGridViewColumn.HeaderText = @"Отмечен";
                dataGridViewColumn.Width = 60;
                dataGridViewColumn.DisplayIndex = 1;
            }

            dataGridViewColumn = dgv.Columns[nameof(Content.Name)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.Width = 500;
                dataGridViewColumn.HeaderText = @"Наименование ТРУ";
                dataGridViewColumn.ReadOnly = true;
                if (dgv.Name.Equals("dgvSearchResult")) dataGridViewColumn.DisplayIndex = 2;
            }
            dataGridViewColumn = dgv.Columns[nameof(Content.Price)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.Visible = false;
            }
            dataGridViewColumn = dgv.Columns[nameof(Content.Nprice)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Цена";
                dataGridViewColumn.ReadOnly = true;
                dataGridViewColumn.DisplayIndex = 3;
            }
            dataGridViewColumn = dgv.Columns[nameof(Content.Uri)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.Width = 500;
                dataGridViewColumn.HeaderText = @"Ссылка на ТРУ";
                dataGridViewColumn.ReadOnly = true;
                dataGridViewColumn.DisplayIndex = 4;
            }
            dataGridViewColumn = dgv.Columns[nameof(Content.Collected)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Дата";
                dataGridViewColumn.DisplayIndex = 5;
            }
            dataGridViewColumn = dgv.Columns[nameof(Expert.Price2016)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Цена(2016)";
            }
            dataGridViewColumn = dgv.Columns[nameof(Expert.Price2017)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Цена(2017)";
            }
            dataGridViewColumn = dgv.Columns[nameof(Expert.Price2018)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Цена(2018)";
            }
            dataGridViewColumn = dgv.Columns[nameof(Expert.TruPrim)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Уточнение";
            }
            dataGridViewColumn = dgv.Columns[nameof(Expert.Edizm)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Единица измерения";
            }
            dataGridViewColumn = dgv.Columns[nameof(Expert.ExpertDate)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Дата";
            }
            dataGridViewColumn = dgv.Columns[nameof(Expert.ExpertName)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Эксперт";
            }
            dataGridViewColumn = dgv.Columns[nameof(Expert.ExpertNumber)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"№ экспертизы";
            }
            dataGridViewColumn = dgv.Columns[nameof(Expert.Npp)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"№ п/п";
            }
            dataGridViewColumn = dgv.Columns[nameof(Expert.KpgzName)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Наименование КПГЗ (ЕАИСТ 2.0)";
            }
            dataGridViewColumn = dgv.Columns[nameof(Expert.KpgzCode)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"№ КПГЗ (ЕАИСТ 2.0)";
            }
            dataGridViewColumn = dgv.Columns[nameof(Expert.SpgzName)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"СПГЗ";
            }
            dataGridViewColumn = dgv.Columns[nameof(Expert.Okpd2)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"ОКПД2";
            }
            dataGridViewColumn = dgv.Columns[nameof(Expert.Comment)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Комментарий";
            }
            dataGridViewColumn = dgv.Columns[nameof(Expert.Comment2)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Примечание";
            }
            dataGridViewColumn = dgv.Columns[nameof(Expert.ZakupkiLink)];
            if (dataGridViewColumn != null)
            {
                dataGridViewColumn.HeaderText = @"Ссылка на закупки";
            }
        }
    }
}
