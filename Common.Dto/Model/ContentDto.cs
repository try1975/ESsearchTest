using System;
using Newtonsoft.Json;

namespace Common.Dto.Model
{
    /// <summary>
    ///     Результат поиска
    /// </summary>
    public class ContentDto
    {

        [JsonIgnore]
        public bool Selected { get; set; }

        /// <summary>
        ///     Наименование позиции
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        ///     Цена позиции
        /// </summary>
        [JsonIgnore]
        public string Price { get; set; }

        /// <summary>
        ///     Цена позиции
        /// </summary>
        public double Nprice
        {
            get {
                try
                {
                    return Convert.ToDouble(Price);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                return 0.00;
            }
        }

        /// <summary>
        ///     Ссылка на источник
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        ///     Продавец
        /// </summary>
        public string Seller { get; set; }

        /// <summary>
        ///     Дата и время сбора цены (UTC)
        /// </summary>
        public long? CollectedAt { get; set; }

        /// <summary>
        ///     Дата и время сбора цены
        /// </summary>
        public DateTime Collected
        {
            get { return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Convert.ToDouble(CollectedAt)) /*.Date*/; }
        }

        /// <summary>
        ///     Идентификатор позиции
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Производитель
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
    }
}