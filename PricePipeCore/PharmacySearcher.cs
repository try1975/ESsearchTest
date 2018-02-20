using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using Norm.MedPrep;
using PriceCommon.Model;
using PriceCommon.Norm;
using PriceCommon.Utils;

namespace PricePipeCore
{
    public class PharmacySearcher
    {
        private readonly ElasticClient _elasticClient;

        private readonly int _maxTake;
        private INorm _norm;
        private List<Content> _founded;

        public PharmacySearcher(string source)
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

        public IEnumerable<Content> Search(string name = "", string firstWords = ""
            , string lekForm = "", string upak = "", string dozValue = "", string dozKey = "", string normNumber = "", string syn="")
        {
            var medPrepNorm = new MedPrepNorm(_elasticClient, normNumber);
            _norm = medPrepNorm;
            _norm.InitialName = name;
            if (!string.IsNullOrEmpty(firstWords)) medPrepNorm.FirstWords = firstWords;
            if (!string.IsNullOrEmpty(lekForm)) medPrepNorm.LekFormNorm.NormResult = lekForm;
            if (!string.IsNullOrEmpty(upak)) medPrepNorm.UpakNorm.NormResult = upak;
            if (!string.IsNullOrEmpty(dozValue) && !string.IsNullOrEmpty(dozKey))
            {
                medPrepNorm.DozNorm.NormResult = $"{dozValue} {dozKey}";
                medPrepNorm.DozNorm.DozKey = dozKey;
                medPrepNorm.DozNorm.DozValue = dozValue;
            }
            if (!string.IsNullOrEmpty(syn)) medPrepNorm.Syn = syn;

            var container = GetQueryContainer();
            if (container.Count == 0) return new List<Content>();
            var response = _elasticClient.Search<Content>(s => s
                .Take(_maxTake)
                .Query(q => q
                    .Bool(b => b
                        .Must(container.ToArray())))
                );
            return response.Hits.Select(s => s.Source);
        }

        public decimal GetNmck(string name)
        {

            _founded = Search(name).ToList();
            return Calculate();
        }

        private decimal Calculate()
        {
            string text;
            var prices = _founded.Select(z => z.Nprice).ToList();
            return (decimal)Utils.GetPriceCalculation(prices, out text);
        }

        private List<QueryContainer> GetQueryContainer()
        {
            var queryContainer = new List<QueryContainer>();
            queryContainer.AddRange(_norm.QueryContainer);
            return queryContainer;
        }
    }
}