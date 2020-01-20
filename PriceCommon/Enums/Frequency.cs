namespace PriceCommon.Enums
{
    public enum Frequency
    {
        [EnumLocalizeAttribite("Ежедневно")]
        Daily = 0,
        [EnumLocalizeAttribite("Еженедельно")]
        Weekly = 1,
        [EnumLocalizeAttribite("Ежемесячно")]
        Monthly,
        Every2Month,
        Quarterly,
        HalfYearly,
        Annually
    }
}
