using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Nest;
using Newtonsoft.Json;
using PriceCommon.Model;
using PriceCommon.Norm;

namespace Norm.MedPrep
{
    public class LekFormNorm : INorm
    {
        private readonly List<QueryContainer> _queryContainer;
        public readonly Dictionary<string, Detect> NormDictionary;
        private string _initialName;

        public LekFormNorm()
        {
            _queryContainer = new List<QueryContainer>();
            var uri =
                new Uri(
                    $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)}\\Norm\\json\\{nameof(LekFormNorm)}.json");
            if (!File.Exists(uri.LocalPath)) return;
            var path = uri.LocalPath;
            var json = File.ReadAllText(path);
            NormDictionary = JsonConvert.DeserializeObject<Dictionary<string, Detect>>(json);
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

        public string NormResult { get; set; }

        public List<QueryContainer> QueryContainer
        {
            get
            {
                FillContainer();
                return _queryContainer;
            }
        }

        private void Normalize()
        {
            NormResult = "";
            if (NormDictionary == null || InitialName == null) return;
            foreach (var detect in NormDictionary)
            {
                foreach (var regExpDetector in detect.Value.RegExpDetectors)
                {
                    if (
                        !Regex.IsMatch(_initialName, regExpDetector,
                            RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace)) continue;
                    NormResult = detect.Key;
                    break;
                }
                if (!string.IsNullOrEmpty(NormResult)) break;
            }
        }

        private void FillContainer()
        {
            var detect = NormDictionary[NormResult];
            _queryContainer.Clear();
            foreach (var queryString in detect.QueryStrings)
            {
                if (!string.IsNullOrEmpty(queryString))
                {
                    _queryContainer.Add(Query<Content>
                        .QueryString(q => q.Query(queryString)
                            .Fields(f => f.Field(fn => fn.Name) /*.Field(fn=>fn.Seller)*/)
                        )
                        );
                }
            }
        }
    }
}