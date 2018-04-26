﻿using System.Linq;
using Price.Db.Entities.Entities;

namespace Price.Db.Entities.QueryProcessors
{
    public interface IInternetContentQuery : ITypedQuery<InternetContentEntity, int>
    {
    }
}