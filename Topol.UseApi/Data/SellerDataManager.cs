using Common.Dto.Model.FindCompany;
using PriceCommon;
using Topol.UseApi.Data.Common;
using Topol.UseApi.Interfaces.Data;

namespace Topol.UseApi.Data
{
    public class SellerDataManager : TypedDataMаnager<FindCompanyDto, int>, ISellerDataManager
    {
        public SellerDataManager() : base(PriceCommonConstants.ClientAppApi.FindCompany)
        {
        }
    }
}