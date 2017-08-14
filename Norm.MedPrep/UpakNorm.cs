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
    public class UpakNorm : INorm
    {
        private readonly List<IDetect> _detects;

        private readonly string _pathPrefix =
            $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)}\\Norm\\json\\{nameof(UpakNorm)}";

        private readonly List<QueryContainer> _queryContainer;
        private string _initialName;
        

        public UpakNorm(string normNumber = "")
        {
            _queryContainer = new List<QueryContainer>();
            // получить из кэша json, если нет - загрузить из файла
            string json;
            var cacheName = GetCacheName(normNumber);
            if (MedPrepCache.Detects.TryGetValue(cacheName, out json))
            {
                _detects = new List<IDetect>(JsonConvert.DeserializeObject<List<Detect>>(json));
                return;
            }
            var path = new Uri($"{_pathPrefix}{normNumber}.json").LocalPath;
            if (!File.Exists(path)) path = new Uri($"{_pathPrefix}.json").LocalPath;
            if (!File.Exists(path)) return;
            json = File.ReadAllText(path);
            if (string.IsNullOrEmpty(json)) return;
            MedPrepCache.Detects[cacheName] = json;
            _detects = new List<IDetect>(JsonConvert.DeserializeObject<List<Detect>>(json));
        }

        private string GetCacheName(string normNumber)
        {
            return $"{nameof(UpakNorm)}{nameof(_detects)}{normNumber}";
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
            const RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
            foreach (var detect in _detects)
            {
                foreach (var detector in detect.RegExpDetectors)
                {
                    if (!Regex.IsMatch(_initialName, detector, options)) continue;
                    NormResult = Regex.Match(_initialName, detector, options).Groups[1].Value;
                    break;
                }
                if (!string.IsNullOrEmpty(NormResult)) break;
            }
        }

        private void FillContainer()
        {
            _queryContainer.Clear();
            if (string.IsNullOrEmpty(NormResult)) return;


            foreach (var detect in _detects)
            {
                var shoulds = new List<QueryContainer>();
                foreach (var queryString in detect.QueryStrings)
                {
                    var should = string.Format(queryString, NormResult);
                    if (should.Contains(" "))
                    {
                        shoulds.Add(Query<Content>
                            .MatchPhrase(m => m
                                .Field(f => f.Name)
                                .Query(should))
                            );
                    }
                    else
                    {
                        //shoulds.Add(Query<Content>
                        //    .Match(m => m
                        //        .Field(f => f.Name)
                        //        .Query(should)
                        //        )
                        //    );
                        shoulds.Add(Query<Content>
                            .QueryString(m => m
                                .Query(should + "*")
                                .Fields(f => f.Field(fn => fn.Name))
                            )
                            );
                    }

                    //_queryContainer.Add(Query<Content>
                    // .QueryString(q => q
                    //    .Query(string.Format(queryString, NormResult))
                    //    .Fields(f=>f.Field(fn=>fn.Name)/*.Field(fn=>fn.Seller)*/))
                    // );
                }
                if (shoulds.Count == 0) continue;
                _queryContainer.Add(Query<Content>
                    .Bool(w => w
                        .Should(shoulds.ToArray())
                        .MinimumShouldMatch(1)
                    )
                    );
            }
        }

        public List<IDetect> GetDetects()
        {
            return _detects;
        }

        public bool CreateDetects(string normNumber, List<Detect> detects)
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