using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Price.WebApi.Model.UpdatePrice
{
    /// <summary>
    /// Объекьт обработки
    /// </summary>
    public class UpdatePriceDto
    {
        /// <summary>
        /// Uri
        /// </summary>
        public Uri Uri { get; set; }
        /// <summary>
        /// Когда завершена обработка (UTC)
        /// </summary>
        public long? ProcessedAt { get; set; }

        /// <summary>
        /// Статус объекта обработки
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public UpdatePriceStatus Status { get; set; }

        /// <summary>
        /// Ссылка на скриншот (png)
        /// </summary>
        public Uri ScreenshotLink { get; set; }
    }

    /// <summary>
    /// Статус объекта обработки
    /// </summary>
    public enum UpdatePriceStatus
    {
        /// <summary>
        /// Обработка не начата
        /// </summary>
        NotProcessed = 0,
        /// <summary>
        /// Ошибка при обработке
        /// </summary>
        Error = 1,
        /// <summary>
        /// Обработка успешно завершена
        /// </summary>
        Ok = 2,
        /// <summary>
        /// Ошибка - нет настроек источника
        /// </summary>
        ElangError = 3
    }
}