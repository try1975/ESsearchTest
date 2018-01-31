using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Common.Dto.Model.Packet;
using Newtonsoft.Json;

namespace Price.WebApi.Logic.Packet
{
    public class SearchItemStore : ISearchItemStore
    {
        private readonly ConcurrentDictionary<int, SearchItemDto> _dictionary;

        public SearchItemStore()
        {
            var storePath = PathService.GetSearchItemStorePath();
            if (File.Exists(storePath))
            {
                _dictionary =
                    JsonConvert.DeserializeObject<ConcurrentDictionary<int, SearchItemDto>>(
                        File.ReadAllText(storePath));
                Logger.Log.Error($"Load {nameof(SearchItemStore)}");
                foreach (var dto in _dictionary.Values)
                {
                    if (dto.InCash(AppGlobal.CashSeconds))
                    {
                        dto.Changed += SaveDictionary;
                        dto.Ended += KillAnalystProcess;
                    }
                    else
                    {
                        SearchItemDto ignored;
                        _dictionary.TryRemove(dto.Key.GetHashCode(), out ignored);
                    }
                }
            }
            else
            {
                _dictionary = new ConcurrentDictionary<int, SearchItemDto>();
                Logger.Log.Error($"Create {nameof(SearchItemStore)}");
            }
        }

        public SearchItemDto Get(string aKey)
        {
            var key = aKey.GetHashCode();
            if (_dictionary.ContainsKey(key)) return _dictionary[key];
            var dto = new SearchItemDto();
            dto.Changed += SaveDictionary;
            dto.Ended += KillAnalystProcess;
            _dictionary[key] = dto;
            return dto;
        }

        private void SaveDictionary(object sender, EventArgs e)
        {
            var storePath = PathService.GetSearchItemStorePath();
            File.WriteAllText(storePath,
                JsonConvert.SerializeObject(_dictionary, Formatting.Indented));
            //Logger.Log.Error($"Save to file {nameof(SearchItemStore)}");
        }

        private static void KillAnalystProcess(object sender, EventArgs e)
        {
            var dto = sender as SearchItemDto;
            if (dto == null) return;
            if (dto.AnalystProcessId <= 0) return;
            try
            {
                var p = Process.GetProcessById(dto.AnalystProcessId);
                p.Kill();
            }
            catch (Exception exception)
            {
                Logger.Log.Error($"Process kill {exception}");
                throw;
            }
        }


        /// <inheritdoc />
        public int GetInProcessCount()
        {
            return _dictionary.Values.Count(x => x.Status == TaskStatus.InProcess);
        }

        public IEnumerable<SearchItemDto> TakeCountInQueue(int count)
        {
            return _dictionary.Values.Where(x => x.Status == TaskStatus.InQueue).Take(count).ToList();
        }

        public SearchItemDto GetOneByKeyInProcess(string aKey)
        {
            return _dictionary.Values.FirstOrDefault(x =>
                x.Key.Equals(aKey, StringComparison.OrdinalIgnoreCase)
                && x.Status == TaskStatus.InProcess);
        }
    }
}