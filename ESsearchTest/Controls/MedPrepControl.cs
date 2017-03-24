using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Nest;
using Norm.MedPrep;
using PriceCommon.Norm;

namespace ESsearchTest.Controls
{
    public partial class MedPrepControl : UserControl, IMedPrepControl, INorm
    {
        private readonly MedPrepNorm _medPrepNorm;
        private string _doz;
        private string _dozEd;
        private string _firstWords;
        private string _initialName;
        private string _lekForm;
        private string _upak;
        private string _syn;

        public MedPrepControl()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime) _medPrepNorm = new MedPrepNorm(this);
        }


        public string FirstWords
        {
            get { return _firstWords; }
            set
            {
                if (_firstWords == value) return;
                _firstWords = value;
                tbFirstWords.Text = _firstWords;
            }
        }

        public string LekForm
        {
            get { return _lekForm; }
            set
            {
                if (_lekForm == value) return;
                _lekForm = value;
                cmbLekForm.SelectedIndex = cmbLekForm.FindString(_lekForm);
            }
        }

        public string Doz
        {
            get { return _doz; }
            set
            {
                _doz = value;
                tbDoz.Text = _doz;
            }
        }

        public string DozEd
        {
            get { return _dozEd; }
            set
            {
                if (_dozEd == value) return;
                _dozEd = value;
                cmbDoz.SelectedIndex = cmbDoz.FindString(_dozEd);
            }
        }


        public string Upak
        {
            get { return _upak; }
            set
            {
                if (_upak == value) return;
                _upak = value;
                tbUpak.Text = _upak;
            }
        }

        public string Ob { get; set; }

        public string Syn
        {
            get { return _syn; }
            set
            {
                if(_syn==value) return;
                _syn = value;
                clbSyn.ItemCheck -= clbSyn_ItemCheck;
                clbSyn.Items.Clear();
                foreach (var s in _syn.Split(','))
                {
                    clbSyn.Items.Add(s, true);
                }
                clbSyn.ItemCheck += clbSyn_ItemCheck;
            }
        }

        public List<string> LekFormList
        {
            get { return (from object item in cmbLekForm.Items select item.ToString()).ToList(); }
            set
            {
                cmbLekForm.Items.Clear();
                foreach (var item in value)
                {
                    cmbLekForm.Items.Add(item);
                }
            }
        }

        public List<string> DozEdList
        {
            get { return (from object item in cmbDoz.Items select item.ToString()).ToList(); }
            set
            {
                cmbDoz.Items.Clear();
                foreach (var item in value)
                {
                    cmbDoz.Items.Add(item);
                }
            }
        }

        public string InitialName
        {
            get { return _initialName; }
            set
            {
                if (_initialName == value) return;
                _initialName = value;
                if (_medPrepNorm == null) return;
                _medPrepNorm.InitialName = value;
                NormResult = _medPrepNorm.NormResult;
            }
        }

        public string NormResult { get; set; }

        public List<QueryContainer> QueryContainer => _medPrepNorm?.QueryContainer;

        private void cmbLekForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            _medPrepNorm.LekFormNorm.NormResult = $"{cmbLekForm.SelectedItem}";
        }

        private void tbUpak_TextChanged(object sender, EventArgs e)
        {
            _medPrepNorm.UpakNorm.NormResult = tbUpak.Text;
        }

        private void tbDoz_TextChanged(object sender, EventArgs e)
        {
            _medPrepNorm.DozNorm.DozValue = tbDoz.Text;
            _medPrepNorm.DozNorm.NormResult = tbDoz.Text;
        }

        private void cmbDoz_SelectedIndexChanged(object sender, EventArgs e)
        {
            _medPrepNorm.DozNorm.DozKey = $"{cmbDoz.SelectedItem}";
        }

        private void tbFirstWords_TextChanged(object sender, EventArgs e)
        {
            _medPrepNorm.FirstWords = tbFirstWords.Text;
        }

        private void clbSyn_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var checkedItems = (from object item in clbSyn.CheckedItems select item.ToString()).ToList();
            if (e.NewValue == CheckState.Checked)
            {
                checkedItems.Add(clbSyn.Items[e.Index].ToString());
            }
            else
            {
                checkedItems.Remove(clbSyn.Items[e.Index].ToString());
            }
            
            _syn = string.Join(",", checkedItems);
        }
    }
}