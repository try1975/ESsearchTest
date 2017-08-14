using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Nest;
using Newtonsoft.Json;
using PriceCommon.Model;
using PriceCommon.Norm;

namespace Norm.MedPrep
{
    public class DozNorm : INorm
    {
        private readonly Dictionary<string, DozDetector> _detects;

        private readonly string _pathPrefix =
            $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)}\\Norm\\json\\{nameof(DozNorm)}";

        private readonly List<QueryContainer> _queryContainer;
        private string _initialName;

        public DozNorm(string normNumber = "")
        {
            _queryContainer = new List<QueryContainer>();
            // получить из кэша json, если нет - загрузить из файла
            string json;
            var cacheName = GetCacheName(normNumber);
            if (MedPrepCache.Detects.TryGetValue(cacheName, out json))
            {
                _detects = JsonConvert.DeserializeObject<Dictionary<string, DozDetector>>(json);
                return;
            }
            var path = new Uri($"{_pathPrefix}{normNumber}.json").LocalPath;
            if (!File.Exists(path)) path = new Uri($"{_pathPrefix}.json").LocalPath;
            if (!File.Exists(path)) return;
            json = File.ReadAllText(path);
            if (string.IsNullOrEmpty(json)) return;
            MedPrepCache.Detects[cacheName] = json;
            _detects = JsonConvert.DeserializeObject<Dictionary<string, DozDetector>>(json);
        }

        private string GetCacheName(string normNumber)
        {
            return $"{nameof(DozNorm)}{nameof(_detects)}{normNumber}";
        }

        public string DozKey { get; set; }
        public string DozValue { get; set; }

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
                foreach (var detector in detect.Value.RegExpDetectors)
                {
                    if (!Regex.IsMatch(_initialName, detector, options)) continue;
                    DozKey = detect.Key;
                    var match = Regex.Match(_initialName, detector, options);
                    DozValue = match.Groups[1].Value;
                    if (match.Groups.Count > 1)
                    {
                        try
                        {
                            var value = Convert.ToDecimal(DozValue);
                            var modifier = match.Groups[2].Value.Trim().ToLower();
                            // [0-9]млн МЕ
                            if (modifier.Contains("млн"))
                            {
                                DozValue = $"{value*1000000}";
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e);
                            //throw;
                        }
                    }

                    NormResult = $"{DozValue}{DozKey}";
                    break;
                }
                if (!string.IsNullOrEmpty(NormResult)) break;
            }
        }

        private void FillContainer()
        {
            _queryContainer.Clear();
            if (string.IsNullOrEmpty(NormResult) || NormResult == "-") return;
            var detect = _detects[DozKey];
            var value = 0.0M;

            try
            {
                DozValue = DozValue.Replace('.', ',');
                value = Convert.ToDecimal(DozValue);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //throw;
            }
            var shoulds = new List<QueryContainer>();
            foreach (var mutate in detect.Mutates)
            {
                if (mutate.Rate == 0) return;
                var valueWithRate = mutate.Rate*value;
                var strValueWithRate = valueWithRate.ToString("0.#####");
                // для целочисленных значений
                if (valueWithRate%1 == 0)
                {
                    strValueWithRate = $"{(int) valueWithRate}";
                }
                var should = string.Format(mutate.QueryString, strValueWithRate);
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
                    //        .Query(should))
                    //    );
                    shoulds.Add(Query<Content>
                        .QueryString(m => m
                            .Query(should + "*")
                            .Fields(f => f.Field(fn => fn.Name))
                        ));
                }
            }
            if (shoulds.Count == 0) return;
            _queryContainer.Add(Query<Content>
                .Bool(w => w
                    .Should(shoulds.ToArray())
                    .MinimumShouldMatch(1)
                )
                );
        }

        public Dictionary<string, DozDetector> GetDetects()
        {
            return _detects;
        }

        public bool CreateDetects(string normNumber, Dictionary<string, DozDetector> detects)
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