using Newtonsoft.Json;

namespace Common.Dto.Model.NewApi
{
    /// <summary>
    /// Для поиска по тексту страницы
    /// </summary>
    public class ContentExtTxtDto : ContentExtDto
    {
        [JsonIgnore]
        public string Txt { get; set; }
        [JsonIgnore]
        public string Html { get; set; }
    }
}
