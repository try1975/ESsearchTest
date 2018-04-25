using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Gma.CodeCloud.Controls.Geometry;
using Gma.CodeCloud.Controls.TextAnalyses.Processing;

namespace Gma.CodeCloud.Controls
{
    public class CloudControl : Panel
    {
        private readonly Color[] _mDefaultPalette =
        {
            Color.DarkRed, Color.DarkBlue, Color.DarkGreen, Color.Navy, Color.DarkCyan, Color.DarkOrange,
            Color.DarkGoldenrod, Color.DarkKhaki, Color.Blue, Color.Red, Color.Green
        };

        private Color _mBackColor;
        private LayoutItem _mItemUderMouse;
        private ILayout _mLayout;
        private LayoutType _mLayoutType;

        private int _mMaxFontSize;
        private int _mMaxWordWeight;
        private int _mMinFontSize;
        private int _mMinWordWeight;
        private Color[] _mPalette;
        private IEnumerable<IWord> _mWords;

        public CloudControl()
        {
            _mMinWordWeight = 0;
            _mMaxWordWeight = 0;

            MaxFontSize = 68;
            MinFontSize = 6;

            BorderStyle = BorderStyle.FixedSingle;
            ResizeRedraw = true;

            _mPalette = _mDefaultPalette;
            _mBackColor = Color.White;
            _mLayoutType = LayoutType.Spiral;
        }

        public LayoutType LayoutType
        {
            get { return _mLayoutType; }
            set
            {
                if (value == _mLayoutType)
                    return;

                _mLayoutType = value;
                BuildLayout();
                Invalidate();
            }
        }


        public override Color BackColor
        {
            get { return _mBackColor; }
            set
            {
                if (_mBackColor == value)
                    return;
                _mBackColor = value;
                Invalidate();
            }
        }

        public int MaxFontSize
        {
            get { return _mMaxFontSize; }
            set
            {
                _mMaxFontSize = value;
                BuildLayout();
                Invalidate();
            }
        }

        public int MinFontSize
        {
            get { return _mMinFontSize; }
            set
            {
                _mMinFontSize = value;
                BuildLayout();
                Invalidate();
            }
        }

        public Color[] Palette
        {
            get { return _mPalette; }
            set
            {
                _mPalette = value;
                BuildLayout();
                Invalidate();
            }
        }

        public IEnumerable<IWord> WeightedWords
        {
            get { return _mWords; }
            set
            {
                _mWords = value;
                if (value == null) return;

                var first = _mWords.First();
                if (first != null)
                {
                    _mMaxWordWeight = first.Occurrences;
                    _mMinWordWeight = _mWords.Last().Occurrences;
                }

                BuildLayout();
                Invalidate();
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_mWords == null) return;
            if (_mLayout == null) return;

            var wordsToRedraw = _mLayout.GetWordsInArea(e.ClipRectangle);
            using (var graphics = e.Graphics)
            using (IGraphicEngine graphicEngine =
                new GdiGraphicEngine(graphics, Font.FontFamily, FontStyle.Regular, _mPalette, MinFontSize, MaxFontSize,
                    _mMinWordWeight, _mMaxWordWeight))
            {
                foreach (var currentItem in wordsToRedraw)
                    if (_mItemUderMouse == currentItem)
                        graphicEngine.DrawEmphasized(currentItem);
                    else
                        graphicEngine.Draw(currentItem);
            }
        }

        private void BuildLayout()
        {
            if (_mWords == null) return;

            using (var graphics = CreateGraphics())
            {
                IGraphicEngine graphicEngine =
                    new GdiGraphicEngine(graphics, Font.FontFamily, FontStyle.Regular, _mPalette, MinFontSize,
                        MaxFontSize, _mMinWordWeight, _mMaxWordWeight);
                _mLayout = LayoutFactory.CrateLayout(_mLayoutType, Size);
                _mLayout.Arrange(_mWords, graphicEngine);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            LayoutItem nextItemUnderMouse;
            var mousePositionRelativeToControl = PointToClient(new Point(MousePosition.X, MousePosition.Y));
            TryGetItemAtLocation(mousePositionRelativeToControl, out nextItemUnderMouse);
            if (nextItemUnderMouse != _mItemUderMouse)
            {
                if (nextItemUnderMouse != null)
                {
                    var newRectangleToInvalidate = RectangleGrow(nextItemUnderMouse.Rectangle, 6);
                    Invalidate(newRectangleToInvalidate);
                }
                if (_mItemUderMouse != null)
                {
                    var prevRectangleToInvalidate = RectangleGrow(_mItemUderMouse.Rectangle, 6);
                    Invalidate(prevRectangleToInvalidate);
                }
                _mItemUderMouse = nextItemUnderMouse;
            }
            base.OnMouseMove(e);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            BuildLayout();
            base.OnResize(eventargs);
        }

        private static Rectangle RectangleGrow(RectangleF original, int growByPixels)
        {
            return new Rectangle(
                (int) (original.X - growByPixels),
                (int) (original.Y - growByPixels),
                (int) (original.Width + growByPixels + 1),
                (int) (original.Height + growByPixels + 1));
        }

        public IEnumerable<LayoutItem> GetItemsInArea(RectangleF area)
        {
            if (_mLayout == null)
                return new LayoutItem[] { };

            return _mLayout.GetWordsInArea(area);
        }

        public bool TryGetItemAtLocation(Point location, out LayoutItem foundItem)
        {
            foundItem = null;
            var itemsInArea = GetItemsInArea(new RectangleF(location, new SizeF(0, 0)));
            foreach (var item in itemsInArea)
            {
                foundItem = item;
                return true;
            }
            return false;
        }
    }
}