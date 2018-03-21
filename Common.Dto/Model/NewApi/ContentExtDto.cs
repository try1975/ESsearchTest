using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PriceCommon.Enums;

namespace Common.Dto.Model.NewApi
{
    public class ContentExtDto : IDto<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Uri { get; set; }
        /// <summary>
        ///     Дата и время сбора цены (UTC)
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
        public string ElasticId { get; set; }
        public string SearchItemId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public PriceType PriceType { get; set; }

        [JsonIgnore]
        public string PriceTypeString => PriceCommon.Utils.Utils.GetDescription(PriceType);

        public string Screenshot { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public PriceStatus PriceStatus { get; set; }
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
    }
}