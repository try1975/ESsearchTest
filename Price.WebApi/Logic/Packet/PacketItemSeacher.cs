﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using Common.Dto.Model;
using Common.Dto.Model.Packet;
using Norm.MedPrep;
using Price.WebApi.Logic.Internet;
using PriceCommon.Model;
using PricePipeCore;

namespace Price.WebApi.Logic.Packet
{
    /// <summary>
    /// 
    /// </summary>
    public class PacketItemSeacher
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchItemDto"></param>
        public static void Search(SearchItemParam searchItem, SearchItemDto searchItemDto)
        {
            var sources = searchItemDto.Source.ToLower().Split(',');

            foreach (var source in sources)
            {
                if (source.Contains("internet"))
                {
                    InternetSearcher.Search(searchItem, searchItemDto);
                    continue;
                    //break;
                }

                try
                {
                    IEnumerable<Content> listContent = null;
                    var delimiter = SimpleSearcher.ListDelimiter.FirstOrDefault();
                    var splitResult = searchItem.Name.Split(SimpleSearcher.ListDelimiter,
                        StringSplitOptions.RemoveEmptyEntries);
                    if (searchItem.Norm != null && searchItem.Norm.Equals("лек_средства:основной"))
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
                                if (property.Key.Equals("МНН")) firstWords = property.Value.Trim();
                                if (property.Key.Equals("Форма выпуска"))
                                {
                                    var lekFormNorm = new LekFormNorm { InitialName = property.Value.Trim() };
                                    lekForm = lekFormNorm.NormResult;
                                }
                                if (property.Key.Equals("Дозировка"))
                                {
                                    var dozNorm = new DozNorm() { InitialName = property.Value.Trim() };
                                    dozValue = dozNorm.DozValue;
                                    dozKey = dozNorm.DozKey;
                                }
                                if (property.Key.Equals("Фасовка"))
                                {
                                    var upakNorm = new UpakNorm(name) { InitialName = property.Value.Trim() };
                                    upak = upakNorm.NormResult;
                                }
                            }
                        }
                        var syn = string.Empty;
                        if (searchItem.Syn != null && searchItem.Syn.Length > 0)
                        {
                            syn = firstWords + "," + string.Join(",", searchItem.Syn.Select(p => p.Trim()));
                        }

                        var pharmacySearcher = new PharmacySearcher(source);
                        listContent = pharmacySearcher.Search(name, firstWords, lekForm, upak, dozValue, dozKey, syn);

                        //// add xpath results
                        //var xpathSearcher = new PharmacySearcher("md_xpath");
                        //var xpathContent = xpathSearcher.Search(name, firstWords, lekForm, upak, dozValue, dozKey, syn);
                        //var enumerable = xpathContent as Content[] ?? xpathContent.ToArray();
                        //if (enumerable.Any())
                        //{
                        //    var contents = listContent as Content[] ?? listContent.ToArray();
                        //    var intersect = contents.Intersect(enumerable, new ContentComparer())
                        //        .Select(z => z.Id).ToList();
                        //    listContent = contents.Where(z => !intersect.Contains(z.Id));
                        //    listContent = listContent.Concat(enumerable);
                        //}
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

                        }
                    }
                    var listContentDto = Mapper.Map<IEnumerable<ContentDto>>(listContent ?? new List<Content>());
                    foreach (var dto in listContentDto)
                    {
                        dto.PriceType = PriceType.Trusted;
                    }
                    if (searchItemDto.Content == null)
                    {
                        searchItemDto.Content = listContentDto;
                    }
                    else
                    {
                        searchItemDto.Content = searchItemDto.Content.Concat(listContentDto);
                    }
                    if (sources.Length == 1)
                    {
                        searchItemDto.ProcessedAt = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                        searchItemDto.Status = TaskStatus.Ok;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    Logger.Log.Error($"{e}");
                    searchItemDto.ProcessedAt = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    searchItemDto.Status = TaskStatus.Error;
                }
            }
        }
    }

    class ContentComparer : IEqualityComparer<Content>
    {
        public bool Equals(Content x, Content y)
        {
            if (x == null) return false;
            if (y == null) return false;
            return x.Id == y.Id;
        }

        public int GetHashCode(Content obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}