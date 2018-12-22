using System.Collections.Generic;
using System.Web.Http;
using Common.Dto.Model.FindCompany;
using Price.WebApi.Maintenance.Interfaces;

namespace Price.WebApi.Controllers
{
    public class FindCompanyController : TypedController<FindCompanyDto, int>
    {
        public FindCompanyController(IFindCompanyApi api) : base(api)
        {
        }


        /// <summary>
        /// Количество продавцов
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/FindCompany/count", Name = nameof(GetLimitedCount) + "Route")]
        public int GetLimitedCount([FromUri] string search = null)
        {
            return ((IFindCompanyApi)_api).GetSellerCount(search);
        }
        /// <summary>
        /// Фильтрованный постраничный список организаций
        /// </summary>
        /// <param name="search">фильтр</param>
        /// <param name="pageSize">размер страницы (100 по умолчанию)</param>
        /// <param name="pageNum">номер страницы</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/FindCompany/filtered", Name = nameof(GetLimited) + "Route")]
        public IEnumerable<FindCompanyDto> GetLimited([FromUri] string search = null, int pageSize = 0, int pageNum = 0)
        {
            var list = ((IFindCompanyApi)_api).GetLimitedFindCompany(search, pageSize, pageNum);
            var dtos = list ?? new List<FindCompanyDto>();
            return dtos;
        }
    }
}
