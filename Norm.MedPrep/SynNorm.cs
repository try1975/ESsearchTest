using System.Collections.Generic;
using System.Linq;
using Nest;
using PriceCommon.Norm;

namespace Norm.MedPrep
{
    public class SynNorm : INorm
    {

        private readonly List<QueryContainer> _queryContainer;
        private string _initialName;
        private readonly List<string> _list01, _list02, _list03, _list04;

        public SynNorm()
        {
            _queryContainer = new List<QueryContainer>();
            _list01 = new List<string> { "эзомепразол", "нео-зекст", "эманера", "нексиум" };
            _list02 = new List<string> { "панкреатин", "креон", "микразим", "пангрол", "эрмиталь" };
            _list03 = new List<string> { "инсулин", "биосулин", "возулим", "генсулин", "инсуман", "ринсулин" };
            _list04 = new List<string> { "орлистат", "алли", "ксеналтен" };

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
            if (InitialName == null) return;
            _initialName = _initialName.Trim().ToLower();
            if (_list01.Contains(_initialName))
            {
                NormResult = string.Join(",", _list01.Where(z => !z.Equals(_initialName)).ToList());
                return;
            }
            if (_list02.Contains(_initialName))
            {
                NormResult = string.Join(",", _list02.Where(z => !z.Equals(_initialName)).ToList());
                return;
            }
            if (_list03.Contains(_initialName))
            {
                NormResult = string.Join(",", _list03.Where(z => !z.Equals(_initialName)).ToList());
                return;
            }
            if (_list04.Contains(_initialName))
            {
                NormResult = string.Join(",", _list04.Where(z => !z.Equals(_initialName)).ToList());
                return;
            }
        }

        public string NormResult { get; set; }
    }
}
