using System;

namespace PriceCommon.Enums
{
    public class EnumLocalizeAttribite : Attribute
    {
        public readonly string Text;

        public EnumLocalizeAttribite(string text)
        {
            Text = text;
        }

    }
}