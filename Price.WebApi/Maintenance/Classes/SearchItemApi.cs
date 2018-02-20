using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Common.Dto.Model.NewApi;
using Price.Db.Entities.Entities;
using Price.Db.Entities.QueryProcessors;
using Price.WebApi.Maintenance.Interfaces;
using PriceCommon.Enums;

namespace Price.WebApi.Maintenance.Classes
{
    public class SearchItemApi : TypedApi<SearchItemExtDto, SearchItemEntity, string>, ISearchItemApi
    {
        private readonly IInternetContentQuery _internetContentQuery;
        private readonly IContentQuery _contentQuery;

        public SearchItemApi(ISearchItemQuery query, IInternetContentQuery internetContentQuery, IContentQuery contentQuery) : base(query)
        {
            _internetContentQuery = internetContentQuery;
            _contentQuery = contentQuery;
        }

        public override SearchItemExtDto GetItem(string id)
        {
            var entity = Query.GetEntity(id);
            var dto = Mapper.Map<SearchItemExtDto>(entity);
            if (entity == null) return dto;

            var contentResults = _contentQuery
                .GetEntities()
                .Where(z => z.SearchItemId == entity.Id)
                .ToList();
            dto.Contents = Mapper.Map<List<ContentExtDto>>(contentResults);
            if (string.IsNullOrEmpty(entity.InternetSessionId)) return dto;

            var internetResults = _internetContentQuery
                    .GetEntities()
                    .Where(z => z.session_id == entity.InternetSessionId)
                    .ToList()
                ;
            dto.Contents.AddRange(internetResults.Select(z => new ContentExtDto()
            {
                Id = z.Id,
                Name = z.preview,
                Price = z.price.ToString(),
                Uri = z.url,
                SearchItemId = id,
                CollectedAt = (long)z.dt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                PriceType = PriceType.Check
            })
                .ToList());
            return dto;
        }

        public bool Exists(string id)
        {
            return Query.GetEntity(id) != null;
        }

        public IEnumerable<SearchItemExtDto> GetItemsByCondition(SearchItemCondition searchItemCondition)
        {
            var queryCondition = Query.GetEntities();
            if (!string.IsNullOrEmpty(searchItemCondition.Name))
                queryCondition = queryCondition
                        .Where(z => z.Name.Contains(searchItemCondition.Name.ToLower()))
                    ;
            if (!string.IsNullOrEmpty(searchItemCondition.ExtId))
                queryCondition = queryCondition
                    .Where(z => z.ExtId == searchItemCondition.ExtId)
                    ;
            if (searchItemCondition.IsInternet)
                queryCondition = queryCondition
                        .Where(z => !string.IsNullOrEmpty(z.InternetSessionId))
                    ;
            if (searchItemCondition.Status!=null)
                queryCondition = queryCondition
                        .Where(z => z.Status== searchItemCondition.Status)
                    ;
            var list = queryCondition.ToList();
            return Mapper.Map<List<SearchItemExtDto>>(list);
        }
    }
}