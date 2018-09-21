using System;
using System.Windows.Forms;
using Topol.UseApi.Interfaces;
using Topol.UseApi.Interfaces.Common;
using Topol.UseApi.Interfaces.Data;

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
            _presenter = new SellerPresenter(this, bankDataMаnager, dataMаnager);
        }
    }
}
