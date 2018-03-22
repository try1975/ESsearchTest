using Common.Dto.Model.NewApi;

namespace Price.WebApi.Maintenance.Interfaces
{
    public interface IContentApi : ITypedApi<ContentExtDto, int>
    {
        bool ContentChecked(int id);
        bool ContentSetPrice(int id, string price);
    }
}