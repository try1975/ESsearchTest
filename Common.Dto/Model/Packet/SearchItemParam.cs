using Newtonsoft.Json;

namespace Common.Dto.Model.Packet
{
    
    /// <summary>
    /// искомая позиция
    /// </summary>
    public class SearchItemParam
    {
        /// <summary>
        /// Использовать Yandex при поиске
        /// </summary>
        public const byte UseYandex = 1;
        /// <summary>
        /// Использовать Google при поиске
        /// </summary>
        public const byte UseGoogle = 2;
        /// <summary>
        /// Конструктор, поисковые движки по умолчанию все
        /// </summary>
        public SearchItemParam()
        {
            SearchEngine = UseYandex & UseGoogle;
        }
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
        /// <summary>
        /// Приоритет запроса
        /// Normal = Нормальный
        /// High = Высокий
        /// Max = Максимальный
        /// </summary>
        public string Priority { get; set; }
        /// <summary>
        /// Битовая маска опций поиска
        ///  Раздельные запросы по каждому дополнительному слову - установить флаг 8
        ///  Не извлекать цены - установить флаг 2
        /// </summary>
        public byte Options { get; set; }
        /// <summary>
        /// Битовая маска использования поисковых систем
        /// Yandex    = 0x0001
        /// Google    = 0x0002
        /// </summary>
        public byte SearchEngine { get; set; }
        /// <summary>
        /// Код ОКПД2 - может быть использован отбора
        /// </summary>
        public string Okpd2 { get; set; }
    }
}