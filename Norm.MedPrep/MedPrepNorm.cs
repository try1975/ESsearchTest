using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Nest;
using PriceCommon.Model;
using PriceCommon.Norm;

namespace Norm.MedPrep
{
    public class MedPrepNorm : INorm, IMedPrep
    {
        private readonly IMedPrepControl _controlMedPrep;
        private readonly List<QueryContainer> _queryContainer;
        public readonly DozNorm DozNorm;

        public readonly LekFormNorm LekFormNorm;
        public readonly UpakNorm UpakNorm;
        private readonly SynNorm _synNorm;
        private string _doz;
        private string _dozEd;
        private string _firstWords;
        private string _initialName;
        private string _lekForm;
        private string _upak;
        private string _syn;
        private string _synInitial;

        public MedPrepNorm(ElasticClient elasticClient)
        {
            _queryContainer = new List<QueryContainer>();
            LekFormNorm = new LekFormNorm();
            UpakNorm = new UpakNorm();
            DozNorm = new DozNorm();
            _synNorm = new SynNorm(elasticClient);
        }

        public MedPrepNorm(IMedPrepControl controlMedPrep, ElasticClient elasticClient)
        {
            _controlMedPrep = controlMedPrep;
            _queryContainer = new List<QueryContainer>();
            LekFormNorm = new LekFormNorm();

            if (LekFormNorm.NormDictionary != null && _controlMedPrep != null)
            {
                _controlMedPrep.LekFormList = LekFormNorm.NormDictionary.Select(i => i.Key).ToList();
            }

            UpakNorm = new UpakNorm();
            DozNorm = new DozNorm();
            if (DozNorm.DozDictionary != null && _controlMedPrep != null)
            {
                _controlMedPrep.DozEdList = DozNorm.DozDictionary.Select(i => i.Key).ToList();
            }

            _synNorm = new SynNorm(elasticClient);
        }

        public string FirstWords
        {
            get { return _firstWords; }
            set
            {
                if (_firstWords == value) return;
                _firstWords = value;
                if (_controlMedPrep != null) _controlMedPrep.FirstWords = _firstWords;
            }
        }

        public string LekForm
        {
            get { return _lekForm; }
            set
            {
                _lekForm = value;
                if (_controlMedPrep != null) _controlMedPrep.LekForm = LekFormNorm.NormResult;
            }
        }

        public string Doz
        {
            get { return _doz; }
            set
            {
                _doz = value;
                if (_controlMedPrep != null) _controlMedPrep.Doz = DozNorm.DozValue;
            }
        }

        public string DozEd
        {
            get { return _dozEd; }
            set
            {
                _dozEd = value;
                if (_controlMedPrep != null) _controlMedPrep.DozEd = DozNorm.DozKey;
            }
        }

        public string Upak
        {
            get { return _upak; }
            set
            {
                _upak = value;
                if (_controlMedPrep != null) _controlMedPrep.Upak = UpakNorm.NormResult;
            }
        }

        public string Ob { get; set; }

        public string Syn
        {
            get { return _syn; }
            set
            {
                _syn = value;
                if (_controlMedPrep != null && _synNorm != null) _controlMedPrep.Syn = _synNorm.NormResult;
            }

        }

        public string InitialName
        {
            get { return _initialName; }
            set
            {
                _initialName = value;
                if (_initialName != null)
                {
                    FirstWords = GetFirstWords(_initialName);
                }


                LekFormNorm.InitialName = _initialName;
                LekForm = LekFormNorm.NormResult;
                UpakNorm.InitialName = _initialName;
                Upak = UpakNorm.NormResult;
                DozNorm.InitialName = _initialName;
                Doz = DozNorm.DozValue;
                DozEd = DozNorm.DozKey;
                if (_synNorm == null) return;
                _synInitial = GetSynInitial();
                _synNorm.InitialName = _synInitial;
                Syn = _synNorm.NormResult;
            }
        }

        private string GetFirstWords(string input)
        {
            const RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
            const string exprRegex = @"\b([\w-]+)\b";
            // применять нормализаторы, чтобы определить FirstWords
            var words = Regex.Matches(input, exprRegex, options)
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList();
            var aFirstWords = "";
            if (!words.Any()) return aFirstWords;
            var tempFirstWords = "";
            foreach (var word in words)
            {
                tempFirstWords += $" {word}";
                LekFormNorm.InitialName = tempFirstWords;
                UpakNorm.InitialName = tempFirstWords;
                DozNorm.InitialName = tempFirstWords;
                if (LekFormNorm.NormResult.Equals("-")
                    && string.IsNullOrEmpty(UpakNorm.NormResult)
                    && DozNorm.DozKey.Equals("-")
                    && char.IsLetter(word[0]))
                {
                    aFirstWords = tempFirstWords;
                }
                else
                {
                    break;
                }
            }
            return aFirstWords.Trim();
        }

        private string GetSynInitial()
        {
            if (!_initialName.Contains('+')) return FirstWords;
            var innList = _initialName.Split('+');
            var i = 0;
            foreach (var inn in innList)
            {
                innList[i] = GetFirstWords(inn).ToLower().Trim();
                i++;
            }
            return string.Join("+", innList);
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

        private void FillContainer()
        {
            _queryContainer.Clear();

            if (!string.IsNullOrEmpty(FirstWords))
            {
                if (!string.IsNullOrEmpty(_controlMedPrep?.Syn)) Syn = _controlMedPrep?.Syn;
                if (!string.IsNullOrEmpty(Syn))
                {
                    var shoulds = new List<QueryContainer>();
                    foreach (var queryString in Syn.Split(','))
                    {
                        var should = queryString;
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
                                .QueryString(m => m
                                    .Query(should + "*")
                                    .Fields(f => f.Field(fn => fn.Name))
                                )
                                );
                        }
                    }
                    // это убрать?
                    //shoulds.Add(Query<Content>
                    //    .QueryString(q => q.Query(FirstWords.Trim().ToLower() + "*")
                    //        .Fields(f => f.Field(fn => fn.Name))
                    //    )
                    //    );
                    _queryContainer.Add(Query<Content>
                    .Bool(w => w
                        .Should(shoulds.ToArray())
                        .MinimumShouldMatch(1)
                        )
                    );
                }
                else
                {
                    _queryContainer.Add(Query<Content>
                        .QueryString(q => q.Query(FirstWords.Trim().ToLower() + "*")
                            .Fields(f => f.Field(fn => fn.Name) /*.Field(fn=>fn.Seller)*/)
                        )
                        );
                }
            }
            _queryContainer.AddRange(LekFormNorm.QueryContainer);
            _queryContainer.AddRange(UpakNorm.QueryContainer);
            _queryContainer.AddRange(DozNorm.QueryContainer);
        }
    }
}