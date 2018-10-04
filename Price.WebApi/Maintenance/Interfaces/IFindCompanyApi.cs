using Common.Dto.Model.FindCompany;

namespace Price.WebApi.Maintenance.Interfaces
{
    public interface IFindCompanyApi : ITypedApi<FindCompanyDto, int>
    {
        int GetSellerCount();
    }
}