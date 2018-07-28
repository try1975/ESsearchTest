using System;
using Common.Dto.Logic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PriceCommon.Enums;

namespace Common.Dto.Model.NewApi
{
    /// <summary>
    /// Заголовок поискового запроса
    /// </summary>
    public class SearchItemHeaderDto : IDto<string>
    {
        /// <summary>
        /// Идентификатор поискового запроса
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Наименование ТРУ для поиска
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Время старта поискового запроса (Utc[https://ru.wikipedia.org/wiki/%D0%92%D1%81%D0%B5%D0%BC%D0%B8%D1%80%D0%BD%D0%BE%D0%B5_%D0%BA%D0%BE%D0%BE%D1%80%D0%B4%D0%B8%D0%BD%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%BD%D0%BE%D0%B5_%D0%B2%D1%80%D0%B5%D0%BC%D1%8F], Unix Time)
        /// </summary>
        public long? StartProcessed { get; set; }
        /// <summary>
        /// Время последнего обновления результатов поискового запроса (Utc, Unix Time)
        /// </summary>
        public long? LastUpdate { get; set; }
        /// <summary>
        /// Время окончания поискового запроса (Utc, Unix Time)
        /// </summary>
        public long? ProcessedAt { get; set; }

        /// <summary>
        /// Статус поискового запроса
        /// 0- Не обработан (NotInitialized)
        /// 1 - В очереди (InQueue)
        /// 2- Ошибка (Error)
        /// 3- Завершено (Ok)
        /// 4- В процессе (InProcess)
        /// 5 - Прекращено по таймауту (BreakByTimeout)
        /// 6 - Прекращено (Break)
        /// 7 - Проверено (Checked)
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TaskStatus Status { get; set; }

        /// <summary>
        /// Наименование источника поиска (md5 - по алиасу md5 в ElasticSearch, internet - Интернет, поисковые системы)
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Метка - берется из Id элемента пакета поиска(классификатор ТРУ), может задаваться произвольно или формироваться автоматически
        /// Имеет длину 128 символов, по ней доступен поиск по условиям по полному совпадению
        /// </summary>
        public string ExtId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public string InternetSessionId { get; set; }

        /// <summary>
        /// Нормализатор
        /// </summary>
        public string Normalizer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public DateTime? StartProcessedDateTime
        {
            get
            {
                var unixTimeStamp = StartProcessed ?? 0;
                return Utils.UnixTimeStampToDateTime(unixTimeStamp);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public DateTime? LastUpdateDateTime
        {
            get
            {
                var unixTimeStamp = LastUpdate ?? 0;
                return Utils.UnixTimeStampToDateTime(unixTimeStamp);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public DateTime? ProcessedAtDateTime
        {
            get
            {
                var unixTimeStamp = ProcessedAt ?? 0;
                return Utils.UnixTimeStampToDateTime(unixTimeStamp);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public string StatusString => PriceCommon.Utils.Utils.GetDescription(Status);

        /// <summary>
        /// Количество элементов результата поиска по поисковому запросу
        /// </summary>
        public int ContentCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tick"></param>
        public void BeginProcess(long tick)
        {
            StartProcessed = tick;
            LastUpdate = tick;
            ProcessedAt = null;
            Status = TaskStatus.InProcess;
        }

    }
}