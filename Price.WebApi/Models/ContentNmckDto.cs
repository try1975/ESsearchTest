using System;
using System.Collections.Generic;

namespace Price.WebApi.Models
{
    /// <summary>
    ///     Результат расчета НМЦК
    /// </summary>
    public class ContentNmckDto
    {
        /// <summary>
        ///     Исходное наименование ТРУ для поиска
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     Значение НМЦК
        /// </summary>
        public decimal Nmck { get; set; }

        /// <summary>
        ///     Дата расчета
        /// </summary>
        public DateTime CalcDate { get; set; }

        /// <summary>
        ///     Текст расчета
        /// </summary>
        public string CalcText { get; set; }

        /// <summary>
        ///     Список результатов поиска для расчета
        /// </summary>
        public IEnumerable<ContentDto> Contents { get; set; }
    }
}