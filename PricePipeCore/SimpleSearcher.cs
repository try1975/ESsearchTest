using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Nest;
using PriceCommon.Model;

namespace PricePipeCore
{
    public class SimpleSearcher
    {
        private readonly ElasticClient _elasticClient;
        private readonly int _maxTake;

        public SimpleSearcher(string source)
        {
            _maxTake = 200;
            try
            {
                _elasticClient = ElasticClientFactory.GetElasticClient(source);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                throw;
            }
        }

        public IEnumerable<Content> SimpleSearch(string text)
        {
            var container = GetExactQueryContainer(text);
            if (container.Count == 0) return new List<Content>();
            var response = _elasticClient.Search<Content>(s => s
                .Take(_maxTake)
                .Query(q => q
                    .Bool(b => b
                        .Must(container.ToArray())))
                );
            return response.Hits.Select(s => s.Source);
        }

        private static List<QueryContainer> GetExactQueryContainer(string text)
        {
            var queryContainer = new List<QueryContainer>();
            var exactRows = text.ToLower().Split(' ');
            foreach (var row in exactRows)
            {
                if (string.IsNullOrEmpty(row)) continue;
                queryContainer.Add(Query<Content>
                    .Match(m => m
                        .Field(p => p.Name)
                        .Query(row.Trim())));
            }
            return queryContainer;
        }
    }
}