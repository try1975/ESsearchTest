using System;
using PriceCommon.Enums;

namespace Common.Dto.Model.NewApi
{
    /// <summary>
    /// Условия отбора поисковых запросов
    /// </summary>
    public class SearchItemCondition
    {
        /// <summary>
        /// Дата начала периода, в котором будет производиться поиск (по времени старта поискового запроса), может отсутствовать
        /// </summary>
        public DateTime? DateFrom { get; set; }
        /// <summary>
        /// Дата окончания периода, в котором будет производиться поиск (по времени старта поискового запроса), может отсутствовать
        /// </summary>
        public DateTime? DateTo { get; set; }
        /// <summary>
        /// Фильтр по статусу поискового запроса, может отсутствовать
        /// 0- Не обработан (NotInitialized)
        /// 1 - В очереди (InQueue)
        /// 2- Ошибка (Error)
        /// 3- Завершено (Ok)
        /// 4- В процессе (InProcess)
        /// 5 - Прекращено по таймауту (BreakByTimeout)
        /// 6 - Прекращено (Break)
        /// 7 - Проверено (Checked)
        /// </summary>
        public TaskStatus? Status { get; set; }
        /// <summary>
        /// Наименование ТРУ для поиска в поисковом запросе, поиск по неполному совпадению
        /// (например если name=болт, будут найдены поисковые запросы болт стальной и набор гайка, болт, шайба)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Метка - берется из Id элемента пакета поиска(классификатор ТРУ), может задаваться произвольно или формироваться автоматически
        /// Имеет длину 128 символов, по ней доступен поиск по условиям по полному совпадению
        /// </summary>
        public string ExtId { get; set; }

        /// <summary>
        /// Взять только те поисковые запросы, где среди источников поиска был указан источник internet
        /// </summary>
        public bool IsInternet { get; set; }

        /// <summary>
        /// Взять только те поисковые запросы, где совпадает ОКПД2
        /// </summary>
        public string Okpd2 { get; set; }

        /// <summary>
        /// размер страницы (100 по умолчанию)
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// номер страницы
        /// </summary>
        public int PageNum { get; set; }
    }
}