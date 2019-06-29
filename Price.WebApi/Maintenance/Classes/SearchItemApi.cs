using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Common.Dto.Logic;
using Common.Dto.Model.NewApi;
using Gma.CodeCloud.Controls.TagCloud;
using Gma.CodeCloud.Controls.TextAnalyses.Blacklist;
using Gma.CodeCloud.Controls.TextAnalyses.Extractors;
using Price.Db.Entities.Entities;
using Price.Db.Entities.QueryProcessors;
using Price.WebApi.Logic;
using Price.WebApi.Logic.Screenshot;
using Price.WebApi.Maintenance.Interfaces;
using PriceCommon.Enums;
using Gma.CodeCloud.Controls.TextAnalyses.Processing;

namespace Price.WebApi.Maintenance.Classes
{
    /// <summary>
    /// 
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class SearchItemApi : TypedApi<SearchItemExtDto, SearchItemEntity, string>, ISearchItemApi
    {
        private readonly IInternetContentQuery _internetContentQuery;
        private readonly IContentQuery _contentQuery;
        private readonly IInternetContentApi _internetContentApi;
        private readonly IContentApi _contentApi;
        private readonly IEnricheApi _enricheApi;

        private string _getUrl;
        private string _baseUrl;

        /// <summary>
        /// 
        /// </summary>
        public string BaseUrl
        {
            get => _baseUrl;
            set
            {
                _baseUrl = value;
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
        /// <param name="enricheApi"></param>
        public SearchItemApi(ISearchItemQuery query
            , IInternetContentQuery internetContentQuery, IContentQuery contentQuery
            , IInternetContentApi internetContentApi, IContentApi contentApi, IEnricheApi enricheApi
            ) : base(query)
        {
            _internetContentQuery = internetContentQuery;
            _contentQuery = contentQuery;
            _internetContentApi = internetContentApi;
            _contentApi = contentApi;
            _enricheApi = enricheApi;
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
            if(!string.IsNullOrWhiteSpace(searchItemCondition.Okpd2))
                queryCondition = queryCondition
                        .Where(z => z.JsonText.Contains(searchItemCondition.Okpd2))
                    ;
            var pageSize = searchItemCondition.PageSize > 0 ? searchItemCondition.PageSize : 100;
            var pageNum = searchItemCondition.PageNum > 1 ? (searchItemCondition.PageNum - 1) * pageSize : 0;

            var entities = queryCondition.OrderBy(i => i.StartProcessed)
                    .Skip(pageNum)
                    .Take(pageSize)
                    .ToList()
                    ;

            var dtos = new List<SearchItemHeaderDto>(entities.Count);
            foreach (var searchItemEntity in entities)
            {
                dtos.Add(MapSearchItemHeaderDto(searchItemEntity));
            }
            return dtos;
        }

        private SearchItemHeaderDto MapSearchItemHeaderDto(SearchItemEntity searchItemEntity)
        {
            if(searchItemEntity==null) return null;
            var contentCount = 0;
            contentCount += searchItemEntity.ElasticCount;
            if (!string.IsNullOrEmpty(searchItemEntity.InternetSessionId)) contentCount += searchItemEntity.InternetCount;
            var dto = new SearchItemHeaderDto
            {
                Id = searchItemEntity.Id,
                Name = searchItemEntity.Name,
                InternetSessionId = searchItemEntity.InternetSessionId,
                StartProcessed = searchItemEntity.StartProcessed,
                LastUpdate = searchItemEntity.LastUpdate,
                ProcessedAt = searchItemEntity.ProcessedAt,
                Status = searchItemEntity.Status,
                Source = searchItemEntity.Source,
                ExtId = searchItemEntity.ExtId,
                Normalizer = searchItemEntity.Normalizer,
                JsonText = searchItemEntity.JsonText,
                ContentCount = contentCount
            };
            return dto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
            return MapSearchItemHeaderDto(Query.GetEntity(id));
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

        public List<ContentExtTxtDto> GetItemContentsTxt(string id)
        {
            var entity = Query.GetEntity(id);
            if (entity == null) return null;
            var list = Mapper.Map<List<ContentExtTxtDto>>(GetContentCollection(entity));
            if (string.IsNullOrEmpty(entity.InternetSessionId)) return list;
            list.AddRange(GetInternetContentTxtCollection(entity));
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
                        Screenshot = string.IsNullOrEmpty(z.Screenshot) ? $"{_getUrl}{WebshotTool.GetWebshotName(z.Id, z.Uri)}" : $"{_getUrl}{z.Screenshot}",
                        PriceStatus = z.PriceStatus,
                        Seller = _enricheApi.GetSeller(z.Uri),
                        Producer = z.Producer,
                        Phones = z.Phones,
                        ProdStatus = z.ProdStatus
                    }).ToList();
        }

        private List<ContentExtDto> GetInternetContentCollection(SearchItemEntity entity)
        {
            // not take screenshot big blob
            var list = _internetContentQuery.GetEntities().Where(x => x.session_id == entity.InternetSessionId)
                .Select(z => new
                {
                    z.Id,
                    z.spgz_Id,
                    z.dt,
                    z.price,
                    z.url,
                    z.src_id,
                    z.contact_url,
                    z.task_id,
                    z.session_id,
                    z.preview,
                    z.selected,
                    z.currency,
                    z.opt,
                    z.referer,
                    z.prices,
                    z.PriceStatus,
                    z.ProdStatus
                })
                .ToList();
            return list.Select(z => new ContentExtDto()
            {
                Id = z.Id,
                Name = z.preview,
                Price = z.price.ToString(),
                Uri = z.url,
                SearchItemId = entity.Id,
                CollectedAt = (long)z.dt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                PriceType = PriceType.Check,
                Screenshot = string.IsNullOrEmpty(z.contact_url) ? $"{_getUrl}{WebshotTool.GetWebshotName(z.Id, z.url)}" : $"{_getUrl}{z.contact_url}",
                PriceStatus = z.PriceStatus,
                PriceVariants = z.prices,
                Seller = _enricheApi.GetSeller(z.url),
                ProdStatus = z.ProdStatus
            }).ToList();
        }

        private List<ContentExtTxtDto> GetInternetContentTxtCollection(SearchItemEntity entity)
        {
            // not take screenshot big blob
            var list = _internetContentQuery.GetEntities().Where(x => x.session_id == entity.InternetSessionId)
             .Select(z => new
             {
                 z.Id,
                 z.spgz_Id,
                 z.dt,
                 z.price,
                 z.url,
                 z.src_id,
                 z.contact_url,
                 z.task_id,
                 z.session_id,
                 z.preview,
                 z.selected,
                 z.currency,
                 z.opt,
                 z.referer,
                 z.prices,
                 /*z.unit_price,
                 z.unit,
                 z.weight,
                 z.rate,*/
                 z.PriceStatus,
                 z.ProdStatus,
                 z.txt,
                 z.html
             })
             .ToList();
            return list.Select(z => new ContentExtTxtDto()
            {
                Id = z.Id,
                Name = z.preview,
                Price = z.price.ToString(),
                Uri = z.url,
                SearchItemId = entity.Id,
                CollectedAt = (long)z.dt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                PriceType = PriceType.Check,
                Screenshot = string.IsNullOrEmpty(z.contact_url) ? $"{_getUrl}{WebshotTool.GetWebshotName(z.Id, z.url)}" : $"{_getUrl}{z.contact_url}",
                PriceStatus = z.PriceStatus,
                PriceVariants = z.prices,
                Seller = _enricheApi.GetSeller(z.url),
                ProdStatus = z.ProdStatus,
                Txt = z.txt,
                Html = z.html
            }).ToList();
            //TODO: enriche seller from external source
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="extId"></param>
        /// <returns></returns>
        public SearchItemHeaderDto MoveContents(IEnumerable<ContentMoveDto> dtos, string id, string name, string extId)
        {
            var entity = Query.GetEntity(id);
            if (entity == null) return null;
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(extId))
            {
                entity = Query.InsertEntity(new SearchItemEntity
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Name = string.IsNullOrEmpty(name) ? entity.Name : name,
                    ExtId = string.IsNullOrEmpty(extId) ? entity.ExtId : extId,
                    InternetSessionId = Guid.NewGuid().ToString("B"),
                    JsonText = entity.JsonText,
                    Normalizer = entity.Normalizer,
                    Source = entity.Source,
                    StartProcessed = entity.StartProcessed,
                    LastUpdate = entity.LastUpdate,
                    ProcessedAt = entity.ProcessedAt,
                    Status = entity.Status
                });
            }
            foreach (var moveDto in dtos)
            {
                if (!string.IsNullOrEmpty(moveDto.ElasticId))
                {
                    var contentEntity = _contentQuery.GetEntities().FirstOrDefault(z => z.Id == moveDto.Id);
                    if (contentEntity == null) continue;
                    contentEntity.SearchItemId = entity.Id;
                    _contentQuery.UpdateEntity(contentEntity);
                }
                else
                {
                    var contentEntity = _internetContentQuery.GetEntities().FirstOrDefault(z => z.Id == moveDto.Id);
                    if (contentEntity == null) continue;
                    contentEntity.session_id = entity.InternetSessionId;
                    _internetContentQuery.UpdateEntity(contentEntity);
                }
            }
            return MapSearchItemHeaderDto(entity);
        }

        public IEnumerable<IWord> WordsCloud(string id)
        {
            //return new List<WordDto>();
            var entity = Query.GetEntity(id);
            if (entity == null) return new List<IWord>();
            var contents = GetContentCollection(entity);
            if (!string.IsNullOrEmpty(entity.InternetSessionId))
            { contents.AddRange(GetInternetContentCollection(entity)); }

            var text = contents.AsEnumerable()
                .Select(s => s.Name)
                .Aggregate((cur, next) => cur + "\r\n" + next);

            var blacklist = ComponentFactory.CreateBlacklist(true);
            var customBlacklist = CommonBlacklist.CreateFromTextFile("blacklist.txt");

            var inputType = ComponentFactory.DetectInputType(text);
            var progress = new FakeProgress();
            var terms = ComponentFactory.CreateExtractor(inputType, text, progress);
            var stemmer = ComponentFactory.CreateWordStemmer(true);

            var words = terms
                .Filter(blacklist)
                .Filter(customBlacklist)
                .CountOccurences()
                .ToList();

            return 
                words
                    .GroupByStem(stemmer)
                    .SortByOccurences()
                    //.Where(z => z.Occurrences > 3)
                    .Cast<IWord>()
                ;
        }

        private class FakeProgress : IProgressIndicator
        {
            public int Maximum { get; set; }
            public void Increment(int value)
            {
                return;
            }
        }
    }
}