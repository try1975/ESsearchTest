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
        public static string[] ListDelimiter = {";"};
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
            var container = GetExactQueryContainer(text, ListDelimiter);
            if (container.Count == 0) return new List<Content>();
            var response = _elasticClient.Search<Content>(s => s
                .Take(_maxTake)
                .Query(q => q
                    .Bool(b => b
                        .Must(container.ToArray())))
                );
            return response.Hits.Select(s => s.Source);
        }
        public IEnumerable<Content> MaybeSearch(string must, string should, string mustNot)
        {
            var containerMust = GetExactQueryContainer(must, ListDelimiter);
            var containerShould = GetExactQueryContainer(should, ListDelimiter);
            var containerMustNot = GetExactQueryContainer(mustNot, ListDelimiter);
            if ((containerMust.Count == 0) && (containerShould.Count == 0) && (containerMustNot.Count == 0)) return new List<Content>();
            var response = _elasticClient.Search<Content>(s => s
                .Take(_maxTake)
                .Query(q => q
                    .Bool(b => b
                        .Must(containerMust.ToArray())
                        .Should(containerShould.ToArray())
                        .MustNot(containerMustNot.ToArray())
                        ))
                );
            return response.Hits.Select(s => s.Source);
        }

        private static List<QueryContainer> GetExactQueryContainer(string text, string[] splitter)
        {
            var queryContainer = new List<QueryContainer>();
            var exactRows = text.ToLower().Split(splitter, StringSplitOptions.RemoveEmptyEntries);
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