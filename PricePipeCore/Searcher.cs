using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Nest;
using Norm.MedPrep.Norm;
using PriceCommon.Model;
using PriceCommon.Norm;
using PriceCommon.Utils;

namespace PricePipeCore
{
    public class Searcher
    {
        private readonly string _defaultIndex;
        private readonly ElasticClient _elasticClient;
        private List<Content> _founded;
        private readonly INorm _norm;

        private readonly int _maxTake;

        public Searcher(string defaultIndex, string address, string userName, string password)
        {
            _maxTake = 200;

            ConnectionSettings connectionSettings;
            _defaultIndex = defaultIndex;
            try
            {
                connectionSettings = new ConnectionSettings(new Uri(address))
                    .OnRequestCompleted(details =>
                    {
                        var s = details.RequestBodyInBytes != null
                            ? Encoding.UTF8.GetString(details.RequestBodyInBytes)
                            : null;
                        Debug.WriteLine($"{s}");
                    })
                    .DisableDirectStreaming()
                    .DefaultIndex(_defaultIndex)
                    .BasicAuthentication(userName, password)
                    ;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                throw;
            }
            try
            {
                _elasticClient = new ElasticClient(connectionSettings);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                throw;
            }

            _norm = new MedPrepNorm();
        }

        public decimal GetNmck(string name)
        {
            _norm.InitialName = name;
            Search();
            return Calculate();
        }

        private decimal Calculate()
        {
            string calculationText;
            var prices = _founded.Select(z => z.Nprice).ToList();
            return (decimal) Utils.GetPriceCalculation(prices, out calculationText);
        }

        private void Search()
        {
            //Search query to retrieve info
            var queryContainer = GetQueryContainer();
            if (queryContainer.Count == 0) return;
            var response = _elasticClient.Search<Content>(s => s
                .Take(_maxTake)
                .Query(q => q
                    .Bool(b => b
                        .Must(queryContainer.ToArray())))
                );

            _founded = response.Hits.Select(s => s.Source).ToList();
        }

        private List<QueryContainer> GetQueryContainer()
        {
            var queryContainer = new List<QueryContainer>();
            queryContainer.AddRange(_norm.QueryContainer);
            return queryContainer;
        }
    }
}
