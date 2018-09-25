using System;


namespace PriceCommon.Model
{
    /// <summary>
    /// Результат поиска
    /// </summary>
    public class Content
    {
        private string _uri;
        public bool Selected { get; set; }

        /// <summary>
        /// Наименование позиции
        /// </summary>
        public string Name { get; set; }

        //[JsonIgnore]
        /// <summary>
        /// Цена позиции
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// Цена позиции
        /// </summary>
        public double Nprice => Convert.ToDouble(Price);

        /// <summary>
        /// Ссылка на источник
        /// </summary>
        public string Uri
        {
            get { return _uri; }
            set
            {
                _uri = value;
                if (!_uri.StartsWith("http")) Uri = $"http://zakupki.gov.ru/epz/contract/contractCard/payment-info-and-target-of-order.html?reestrNumber={_uri}";
            }
        }

        /// <summary>
        /// Продавец
        /// </summary>
        public string Seller { get; set; }

        /// <summary>
        /// Дата и время сбора цены (UTC)
        /// </summary>
        public long? CollectedAt { get; set; }

        /// <summary>
        /// Дата и время сбора цены
        /// </summary>
        public virtual DateTime Collected => new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Convert.ToDouble(CollectedAt))/*.Date*/;

        /// <summary>
        /// Идентификатор позиции
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Производитель
        /// </summary>
        public string Producer { get; set; }

        /// <summary>
        ///     Телефоны
        /// </summary>
        public string Phones { get; set; }

        /// <summary>
        ///     ОКПД2
        /// </summary>
        public string Okpd2 { get; set; }

        /// <summary>
        ///     Единица измерения
        /// </summary>
        public string Okei { get; set; }

        /// <summary>
        ///     Валюта
        /// </summary>
        public string Currency { get; set; }

        public string prodStatus { get; set; }

    }
}