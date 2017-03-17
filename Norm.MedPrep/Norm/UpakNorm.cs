﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Nest;
using Newtonsoft.Json;
using PriceCommon.Model;
using PriceCommon.Norm;

namespace Norm.MedPrep.Norm
{
    public class UpakNorm : INorm
    {
        private readonly List<Detect> _list;
        private readonly List<QueryContainer> _queryContainer;
        private string _initialName;

        public UpakNorm()
        {
            _queryContainer = new List<QueryContainer>();
            //_list = new List<Detect>()
            //{
            //    new Detect() {QueryStrings = new[] {"{ss}"},  RegExpDetectors = new []{@"\s[№n]([0-9]*)"}}
            //};
            //File.WriteAllText($"{nameof(UpakNorm)}.json", JsonConvert.SerializeObject(_list));

            var path =
                new Uri(
                    $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)}\\Norm\\json\\{nameof(UpakNorm)}.json")
                    .LocalPath;
            if (!File.Exists(path)) return;
            var json = File.ReadAllText(path);
            _list = JsonConvert.DeserializeObject<List<Detect>>(json);
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
            if (_list == null || InitialName == null) return;
            const RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
            foreach (var detect in _list)
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


            foreach (var item in _list)
            {
                var shoulds = new List<QueryContainer>();
                foreach (var queryString in item.QueryStrings)
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
                                .Analyzer("whitespace")
                                .Fields(f => f.Field(fn => fn.Name))
                            )
                            );
                    }

                    //_queryContainer.Add(Query<Content>
                    // .QueryString(q => q
                    //    .Query(string.Format(queryString, NormResult))
                    //    .Analyzer("name_analyzer")
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
    }
}