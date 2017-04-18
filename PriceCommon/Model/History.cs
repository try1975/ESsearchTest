using System;

namespace PriceCommon.Model
{
    public class History : Content
    {
        public override DateTime Collected => base.Collected.Date;
        public string Idc { get; set; }
    }
}