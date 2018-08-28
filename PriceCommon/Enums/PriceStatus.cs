namespace PriceCommon.Enums
{
    public enum PriceStatus
    {
        [EnumLocalizeAttribite("Не проверено")]
        NotChecked = 0,
        [EnumLocalizeAttribite("Проверено")]
        Checked = 1,
        [EnumLocalizeAttribite("Отбраковано")]
        Rejected = 2
    }
}