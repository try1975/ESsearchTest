using System;

namespace Price.WebApi.Models
{
    public class FetcheeDto
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Адрес страницы товара
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// название товара
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// цена
        /// </summary>
        public float Price { get; set; }
        /// <summary>
        /// код валюты (например, RUB, USD, EUR)
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// производитель товара
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// true — когда товар отсутствует в продаже
        /// </summary>
        public bool out_of_stock { get; set; }
        /// <summary>
        /// true — когда товар удален
        /// </summary>
        public bool removed { get; set; }
        /// <summary>
        /// true — когда API точно знает, что указан URL не магазина
        /// </summary>
        public bool not_shop { get; set; }
        /// <summary>
        /// когда API точно знает, что указан URL не товара 
        /// </summary>
        public bool not_product { get; set; }
        /// <summary>
        /// true — когда API не смог получить ключевые параметры товара, такие как title или price
        /// </summary>
        public bool Unprocessed { get; set; }
        public string img_url { get; set; }
        /// <summary>
        /// время создания запроса на обработку товара 
        /// </summary>
        public DateTime created_at { get; set; }
        public DateTime last_track_at { get; set; }
    }

    public class FetcheeTaskReturnDto
    {

        /// <summary>
        /// true - удачно поставлен в очередь на обработку 
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// true - Товар по данному URL уже был добавлен вами в очередь менее 6 часов назад
        /// </summary>
        public bool already_processed { get; set; }
        /// <summary>
        /// true — когда API точно знает, что указан URL не магазина
        /// </summary>
        public bool not_shop { get; set; }
        /// <summary>
        /// когда API точно знает, что указан URL не товара 
        /// </summary>
        public bool not_product { get; set; }
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public string Id { get; set; }
    }


    public class FetcheeTaskDto
    {
        public string Url { get; set; }
        public string api_key { get; set; }
        public string callback_url { get; set; }
    }


}