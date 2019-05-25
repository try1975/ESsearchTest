namespace Common.Dto.Model.Packet
{
    public interface ISearchItemCallback
    {
        void FireCallback(string url, string id);
    }
}