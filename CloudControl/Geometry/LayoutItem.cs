using System.Drawing;
using Gma.CodeCloud.Controls.TextAnalyses.Processing;

namespace Gma.CodeCloud.Controls.Geometry
{
    public class LayoutItem
    {
        public LayoutItem(RectangleF rectangle, IWord word)
        {
            Rectangle = rectangle;
            Word = word;
        }

        public RectangleF Rectangle { get; }
        public IWord Word { get; }

        public LayoutItem Clone()
        {
            return new LayoutItem(Rectangle, Word);
        }
    }
}