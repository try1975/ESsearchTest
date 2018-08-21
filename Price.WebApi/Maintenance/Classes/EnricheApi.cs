using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using FindCompany.Db.Entities.QueryProcessors;
using Price.WebApi.Maintenance.Interfaces;

namespace Price.WebApi.Maintenance.Classes
{
    public class EnricheApi : IEnricheApi
    {
        private readonly IFindCompanyQuery _findCompanyQuery;
        private readonly ConcurrentDictionary<string, string> _sellersConcurrentDictionary;

        public EnricheApi(IFindCompanyQuery findCompanyQuery)
        {
            _findCompanyQuery = findCompanyQuery;
            var findCompanies = _findCompanyQuery.GetEntities()
                .Select(z => new {z.Host, z.Name}).ToList();
            _sellersConcurrentDictionary = new ConcurrentDictionary<string, string>(findCompanies.Select(z=> new KeyValuePair<string, string>(z.Host, z.Name)));
        }

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