using System;
using Nest;

namespace PriceCommon.Enums
{
    public enum ProdStatus
    {
        [EnumLocalizeAttribite("не определен")]
        Undefined = -1,
       
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

    public static class ProdStatusConvert
    {
        public static ProdStatus? FromString(string strProdStatus)
        {
            return int.TryParse(strProdStatus, out var intProdStatus) ? FromInt(intProdStatus) : ProdStatus.Undefined;
        }

        public static ProdStatus? FromInt(int intProdStatus)
        {
            
            var prodStatus = (ProdStatus?)intProdStatus;
            return prodStatus;
        }
    }
}