namespace Common.Dto.Model.NewApi
{
    public class ContentMoveDto : IDto<int>
    {
        public int Id { get; set; }
        public string ElasticId { get; set; }
    }
}