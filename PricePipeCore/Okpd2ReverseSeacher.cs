using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using PriceCommon.Model;

namespace PricePipeCore
{
    public class Okpd2ReverseSeacher
    {
        private readonly ElasticClient _elasticClient;

        public Okpd2ReverseSeacher()
        {
            try
            {
                _elasticClient = ElasticClientFactory.GetElasticClient(nameof(ElacticIndexName.Gz));
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                throw;
            }
        }

        public IEnumerable<Okpd2Reverse> Search(string text)
        {
            const string aggs = "okpd2";
            var response = _elasticClient.Search<Content>(s => s
                .Query(q => q.QueryString(qs => qs.Query(text).DefaultOperator((Operator?)DefaultOperator.And)))
                   .Aggregations(a => a
                       .Terms(aggs, te => te
                           .Field($"{nameof(Content.Okpd2).ToLower()}.keyword")
                           .MinimumDocumentCount(2)
                           .Size(100)
                       )
                   )
                   );
            return response.Aggs.Terms(aggs).Buckets.Select(z => new Okpd2Reverse()
            {
                Okpd2 = z.Key,
                DocCount = z.DocCount
            }).ToList();
        }
    }
}