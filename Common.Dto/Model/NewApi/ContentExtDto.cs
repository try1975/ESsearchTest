using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PriceCommon.Enums;

namespace Common.Dto.Model.NewApi
{
    /// <summary>
    /// Элемент результата поискового запроса
    /// </summary>
    public class ContentExtDto : IDto<int>
    {
        /// <summary>
        /// Идентификатор элемент результата поискового запроса
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Наименование элемент результата (результат поиска)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Цена в формате строки для элемента результата поискового запроса
        /// </summary>
        public string Price { get; set; }
        /// <summary>
        /// Ссылка на конкретный ресурс, где был найден элемент результата поискового запроса
        /// </summary>
        public string Uri { get; set; }
        /// <summary>
        ///     Дата и время сбора цены (UTC, UnixTime)
        /// </summary>
        public long? CollectedAt { get; set; }

        /// <summary>
        ///     Дата и время сбора цены
        /// </summary>
        [JsonIgnore]
        public DateTime Collected
        {
            get { return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Convert.ToDouble(CollectedAt)) /*.Date*/; }
        }
        /// <summary>
        /// Идентификатор elasticId элемента результата поиска
        /// </summary>
        public string ElasticId { get; set; }
        /// <summary>
        /// Идентификатор поискового запроса
        /// </summary>
        public string SearchItemId { get; set; }
        /// <summary>
        /// Тип цены элемента результата поискового запроса
        /// 0 - Доверять (Trusted) - элемент результата получен из ElasticSearch - большая вероятность правильного определения цены
        /// 1 - Требует проверки (Check) - - элемент результата получен из интеренет, требует проверки оператором, например, контроль по скриншоту
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PriceType PriceType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public string PriceTypeString => PriceCommon.Utils.Utils.GetDescription(PriceType);

        /// <summary>
        /// Ссылка на сккриншот ресурса, где был найден элемент результата поискового запроса
        /// </summary>
        public string Screenshot { get; set; }
        /// <summary>
        /// Статус цены элемента результата поискового запроса, только проверенные оператором цены
        /// будут впоследствии автоматически актуализироваться (api/contentitem/checked/{id:int}).
        /// 0 - Не проверено (NotChecked)
        /// 1 - Проверено (Checked)
        /// 2 - Отбраковано (Rejected)
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PriceStatus PriceStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public string PriceStatusString => PriceCommon.Utils.Utils.GetDescription(PriceStatus);

        /// <summary>
        ///     Продавец
        /// </summary>
        public string Seller { get; set; }
        /// <summary>
        ///     Производитель
        /// </summary>
        public string Producer { get; set; }

        /// <summary>
        ///     Телефоны
        /// </summary>
        public string Phones { get; set; }

        /// <summary>
        ///     Варианты цен
        /// </summary>
        public string PriceVariants { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public bool Selected { get; set; }


        /// <summary>
        ///     Цена  (тип double) для элемента результата поискового запроса 
        /// </summary>
        //[JsonIgnore]
        public double Nprice
        {
            get
            {
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
        /// Причина отбраковки
        /// </summary>
        public string RejectReason { get; set; }
        /// <summary>
        /// Признак ручной установки цены
        /// </summary>
        public int ManualPrice { get; set; }

        /// <summary>
        /// Статус наличия товара
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ProdStatus? ProdStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public string ProdStatusString => PriceCommon.Utils.Utils.GetDescription(ProdStatus);

       
    }
}