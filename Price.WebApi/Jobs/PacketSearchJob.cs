using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using Norm.MedPrep;
using Price.WebApi.Logic.Packet;
using Price.WebApi.Models;
using Price.WebApi.Models.Packet;
using PricePipeCore;
using Quartz;

namespace Price.WebApi.Jobs
{
    public class PacketSearchJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //var key = context.JobDetail.Key;
            //var dataMap = context.JobDetail.JobDataMap;
            //var jobSays = dataMap.GetString("id");
            //var msgString = $"Instance {key} of DumbJob says: {jobSays}";
            //Debug.WriteLine(msgString);
            //Console.Error.WriteLine(msgString);
            var delimiter = SimpleSearcher.ListDelimiter.FirstOrDefault();
            Debug.WriteLine($"{nameof(PacketSearchJob)}---{DateTime.Now}----------------------------------------------");

            var searchItemDtos = SearchItemStore.Dictionary.Values.Where(z => z.Status == TaskStatus.NotProcessed).ToList();
            foreach (var searchItem in searchItemDtos)
            {

                searchItem.Status = TaskStatus.Inprocess;
                Debug.WriteLine($"{searchItem.Id}--------------------------------------------------------------------");
                try
                {
                    var splitResult = searchItem.Name.Split(SimpleSearcher.ListDelimiter, StringSplitOptions.RemoveEmptyEntries);
                    if (searchItem.SearchItem.Norm != null && searchItem.SearchItem.Norm.Equals("лек_средства:основной"))
                    {
                        var name = string.Join(" ", splitResult.Select(p => p.Trim()));
                        var firstWords = string.Empty;
                        var lekForm = string.Empty;
                        var upak = string.Empty;
                        var dozValue = string.Empty;
                        var dozKey = string.Empty;
                        foreach (var property in searchItem.SearchItem.SearchItemProperties)
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
                        var syn = string.Empty;
                        if (searchItem.SearchItem.Syn != null && searchItem.SearchItem.Syn.Length > 0)
                        {
                            syn = firstWords + "," + string.Join(",", searchItem.SearchItem.Syn.Select(p => p.Trim()));
                        }

                        searchItem.Content =
                            Mapper.Map<IEnumerable<ContentDto>>(new PharmacySearcher(searchItem.Source).Search(name, firstWords, lekForm, upak, dozValue, dozKey, syn: syn));
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
                            searchItem.Content =
                                Mapper.Map<IEnumerable<ContentDto>>(
                                    new SimpleSearcher(searchItem.Source).MaybeSearch(must, should, string.Empty));

                        }
                    }
                    searchItem.ProcessedAt = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    searchItem.Status = TaskStatus.Ok;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                    searchItem.ProcessedAt = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    searchItem.Status = TaskStatus.Error;
                }

            }
            var tasks = SearchPacketTaskStore.Dictionary.Values.Where(z => z.ProcessedAt == null).ToList();
            foreach (var searchPacketTaskDto in tasks)
            {
                searchPacketTaskDto.UpdateStatistics();
                if (searchPacketTaskDto.TotalCount == searchPacketTaskDto.ProcessedCount)
                    searchPacketTaskDto.ProcessedAt = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            }
        }
    }
}