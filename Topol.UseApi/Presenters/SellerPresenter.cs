using Common.Dto.Model.FindCompany;
using Topol.UseApi.Interfaces;
using Topol.UseApi.Interfaces.Common;
using Topol.UseApi.Presenters.Common;
using Topol.UseApi.Interfaces.Data;

namespace Topol.UseApi.Presenters
{
    public class SellerPresenter : TypedPresenter<FindCompanyDto, int>
    {
        public SellerPresenter(ISellerView view, ISellerDataManager typedDataMаnager, IDataMаnager dataMаnager)
            : base(view, typedDataMаnager, dataMаnager)
        {
        }
    }
}