using Newtonsoft.Json;

namespace Common.Dto.Model
{
    /// <summary>
    /// Структура для задания параметров поиска типа Maybe
    /// </summary>
    public class MaybeDto
    {
        /// <summary>
        /// обязательное вхождение
        /// </summary>
        [JsonProperty("must")]
        public string[] Musts { get; set; }
        /// <summary>
        /// возможное вхождение
        /// </summary>
        [JsonProperty("should")]
        public string[] Shoulds { get; set; }
        /// <summary>
        /// обязательное отсутствие
        /// </summary>
        [JsonProperty("mustNot")]
        public string[] MustNots { get; set; }

    }
}