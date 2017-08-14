﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

        public PharmacySearcher(string defaultIndex)
        {
            _maxTake = 200;
            try
            {
                //var connectionSettings = new ConnectionSettings(new Uri(address))
                //    .OnRequestCompleted(details =>
                //    {
                //        var s = details.RequestBodyInBytes != null
                //            ? Encoding.UTF8.GetString(details.RequestBodyInBytes)
                //            : null;
                //        Debug.WriteLine($"{s}");
                //    })
                //    .DisableDirectStreaming()
                //    .DefaultIndex(defaultIndex)
                //    .BasicAuthentication(userName, password);
                //_elasticClient = new ElasticClient(connectionSettings);
                _elasticClient = ElasticClientFactory.GetElasticClient(defaultIndex);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                throw;
            }
        }

        public IEnumerable<Content> Search(string name = "", string firstWords = ""
            , string lekForm = "", string upak = "", string dozValue = "", string dozKey = "", string normNumber = "")
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