using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PriceCommon.Enums;
using System;

namespace Common.Dto.Model
{
    /// <summary>
    /// Структура для задания параметров мониторинга (расписание)
    /// </summary>
    public class ScheduleDto: IDto<int>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Дата выполнения 
        /// </summary>
        public DateTime NextDate { get; set; }
        /// <summary>
        /// Частота обновления (0-день, 1-неделя, 2-месяц, 3-два месяца, 4-квартал, 5-полгода, 6-год)
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
