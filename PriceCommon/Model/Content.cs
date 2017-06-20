using System;


namespace PriceCommon.Model
{
    /// <summary>
    /// Результат поиска
    /// </summary>
    public class Content
    {
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
        public string Uri { get; set; }

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
    }
}