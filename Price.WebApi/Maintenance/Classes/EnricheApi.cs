using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using FindCompany.Db.Entities.QueryProcessors;
using Price.WebApi.Maintenance.Interfaces;

namespace Price.WebApi.Maintenance.Classes
{
    /// <summary>
    /// 
    /// </summary>
    public class EnricheApi : IEnricheApi
    {
        private readonly ConcurrentDictionary<string, string> _sellersConcurrentDictionary;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="findCompanyQuery"></param>
        public EnricheApi(IFindCompanyQuery findCompanyQuery)
        {
            var findCompanies = findCompanyQuery.GetEntities()
                .Select(z => new {z.Host, z.Name}).ToList();
            _sellersConcurrentDictionary = new ConcurrentDictionary<string, string>(findCompanies.Select(z=> new KeyValuePair<string, string>(z.Host, z.Name)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetSeller(string url)
        {
            if (string.IsNullOrEmpty(url)) return string.Empty;
            try
            {
                var uri = new Uri(url);
                var host = uri.Host;
                string seller;
                if (!_sellersConcurrentDictionary.TryGetValue(host, out seller))
                {
                  return  string.Empty;
                }
                return seller;

            }
            catch (Exception exception)
            {
                Logger.Log.Error(exception);
            }
            return string.Empty;
        }
    }
}