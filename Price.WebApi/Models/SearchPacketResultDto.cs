﻿using System.Collections.Generic;

namespace Price.WebApi.Models
{
    /// <summary>
    /// Результат пакетного поиска
    /// </summary>
    public class SearchPacketResultDto
    {
        /// <summary>
        /// id позиции задачи поиска
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// наименование позиции задачи поиска
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Найденные ТРУ
        /// </summary>
        public IEnumerable<ContentDto> Content { get; set; }
    }
}