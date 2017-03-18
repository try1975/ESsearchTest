using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Nest;
using PriceCommon.Model;
using PriceCommon.Norm;

namespace Norm.MedPrep.Norm
{
    public class MedPrepNorm : INorm, IMedPrep
    {
        private readonly IMedPrepControl _controlMedPrep;
        private readonly List<QueryContainer> _queryContainer;
        public readonly DozNorm DozNorm;

        public readonly LekFormNorm LekFormNorm;
        public readonly UpakNorm UpakNorm;
        private string _doz;
        private string _dozEd;
        private string _firstWords;
        private string _initialName;
        private string _lekForm;
        private string _upak;

        public MedPrepNorm()
        {
            _queryContainer = new List<QueryContainer>();
            LekFormNorm = new LekFormNorm();
            UpakNorm = new UpakNorm();
            DozNorm = new DozNorm();
        }

        public MedPrepNorm(IMedPrepControl controlMedPrep)
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
            if (DozNorm.NormDictionary != null && _controlMedPrep != null)
            {
                _controlMedPrep.DozEdList = DozNorm.NormDictionary.Select(i => i.Key).ToList();
            }
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

        public string InitialName
        {
            get { return _initialName; }
            set
            {
                _initialName = value;
                if (_initialName != null)
                {
                    const RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace;
                    const string exprRegex = @"\b([\w-]+)\b";
                    if (Regex.IsMatch(_initialName, exprRegex, options))
                        FirstWords = Regex.Match(_initialName, exprRegex, options).Groups[1].Value;
                }

                LekFormNorm.InitialName = _initialName;
                LekForm = LekFormNorm.NormResult;
                UpakNorm.InitialName = _initialName;
                Upak = UpakNorm.NormResult;
                DozNorm.InitialName = _initialName;
                Doz = DozNorm.DozValue;
                DozEd = DozNorm.DozKey;
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

        private void FillContainer()
        {
            _queryContainer.Clear();

            if (!string.IsNullOrEmpty(FirstWords))
            {
                _queryContainer.Add(Query<Content>
                    .QueryString(q => q.Query(FirstWords.Trim().ToLower())
                        .Fields(f => f.Field(fn => fn.Name) /*.Field(fn=>fn.Seller)*/)
                    )
                    );
            }
            _queryContainer.AddRange(LekFormNorm.QueryContainer);
            _queryContainer.AddRange(UpakNorm.QueryContainer);
            _queryContainer.AddRange(DozNorm.QueryContainer);
        }
    }
}