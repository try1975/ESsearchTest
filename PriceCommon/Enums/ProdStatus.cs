namespace PriceCommon.Enums
{
    public enum ProdStatus
    {
        [EnumLocalizeAttribite("в корзину")]
        IntoTheBasket = 1,

        [EnumLocalizeAttribite("нет в наличии")]
        OutOfStock = 2,

        [EnumLocalizeAttribite("нет на складе")]
        OutInWarehouse = 3,

        [EnumLocalizeAttribite("отсутствует в продаже")]
        OutInSale= 4,

        [EnumLocalizeAttribite("под заказ")]
        OnRequest= 5,

        [EnumLocalizeAttribite("есть в наличии")]
        InStock= 6
    }
}