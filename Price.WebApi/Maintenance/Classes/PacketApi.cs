using Common.Dto.Model.NewApi;
using Price.Db.Entities.Entities;
using Price.Db.Entities.QueryProcessors;
using Price.WebApi.Logic;
using Price.WebApi.Maintenance.Interfaces;

namespace Price.WebApi.Maintenance.Classes
{
    public class PacketApi : TypedApi<PacketDto, PacketEntity, string>, IPacketApi
    {
        public PacketApi(IPacketQuery query) : base(query)
        {
        }

        public override PacketDto AddItem(PacketDto dto)
        {
            if (string.IsNullOrEmpty(dto.Id)) dto.Id = IdService.GenerateId();
            return base.AddItem(dto);
        }
    }
}