using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using Common.Dto.Model;
using Common.Dto.Model.Packet;
using Norm.MedPrep;
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
            try
            {
                var delimiter = SimpleSearcher.ListDelimiter.FirstOrDefault();
                var splitResult = searchItem.Name.Split(SimpleSearcher.ListDelimiter, StringSplitOptions.RemoveEmptyEntries);
                if (searchItem.Norm != null && searchItem.Norm.Equals("лек_средства:основной"))
                {
                    var name = string.Join(" ", splitResult.Select(p => p.Trim()));
                    var firstWords = string.Empty;
                    var lekForm = string.Empty;
                    var upak = string.Empty;
                    var dozValue = string.Empty;
                    var dozKey = string.Empty;
                    foreach (var property in searchItem.SearchItemProperties)
                    {
                        if (property.Key.Equals("МНН")) firstWords = property.Value.Trim();
                        if (property.Key.Equals("Форма выпуска"))
                        {
                            var lekFormNorm = new LekFormNorm {InitialName = property.Value.Trim()};
                            lekForm = lekFormNorm.NormResult;
                        }
                        if (property.Key.Equals("Дозировка"))
                        {
                            var dozNorm = new DozNorm() {InitialName = property.Value.Trim()};
                            dozValue = dozNorm.DozValue;
                            dozKey = dozNorm.DozKey;
                        }
                        if (property.Key.Equals("Фасовка"))
                        {
                            var upakNorm = new UpakNorm(name) {InitialName = property.Value.Trim()};
                            upak = upakNorm.NormResult;
                        }
                    }
                    var syn = string.Empty;
                    if (searchItem.Syn != null && searchItem.Syn.Length > 0)
                    {
                        syn = firstWords + "," + string.Join(",", searchItem.Syn.Select(p => p.Trim()));
                    }

                    searchItemDto.Content =
                        Mapper.Map<IEnumerable<ContentDto>>(new PharmacySearcher(searchItemDto.Source).Search(name, firstWords, lekForm, upak,
                            dozValue, dozKey, syn: syn));
                }
                else
                {
                    if (splitResult.Length > 0)
                    {
                        var must = splitResult[0].Trim();
                        var should = string.Empty;
                        if (splitResult.Length > 1)
                        {
                            should = string.Join(delimiter, splitResult.Skip(1).Select(p => p.Trim()));
                        }
                        searchItemDto.Content =
                            Mapper.Map<IEnumerable<ContentDto>>(
                                new SimpleSearcher(searchItemDto.Source).MaybeSearch(must, should, string.Empty));
                    }
                }
                searchItemDto.ProcessedAt = (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                searchItemDto.Status = TaskStatus.Ok;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Logger.Log.Error($"{e}");
                searchItemDto.ProcessedAt = (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                searchItemDto.Status = TaskStatus.Error;
            }
        }
    }
}