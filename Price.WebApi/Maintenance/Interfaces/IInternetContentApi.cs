using Common.Dto.Model.NewApi;

namespace Price.WebApi.Maintenance.Interfaces
{
    public interface IInternetContentApi : ITypedApi<InternetContentDto, int>
    {
        bool InternetContentChecked(int id);
        bool InternetContentSetPrice(int id, string price);
        bool InternetContentRejected(int id, string reason);
    }
}