namespace Common.Dto.Model
{
    /// <summary>
    /// Настройки для парсера
    /// </summary>
    public class SourceDto
    {
        /// <summary>
        /// Наименование источника
        /// </summary>
        public string Nm { get; set; }
        /// <summary>
        /// Хост источника
        /// </summary>
        public string Dsc { get; set; }

        /// <summary>
        /// Выражения для парсинга
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// Телефоны
        /// </summary>
        public string Phones { get; set; }
    }
}