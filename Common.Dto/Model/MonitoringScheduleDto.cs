using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PriceCommon.Enums;
using System;

namespace Common.Dto.Model
{
    /// <summary>
    /// Структура для задания параметров мониторинга
    /// </summary>
    public class MonitoringScheduleDto: IDto<int>
    {
        public int Id { get; set; }
        /// <summary>
        /// Дата начала выполнения 
        /// </summary>
        public DateTime FirstDate { get; set; }
        /// <summary>
        /// Дата последнего выполнения 
        /// </summary>
        public DateTime? LastDate { get; set; }
        /// <summary>
        /// Дата следующего выполнения 
        /// </summary>
        public DateTime? NextRequestDate { get; set; }
        /// <summary>
        /// Частота обновления (0-ежедневно, 1-еженедельно)
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("frequency")]
        public Frequency Frequency { get; set; }
        /// <summary>
        /// Мониторинг актуален 
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ссылка
        /// </summary>
        public string Uri { get; set; }
    }
}
