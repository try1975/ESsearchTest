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

namespace Norm.MedPrep.Norm
{
    public class DozNorm : INorm
    {
        private readonly List<QueryContainer> _queryContainer;
        private string _initialName;
        public Dictionary<string, DozDetector> NormDictionary;

        public DozNorm()
        {
            _queryContainer = new List<QueryContainer>();
            var path =
                new Uri(
                    $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)}\\Norm\\json\\{nameof(DozNorm)}.json")
                    .LocalPath;
            if (!File.Exists(path)) return;
            var json = File.ReadAllText(path);
            NormDictionary = JsonConvert.DeserializeObject<Dictionary<string, DozDetector>>(json);
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
            if (NormDictionary == null || InitialName == null) return;
            const RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
            foreach (var detect in NormDictionary)
            {
                foreach (var regExpDetector in detect.Value.RegExpDetectors)
                {
                    if (!Regex.IsMatch(_initialName, regExpDetector, options)) continue;
                    DozKey = detect.Key;
                    var match = Regex.Match(_initialName, regExpDetector, options);
                    DozValue = match.Groups[1].Value;
                    if (match.Groups.Count > 1)
                    {
                        try
                        {
                            var value = Convert.ToDecimal(DozValue);
                            var modifier = match.Groups[2].Value.Trim().ToLower();
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
            if (string.IsNullOrEmpty(NormResult)) return;
            var detect = NormDictionary[DozKey];
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
            foreach (var edizmPerevod in detect.Mutates)
            {
                if (edizmPerevod.Koef == 0) return;
                // смотреть, целочисленные ли значения
                // сделать bool should math match_phrase
                var valueWithKoef = edizmPerevod.Koef*value;
                var strValueWithKoef = valueWithKoef.ToString("0.#####");
                if (valueWithKoef%1 == 0)
                {
                    strValueWithKoef = $"{(int) valueWithKoef}";
                }
                var should = string.Format(edizmPerevod.QueryString, strValueWithKoef);
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
                    shoulds.Add(Query<Content>
                        .Match(m => m
                            .Field(f => f.Name)
                            .Query(should))
                        );
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
        public EdizmPerevod[] Mutates;

        [JsonProperty("regexp")]
        public string[] RegExpDetectors { get; set; }
    }

    public class EdizmPerevod
    {
        public decimal Koef { get; set; }

        [JsonProperty("query")]
        public string QueryString { get; set; }
    }
}