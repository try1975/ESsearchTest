using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Gma.CodeCloud.Controls.Geometry;

namespace Gma.CodeCloud.Controls
{
    public class GdiGraphicEngine : IGraphicEngine, IDisposable
    {
        private readonly Graphics _mGraphics;
        private readonly int _mMaxWordWeight;

        private readonly int _mMinWordWeight;
        private Font _mLastUsedFont;

        public GdiGraphicEngine(Graphics graphics, FontFamily fontFamily, FontStyle fontStyle, Color[] palette,
            float minFontSize, float maxFontSize, int minWordWeight, int maxWordWeight)
        {
            _mMinWordWeight = minWordWeight;
            _mMaxWordWeight = maxWordWeight;
            _mGraphics = graphics;
            FontFamily = fontFamily;
            FontStyle = fontStyle;
            Palette = palette;
            MinFontSize = minFontSize;
            MaxFontSize = maxFontSize;
            _mLastUsedFont = new Font(FontFamily, maxFontSize, FontStyle);
            _mGraphics.SmoothingMode = SmoothingMode.AntiAlias;
        }

        public FontFamily FontFamily { get; set; }
        public FontStyle FontStyle { get; set; }
        public Color[] Palette { get; }
        public float MinFontSize { get; set; }
        public float MaxFontSize { get; set; }

        public SizeF Measure(string text, int weight)
        {
            var font = GetFont(weight);
            //return m_Graphics.MeasureString(text, font);
            return TextRenderer.MeasureText(_mGraphics, text, font);
        }

        public void Draw(LayoutItem layoutItem)
        {
            var font = GetFont(layoutItem.Word.Occurrences);
            var color = GetPresudoRandomColorFromPalette(layoutItem);
            //m_Graphics.DrawString(layoutItem.Word, font, brush, layoutItem.Rectangle);
            var point = new Point((int) layoutItem.Rectangle.X, (int) layoutItem.Rectangle.Y);
            TextRenderer.DrawText(_mGraphics, layoutItem.Word.Text, font, point, color);
        }

        public void DrawEmphasized(LayoutItem layoutItem)
        {
            var font = GetFont(layoutItem.Word.Occurrences);
            var color = GetPresudoRandomColorFromPalette(layoutItem);
            //m_Graphics.DrawString(layoutItem.Word, font, brush, layoutItem.Rectangle);
            var point = new Point((int) layoutItem.Rectangle.X, (int) layoutItem.Rectangle.Y);
            TextRenderer.DrawText(_mGraphics, layoutItem.Word.Text, font, point, Color.LightGray);
            var offset = (int) (5 * font.Size / MaxFontSize) + 1;
            point.Offset(-offset, -offset);
            TextRenderer.DrawText(_mGraphics, layoutItem.Word.Text, font, point, color);
        }

        public void Dispose()
        {
            _mGraphics.Dispose();
        }

        private Font GetFont(int weight)
        {
            var fontSize =
                (float) (weight - _mMinWordWeight) / (_mMaxWordWeight - _mMinWordWeight) * (MaxFontSize - MinFontSize) +
                MinFontSize;
            if (!float.IsNaN(fontSize) && _mLastUsedFont.Size != fontSize)
                _mLastUsedFont = new Font(FontFamily, fontSize, FontStyle);
            return _mLastUsedFont;
        }

        private Color GetPresudoRandomColorFromPalette(LayoutItem layoutItem)
        {
            var color = Palette[layoutItem.Word.Occurrences * layoutItem.Word.Text.Length % Palette.Length];
            return color;
        }
    }
}