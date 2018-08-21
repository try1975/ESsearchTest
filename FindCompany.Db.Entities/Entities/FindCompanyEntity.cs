using Db.Entities;

namespace FindCompany.Db.Entities.Entities
{
    public class FindCompanyEntity : IEntity<int>
    {
        public int Id { get; set; }
        public string Host { get; set; }
        public string Inn { get; set; }
        public string Name { get; set; }
    }
}