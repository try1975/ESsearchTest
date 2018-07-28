namespace Common.Dto.Model.NewApi
{
    /// <summary>
    /// Элемент результата поискового запроса для перемещения в другой поисковой запрос
    /// </summary>
    public class ContentMoveDto : IDto<int>
    {
        /// <summary>
        /// Идентификатор элемент результата поискового запроса
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор elasticId элемента результата поискового запроса
        /// </summary>
        public string ElasticId { get; set; }
    }
}