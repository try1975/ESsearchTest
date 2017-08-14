using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private readonly Dictionary<string, Detect> _detects;

        private readonly string _pathPrefix =
            $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)}\\Norm\\json\\{nameof(LekFormNorm)}";

        private readonly List<QueryContainer> _queryContainer;
        private string _initialName;

        public LekFormNorm(string normNumber = "")
        {
            _queryContainer = new List<QueryContainer>();
            // получить из кэша json, если нет - загрузить из файла
            string json;
            var cacheName = GetCacheName(normNumber);
            if (MedPrepCache.Detects.TryGetValue(cacheName, out json))
            {
                _detects = JsonConvert.DeserializeObject<Dictionary<string, Detect>>(json);
                return;
            }
            var path = new Uri($"{_pathPrefix}{normNumber}.json").LocalPath;
            if (!File.Exists(path)) path = new Uri($"{_pathPrefix}.json").LocalPath;
            if (!File.Exists(path)) return;
            json = File.ReadAllText(path);
            if (string.IsNullOrEmpty(json)) return;
            MedPrepCache.Detects[cacheName] = json;
            _detects = JsonConvert.DeserializeObject<Dictionary<string, Detect>>(json);
        }

        private string GetCacheName(string normNumber)
        {
            return $"{nameof(LekFormNorm)}{nameof(_detects)}{normNumber}";
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
            if (_detects == null || InitialName == null) return;
            foreach (var detect in _detects)
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
            if (string.IsNullOrEmpty(NormResult)) return;
            //var detect = Detects[NormResult];
            var detect =
                _detects.FirstOrDefault(d => d.Key.Equals(NormResult, StringComparison.InvariantCultureIgnoreCase))
                    .Value;
            _queryContainer.Clear();
            if (detect == null) return;
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

        public Dictionary<string, Detect> GetDetects()
        {
            return _detects;
        }

        public bool CreateDetects(string normNumber, Dictionary<string, Detect> detects)
        {
            if (string.IsNullOrEmpty(normNumber)) return false;
            var path = new Uri($"{_pathPrefix}{normNumber}.json").LocalPath;
            try
            {
                var json = JsonConvert.SerializeObject(detects);
                File.WriteAllText(path, json);
                MedPrepCache.Detects[GetCacheName(normNumber)] = json;
                return true;
            }
            catch
            {
                // ignored
            }
            return false;
        }

        public bool DeleteDetects(string normNumber)
        {
            if (string.IsNullOrEmpty(normNumber)) return false;
            var path = new Uri($"{_pathPrefix}{normNumber}.json").LocalPath;
            try
            {
                File.Delete(path);
                MedPrepCache.Detects.Remove(GetCacheName(normNumber));
                return true;
            }
            catch
            {
                // ignored
            }
            return false;
        }
    }
}