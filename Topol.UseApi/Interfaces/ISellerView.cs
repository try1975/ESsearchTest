using Common.Dto.Model.FindCompany;
using Topol.UseApi.Interfaces.Common;

namespace Topol.UseApi.Interfaces
{
    public interface ISellerView : ITypedView<FindCompanyDto, int>
    {
        string Host { get; set; }
        string Inn { get; set; }
        string SellerName { get; set; }
    }
}