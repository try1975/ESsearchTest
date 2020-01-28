namespace PriceCommon.Enums
{
    public enum Frequency
    {
        [EnumLocalizeAttribite("Каждый день")]
        Daily = 0,
        [EnumLocalizeAttribite("Каждую неделю")]
        Weekly = 1,
        [EnumLocalizeAttribite("Каждый месяц")]
        Monthly = 2,
        [EnumLocalizeAttribite("Каждые два месяця")]
        Every2Month = 3,
        [EnumLocalizeAttribite("Каждый квартал")]
        Quarterly = 4,
        [EnumLocalizeAttribite("Каждые полгода")]
        HalfYearly = 5,
        [EnumLocalizeAttribite("Каждый год")]
        Annually = 6
    }
}
