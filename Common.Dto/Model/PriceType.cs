using Common.Dto.Logic;

namespace Common.Dto.Model
{
    public enum PriceType
    {
        [EnumLocalizeAttribite("Доверять")]
        Trusted,
        [EnumLocalizeAttribite("Требует проверки")]
        Check
    }
}