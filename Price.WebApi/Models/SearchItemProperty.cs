using Newtonsoft.Json;

namespace Price.WebApi.Models
{
    /// <summary>
    /// свойство искомой позиции
    /// </summary>
    public class SearchItemProperty
    {
        /// <summary>
        /// наименование свойства
        /// </summary>
        [JsonProperty("имя")]
        public string Key { get; set; }
        /// <summary>
        /// значение свойства
        /// </summary>
        [JsonProperty("значение")]
        public string Value { get; set; }
        /// <summary>
        /// да/нет - раздел введён для удобства группировки
        /// </summary>
        [JsonProperty("группировка")]
        public string Group { get; set; }
    }

}