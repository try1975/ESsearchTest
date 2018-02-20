using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Common.Dto.Model.Packet;
using Norm.MedPrep;
using Price.Db.Entities.Entities;
using Price.Db.MysSql;
using Price.Db.MysSql.QueryProcessors;
using PriceCommon.Model;
using PricePipeCore;

namespace Price.WebApi.Logic.Packet
{
    public static class ElasticSeacherAndDbWriter
    {
        public static void Execute(SearchItemParam searchItem, string allSources, string searchItemId)
        {
            var sources = allSources.ToLower().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var dbContext = new PriceContext();
            var contentQuery = new ContentQuery(dbContext);
            foreach (var source in sources)
            {
                try
                {
                    IEnumerable<Content> listContent = null;
                    var delimiter = SimpleSearcher.ListDelimiter.FirstOrDefault();
                    var splitResult = searchItem.Name.Split(SimpleSearcher.ListDelimiter,
                        StringSplitOptions.RemoveEmptyEntries);
                    if (searchItem.Norm != null && searchItem.Norm.ToLower().Contains("лек_средства:основной"))
                    {
                        var name = string.Join(" ", splitResult.Select(p => p.Trim()));
                        var firstWords = string.Empty;
                        var lekForm = string.Empty;
                        var upak = string.Empty;
                        var dozValue = string.Empty;
                        var dozKey = string.Empty;
                        if (searchItem.SearchItemProperties != null)
                        {
                            foreach (var property in searchItem.SearchItemProperties)
                            {
                                if (property.Key.ToLower().Contains("мнн")) firstWords = property.Value.Trim();
                                if (property.Key.ToLower().Contains("форма выпуска"))
                                {
                                    var lekFormNorm = new LekFormNorm { InitialName = property.Value.Trim() };
                                    lekForm = lekFormNorm.NormResult;
                                }
                                if (property.Key.ToLower().Contains("дозировка"))
                                {
                                    var dozNorm = new DozNorm() { InitialName = property.Value.Trim() };
                                    dozValue = dozNorm.DozValue;
                                    dozKey = dozNorm.DozKey;
                                }
                                if (property.Key.ToLower().Contains("фасовка"))
                                {
                                    var upakNorm = new UpakNorm(name) { InitialName = property.Value.Trim() };
                                    upak = upakNorm.NormResult;
                                }
                                //if (property.Key.ToLower().Contains("ед.объём"))
                                //{
                                //    var upakNorm = new UpakNorm(name) { InitialName = property.Value.Trim() };
                                //    upak = upakNorm.NormResult;
                                //}
                            }
                        }
                        var syn = string.Empty;
                        if (searchItem.Syn != null && searchItem.Syn.Length > 0)
                        {
                            syn = firstWords + "," + string.Join(",", searchItem.Syn.Select(p => p.Trim()));
                        }

                        var pharmacySearcher = new PharmacySearcher(source);
                        listContent = pharmacySearcher.Search(name, firstWords, lekForm, upak, dozValue, dozKey, syn);
                        contentQuery.InsertEntities(listContent.Select(contentDto => new ContentEntity()
                            {
                                ElasticId = contentDto.Id,
                                Name = contentDto.Name,
                                Price = contentDto.Price,
                                Uri = contentDto.Uri,
                                SearchItemId = searchItemId,
                                CollectedAt = contentDto.CollectedAt,
                                Okpd2 = contentDto.Okpd2
                            })
                            .ToList()
                        );
                    }
                    else
                    {
                        if (splitResult.Length > 0)
                        {
                            var must = splitResult[0].Trim().Replace(" ", delimiter);
                            var i = 1;
                            if (splitResult.Length > 1)
                            {
                                must = $"{must}{delimiter}{splitResult[1].Trim().Replace(" ", delimiter)}";
                                i = 2;
                            }
                            if (splitResult.Length > 2)
                            {
                                must = $"{must}{delimiter}{splitResult[2].Trim().Replace(" ", delimiter)}";
                                i = 3;
                            }
                            var should = string.Empty;
                            if (splitResult.Length > i)
                            {
                                should = string.Join(delimiter, splitResult.Skip(i).Select(p => p.Trim()));
                            }
                            var simpleSearcher = new SimpleSearcher(source);
                            listContent = simpleSearcher.MaybeSearch(must, should, string.Empty);
                            contentQuery.InsertEntities(listContent.Select(contentDto => new ContentEntity()
                                {
                                    ElasticId = contentDto.Id,
                                    Name = contentDto.Name,
                                    Price = contentDto.Price,
                                    Uri = contentDto.Uri,
                                    SearchItemId = searchItemId,
                                    CollectedAt = contentDto.CollectedAt,
                                    Okpd2 = contentDto.Okpd2
                                })
                                .ToList()
                            );
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    Logger.Log.Error($"{e}");
                }
            }
        }
    }
}