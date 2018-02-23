using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Common.Dto.Logic;
using Common.Dto.Model.NewApi;
using Price.Db.Entities.Entities;
using Price.Db.Entities.QueryProcessors;
using Price.WebApi.Logic;
using Price.WebApi.Maintenance.Interfaces;
using PriceCommon.Enums;


namespace Price.WebApi.Maintenance.Classes
{
    public class SearchItemApi : TypedApi<SearchItemExtDto, SearchItemEntity, string>, ISearchItemApi
    {
        private readonly IInternetContentQuery _internetContentQuery;
        private readonly IContentQuery _contentQuery;

        public string BaseUrl
        {
            get { return _baseUrl; }
            set
            {
                _baseUrl = value;
                //_getUrl = $"{_baseUrl}/GetFiles/{nameof(GetFile)}.ashx?id=";
                _getUrl = $"{_baseUrl}/{AppGlobal.Screenshots}/";
            }
        }

        private string _getUrl;
        private string _baseUrl;

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
                PriceType = PriceType.Check,
                Screenshot = string.IsNullOrEmpty(z.contact_url) ? null : $"{_getUrl}{z.contact_url}"
            })
                .ToList());
            return dto;
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
            if (searchItemCondition.Status != null)
                queryCondition = queryCondition
                        .Where(z => z.Status == searchItemCondition.Status)
                    ;
            var list = queryCondition.ToList();
            return Mapper.Map<List<SearchItemExtDto>>(list);
        }

        public SearchItemHeaderDto GetItemHeader(string id)
        {
            var entity = Query.GetEntity(id);
            return Mapper.Map<SearchItemHeaderDto>(entity);
        }

        public List<ContentExtDto> GetItemContents(string id)
        {
            var entity = Query.GetEntity(id);
            if (entity == null) return null;

            var contentResults = _contentQuery
                .GetEntities()
                .Where(z => z.SearchItemId == entity.Id)
                .ToList();
            //var list = Mapper.Map<List<ContentExtDto>>(contentResults);
            var list = contentResults.Select(z => new ContentExtDto
            {
                Id = z.Id,
                Name = z.Name,
                Price = z.Price,
                Uri = z.Uri,
                SearchItemId = z.SearchItemId,
                CollectedAt = z.CollectedAt,
                PriceType = PriceType.Trusted,
                ElasticId = z.ElasticId,
                Screenshot = string.IsNullOrEmpty(z.Screenshot) ? null : $"{_getUrl}{z.Screenshot}"
            })
                .ToList();
            if (string.IsNullOrEmpty(entity.InternetSessionId)) return list;

            var internetResults = _internetContentQuery
                    .GetEntities()
                    .Where(z => z.session_id == entity.InternetSessionId)
                    .ToList()
                ;

            list.AddRange(internetResults.Select(z => new ContentExtDto()
            {
                Id = z.Id,
                Name = z.preview,
                Price = z.price.ToString(),
                Uri = z.url,
                SearchItemId = id,
                CollectedAt = (long)z.dt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                PriceType = PriceType.Check,
                Screenshot = string.IsNullOrEmpty(z.contact_url) ? null : $"{_getUrl}{z.contact_url}"
            })
                .ToList());
            return list;
        }

        public bool SetCompleted(string id)
        {
            var entity = Query.GetEntity(id);
            entity.ProcessedAt = Utils.GetUtcNow();
            entity.Status = TaskStatus.Ok;
            return Query.UpdateEntity(entity) != null;
        }


    }
}