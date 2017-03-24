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
        private readonly List<QueryContainer> _queryContainer;
        private string _initialName;
        public readonly Dictionary<string, DozDetector> DozDictionary;

        public DozNorm()
        {
            _queryContainer = new List<QueryContainer>();

            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var path = new Uri($"{assemblyPath}\\Norm\\json\\{nameof(DozNorm)}.json").LocalPath;
            if (!File.Exists(path)) return;
            var json = File.ReadAllText(path);
            DozDictionary = JsonConvert.DeserializeObject<Dictionary<string, DozDetector>>(json);
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
            if (DozDictionary == null || InitialName == null) return;
            const RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
            foreach (var detect in DozDictionary)
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
                                DozValue = $"{value * 1000000}";
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
            if (string.IsNullOrEmpty(NormResult)) return;
            var detect = DozDictionary[DozKey];
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
                var valueWithRate = mutate.Rate * value;
                var strValueWithRate = valueWithRate.ToString("0.#####");
                // для целочисленных значений
                if (valueWithRate % 1 == 0)
                {
                    strValueWithRate = $"{(int)valueWithRate}";
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
    }

    public class DozDetector
    {
        public Mutate[] Mutates;

        [JsonProperty("regexp")]
        public string[] RegExpDetectors { get; set; }
    }

    public class Mutate
    {
        public decimal Rate { get; set; }

        [JsonProperty("query")]
        public string QueryString { get; set; }
    }
}