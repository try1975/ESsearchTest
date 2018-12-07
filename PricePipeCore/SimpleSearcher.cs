using Nest;
using PriceCommon.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PricePipeCore
{
    public class SimpleSearcher
    {
        public static readonly string[] ListDelimiter = { ";", " " };
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

        public int GetSellerCount()
        {
            try
            {
                var searchResponse = _elasticClient.Search<Content>(s => s
                    .Aggregations(a => a
                        .Terms("unique", te => te
                            .Field(nameof(Content.Seller).ToLower())
                            .MinimumDocumentCount(10)
                            .Size(1000)
                        )
                    )
                );
                var sellers = searchResponse.Aggs.Terms("unique").Buckets.Select(b => b.Key).ToList();
                return sellers.Count;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            return 0;
        }

        private static List<QueryContainer> GetExactQueryContainer(string text, string[] splitter)
        {
            var queryContainer = new List<QueryContainer>();
            if (text == null) return queryContainer;
            var rows = text.ToLower().Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            foreach (var row in rows)
            {
                if (string.IsNullOrEmpty(row)) continue;
                queryContainer.Add(Query<Content>
                    .Match(m => m
                        .Field(p => p.Name)
                        .Query(row.Trim())));
            }
            return queryContainer;
        }

        public IEnumerable<Content> Okpd2Search(string okpd2)
        {
            if (string.IsNullOrWhiteSpace(okpd2)) return new List<Content>();
            var rows = okpd2.ToLower().Split(ListDelimiter, StringSplitOptions.RemoveEmptyEntries);
            var container = new List<QueryContainer>();
            foreach (var row in rows)
            {
                container.Add(Query<Content>
                    .Match(m => m
                        .Field(p => p.Okpd2)
                        .Query(row.Trim())));
            };
            var response = _elasticClient.Search<Content>(s => s
                .Take(_maxTake)
                .Query(q => q
                    .Bool(b => b
                        .Must(container.ToArray())))
            );
            return response.Hits.Select(s => s.Source);
        }
    }
}