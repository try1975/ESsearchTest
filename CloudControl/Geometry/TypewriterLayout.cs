using System.Drawing;

namespace Gma.CodeCloud.Controls.Geometry
{
    public class TypewriterLayout : BaseLayout
    {
        private PointF _mCarret;
        private float _mLineHeight;

        public TypewriterLayout(SizeF size) : base(size)
        {
            _mCarret = new PointF(size.Width, 0);
        }

        public override bool TryFindFreeRectangle(SizeF size, out RectangleF foundRectangle)
        {
            foundRectangle = new RectangleF(_mCarret, size);
            if (HorizontalOverflow(foundRectangle))
            {
                foundRectangle = LineFeed(foundRectangle);
                if (!IsInsideSurface(foundRectangle))
                    return false;
            }
            _mCarret = new PointF(foundRectangle.Right, foundRectangle.Y);

            return true;
        }

        private RectangleF LineFeed(RectangleF rectangle)
        {
            var result = new RectangleF(new PointF(0, _mCarret.Y + _mLineHeight), rectangle.Size);
            _mLineHeight = rectangle.Height;
            return result;
        }

        private bool HorizontalOverflow(RectangleF rectangle)
        {
            return rectangle.Right > Surface.Right;
        }
    }
}