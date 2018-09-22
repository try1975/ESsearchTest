namespace Common.Dto.Model.FindCompany
{
    public class FindCompanyDto: IDto<int>
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public string Inn { get; set; }
        public string Name { get; set; }
    }
}