using Common.Dto.Model.FindCompany;
using Price.WebApi.Maintenance.Interfaces;

namespace Price.WebApi.Controllers
{
    public class FindCompanyController : TypedController<FindCompanyDto, int>
    {
        public FindCompanyController(IFindCompanyApi api) : base(api)
        {
        }
    }
}
