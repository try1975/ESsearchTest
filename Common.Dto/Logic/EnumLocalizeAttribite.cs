using System;

namespace Common.Dto.Logic
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