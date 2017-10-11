using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Price.WebApi.Models.UpdatePrice
{
    /// <summary>
    /// Задача обновления цен
    /// </summary>
    public class UpdatePriceTaskDto
    {
        public UpdatePriceTaskDto(int capacity = 200)
        {
            UpdatePrices = new List<UpdatePriceDto>(capacity);
        }

        [JsonIgnore]
        public bool CreateScreenshots { get; set; }

        [JsonIgnore]
        public string BaseUri { get; set; }

        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Всего объектов в задаче
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// Уже обработано объектов в задаче
        /// </summary>
        public int ProcessedCount { get; set; }

        /// <summary>
        /// Список объектов в задаче
        /// </summary>
        public List<UpdatePriceDto> UpdatePrices { get; }

        public void UpdateStatistics()
        {
            TotalCount = UpdatePrices.Count;
            ProcessedCount = UpdatePrices.Count(z => z.ProcessedAt != null);
        }
    }
}