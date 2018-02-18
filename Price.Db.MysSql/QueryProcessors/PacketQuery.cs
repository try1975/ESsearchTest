using System.Data.Entity;
using Price.Db.Entities.Entities;
using Price.Db.Entities.QueryProcessors;

namespace Price.Db.MysSql.QueryProcessors
{
    public class PacketQuery : TypedQuery<PacketEntity, string>, IPacketQuery
    {
        public PacketQuery(DbContext db) : base(db)
        {
        }
    }
}