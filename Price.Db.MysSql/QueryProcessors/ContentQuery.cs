﻿using Db.Entities;
using Price.Db.Entities.Entities;
using Price.Db.Entities.QueryProcessors;

namespace Price.Db.Postgress.QueryProcessors
{
    public class ContentQuery : TypedQuery<ContentEntity, int>, IContentQuery
    {
        public ContentQuery(PriceContext db) : base(db)
        {
        }
    }
}