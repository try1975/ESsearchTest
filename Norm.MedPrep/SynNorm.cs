using System.Collections.Generic;
using System.Linq;
using Nest;
using PriceCommon.Model;
using PriceCommon.Norm;

namespace Norm.MedPrep
{
    public class SynNorm : INorm
    {

        private readonly List<QueryContainer> _queryContainer;
        private string _initialName;
        private readonly ElasticClient _elasticClient;

        //public SynNorm()
        //{
        //    _queryContainer = new List<QueryContainer>();
        //}

        public SynNorm(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
            _queryContainer = new List<QueryContainer>();
        }

        public List<QueryContainer> QueryContainer
        {
            get { return _queryContainer; }
        }

        public string InitialName
        {
            get { return _initialName; }
            set
            {
                _initialName = value;
                Normalize();
            }
        }

        private void Normalize()
        {
            NormResult = "";
            if (string.IsNullOrEmpty(InitialName)) return;
            _initialName = _initialName.Trim().ToLower();
            var innItems = _initialName.Split('+');

            //var musts = new List<QueryContainer>();
            //foreach (var innItem in innItems)
            //{
            //    musts.Add(Query<Syn>
            //                   .Match(m => m
            //                        .Field(f => f.Inn)
            //                        .Query(innItem))
            //                   );
            //}

            //var innList = _elasticClient.Search<Syn>(s => s
            //    .Size(100)
            //    .Query(q => q
            //        .Bool(b => b
            //            .Must(musts.ToArray())))
            //    );

            var innList = _elasticClient.Search<Syn>(s => s
                .Size(100)
                .Query(q => q
                    .Bool(b => b
                        .Must(innItems.Select(innItem => Query<Syn>.Match(m => m.Field(f => f.Inn).Query(innItem))).ToArray())))
                );

            //var innList = _elasticClient.Search<Syn>(s => s
            //    .Size(100)
            //    .Query(q => q
            //        .QueryString(qs => qs
            //            .Fields(fs => fs.Field(f => f.Inn))
            //            .Query(_initialName)))

            //);
            if (innList.Hits.Any(s => s.Source.Inn.Length == innItems.Length))
            {
                NormResult = innList.Hits
                    .Where(s => s.Source.Inn.Length == innItems.Length)
                    .Select(s => s.Source.Tn)
                    .Distinct()
                    .Aggregate((current, next) => current + "," + next)
                    ;
            }
        }

        public string NormResult { get; set; }
    }
}
