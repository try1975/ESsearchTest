using System.Collections.Generic;
using Common.Dto.Model.FindCompany;

namespace Price.WebApi.Maintenance.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFindCompanyApi : ITypedApi<FindCompanyDto, int>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int GetSellerCount(string search = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        IEnumerable<FindCompanyDto> GetLimitedFindCompany(string search = null, int pageSize = 0, int pageNum = 0);
    }
}