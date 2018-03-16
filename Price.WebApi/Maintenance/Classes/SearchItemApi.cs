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
    /// <summary>
    /// 
    /// </summary>
    public class SearchItemApi : TypedApi<SearchItemExtDto, SearchItemEntity, string>, ISearchItemApi
    {
        private readonly IInternetContentQuery _internetContentQuery;
        private readonly IContentQuery _contentQuery;
        private readonly IInternetContentApi _internetContentApi;
        private readonly IContentApi _contentApi;

        private string _getUrl;
        private string _baseUrl;

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="internetContentQuery"></param>
        /// <param name="contentQuery"></param>
        /// <param name="internetContentApi"></param>
        /// <param name="contentApi"></param>
        public SearchItemApi(ISearchItemQuery query
            , IInternetContentQuery internetContentQuery, IContentQuery contentQuery
            , IInternetContentApi internetContentApi, IContentApi contentApi
            ) : base(query)
        {
            _internetContentQuery = internetContentQuery;
            _contentQuery = contentQuery;
            _internetContentApi = internetContentApi;
            _contentApi = contentApi;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override SearchItemExtDto GetItem(string id)
        {
            var entity = Query.GetEntity(id);
            var dto = Mapper.Map<SearchItemExtDto>(entity);
            if (entity == null) return dto;
            dto.Contents = GetContentCollection(entity);
            if (string.IsNullOrEmpty(entity.InternetSessionId)) return dto;
            dto.Contents.AddRange(GetInternetContentCollection(entity));
            return dto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchItemCondition"></param>
        /// <returns></returns>
        public IEnumerable<SearchItemHeaderDto> GetItemsByCondition(SearchItemCondition searchItemCondition)
        {
            var queryCondition = Query.GetEntities();

            if (searchItemCondition.DateFrom != null)
            {
                var dt = (long)searchItemCondition.DateFrom.Value.Date.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                queryCondition = queryCondition
                        .Where(z => z.StartProcessed >= dt)
                    ;
            }
            if (searchItemCondition.DateTo != null)
            {
                var dt = (long)searchItemCondition.DateTo.Value.Date.AddDays(1).Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                queryCondition = queryCondition
                        .Where(z => z.StartProcessed < dt)
                    ;
            }
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
            var entities = queryCondition.ToList();

            var dtos = Mapper.Map<List<SearchItemHeaderDto>>(entities);
            foreach (var dto in dtos)
            {
                dto.ContentCount += _contentQuery.GetEntities().Count(z => z.SearchItemId == dto.Id);
                dto.ContentCount += _internetContentQuery.GetEntities().Count(z => z.session_id == dto.InternetSessionId);
            }
            return dtos;
        }
        public override bool RemoveItem(string id)
        {
            var entity = Query.GetEntity(id);
            if (entity == null) return false;
            var contents = _contentQuery
                .GetEntities()
                .Where(z => z.SearchItemId == entity.Id)
                .ToList();
            foreach (var content in contents)
            {
                _contentApi.RemoveItem(content.Id);
            }
            var internets = _internetContentQuery
                .GetEntities()
                .Where(z => z.session_id == entity.InternetSessionId)
                .ToList();
            foreach (var internet in internets)
            {
                _internetContentApi.RemoveItem(internet.Id);
            }

            return base.RemoveItem(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SearchItemHeaderDto GetItemHeader(string id)
        {
            var entity = Query.GetEntity(id);
            var dto = Mapper.Map<SearchItemHeaderDto>(entity);
            if (dto == null) return null;
            dto.ContentCount += _contentQuery.GetEntities().Count(z => z.SearchItemId == entity.Id);
            dto.ContentCount += _internetContentQuery.GetEntities().Count(z => z.session_id == entity.InternetSessionId);
            return dto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ContentExtDto> GetItemContents(string id)
        {
            var entity = Query.GetEntity(id);
            if (entity == null) return null;
            var list = GetContentCollection(entity);
            if (string.IsNullOrEmpty(entity.InternetSessionId)) return list;
            list.AddRange(GetInternetContentCollection(entity));
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SearchItemBreak(string id)
        {
            var entity = Query.GetEntity(id);
            if (entity?.Status != TaskStatus.InProcess) return false;
            entity.ProcessedAt = Utils.GetUtcNow();
            entity.Status = TaskStatus.Break;
            Query.UpdateEntity(entity);
            AnalistService.TerminateSession(entity.InternetSessionId);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SearchItemChecked(string id)
        {
            var entity = Query.GetEntity(id);
            if (entity == null) return false;
            var hasContent = _contentQuery.GetEntities().Any(z => z.SearchItemId == entity.Id);
            if (!hasContent && !string.IsNullOrEmpty(entity.InternetSessionId))
                hasContent = _internetContentQuery.GetEntities().Any(z => z.session_id == entity.InternetSessionId);
            if (!hasContent) return false;
            if (!new[] { TaskStatus.Ok, TaskStatus.Break, TaskStatus.BreakByTimeout }.Contains(entity.Status)) return false;
            entity.Status = TaskStatus.Checked;
            Query.UpdateEntity(entity);
            return true;
        }

        private List<ContentExtDto> GetContentCollection(SearchItemEntity entity)
        {
            return _contentQuery.GetEntities().Where(z => z.SearchItemId == entity.Id).ToList()
                    .Select(z => new ContentExtDto()
                    {
                        Id = z.Id,
                        Name = z.Name,
                        Price = z.Price,
                        Uri = z.Uri,
                        SearchItemId = z.SearchItemId,
                        CollectedAt = z.CollectedAt,
                        PriceType = PriceType.Trusted,
                        ElasticId = z.ElasticId,
                        Screenshot = string.IsNullOrEmpty(z.Screenshot) ? null : $"{_getUrl}{z.Screenshot}",
                        PriceStatus = z.PriceStatus
                    }).ToList();
        }

        private List<ContentExtDto> GetInternetContentCollection(SearchItemEntity entity)
        {
            return _internetContentQuery.GetEntities().Where(z => z.session_id == entity.InternetSessionId).ToList()
                    .Select(z => new ContentExtDto()
                    {
                        Id = z.Id,
                        Name = z.preview,
                        Price = z.price.ToString(),
                        Uri = z.url,
                        SearchItemId = entity.Id,
                        CollectedAt = (long)z.dt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                        PriceType = PriceType.Check,
                        Screenshot = string.IsNullOrEmpty(z.contact_url) ? null : $"{_getUrl}{z.contact_url}",
                        PriceStatus = z.PriceStatus
                    }).ToList();
        }
    }
}