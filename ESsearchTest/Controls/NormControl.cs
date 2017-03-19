using System.Collections.Generic;
using System.Windows.Forms;
using Nest;
using PriceCommon.Norm;

namespace ESsearchTest.Controls
{
    public partial class NormControl : UserControl, INorm
    {
        public NormControl()
        {
            InitializeComponent();
            QueryContainer = new List<QueryContainer>();
        }

        public List<QueryContainer> QueryContainer { get; }
        public string InitialName { get; set; }
        public string NormResult { get; set; }
    }
}
