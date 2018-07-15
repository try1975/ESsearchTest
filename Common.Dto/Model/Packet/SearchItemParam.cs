using Newtonsoft.Json;

namespace Common.Dto.Model.Packet
{
    /// <summary>
    /// искомая позиция
    /// </summary>
    public class SearchItemParam
    {
        /// <summary>
        /// id позиции
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// наименование позиции
        /// </summary>
        [JsonProperty("наименование")]
        public string Name { get; set; }
        /// <summary>
        /// категория позиции, может отсутствовать
        /// </summary>
        [JsonProperty("категория")]
        public string Category { get; set; }
        /// <summary>
        /// список свойств, может отсутствовать
        /// </summary>
        [JsonProperty("свойства")]
        public SearchItemProperty[] SearchItemProperties { get; set; }
        /// <summary>
        /// расширенная информация о позиции, может отсутствовать
        /// </summary>
        [JsonProperty("инфо")]
        public string Info { get; set; }
        /// <summary>
        /// альтернативные наименования позиции
        /// </summary>
        [JsonProperty("синонимы")]
        public string[] Syn { get; set; }
        /// <summary>
        /// нормализатор, применяемый при обработке позиции
        /// </summary>
        [JsonProperty("нормализатор")]
        public string Norm { get; set; }
        /// <summary>
        /// Дополнительные слова для поиска
        /// </summary>
        public string AddKeywords { get; set; }

        public string Priority { get; set; }
        public byte Options { get; set; }
    }
}