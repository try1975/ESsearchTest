using Common.Dto.Model.NewApi;

namespace Price.WebApi.Maintenance.Interfaces
{
    public interface IInternetContentApi : ITypedApi<InternetContentDto, int>
    {
        bool InternetContentChecked(int id);
    }
}