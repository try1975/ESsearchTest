namespace Price.Db.Entities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}