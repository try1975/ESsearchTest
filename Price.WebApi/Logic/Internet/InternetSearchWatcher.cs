using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Common.Dto.Model;
using Common.Dto.Model.Packet;
using Price.WebApi.Logic.Interfaces;
using Price.WebApi.Logic.Packet;

namespace Price.WebApi.Logic.Internet
{
    /// <summary>
    /// 
    /// </summary>
    public class InternetSearchWatcher : IInternetSearchWatcher
    {
        private static readonly Regex RegexObj;
        private ISearchItemStore _searchItemStore;

        static InternetSearchWatcher()
        {
            RegexObj = new Regex(@"_([0-9,A-z]+)\.csv",
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        }

        /// <summary>
        /// 
        /// </summary>
        public InternetSearchWatcher(ISearchItemStore searchItemStore)
        {
            _searchItemStore = searchItemStore;
            var watcher = new FileSystemWatcher
            {
                Path = AppGlobal.InternetSearchResultPath,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                               | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = "*.csv"
            };
            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed) return;
            try
            {
                if (string.IsNullOrEmpty(e.FullPath) || !RegexObj.IsMatch(e.FullPath)) return;

                var searchItemDtoKey = $"{RegexObj.Match(e.FullPath).Groups[1].Value}";
                //var searchItemDto = SearchItemStore.Dictionary.Values.FirstOrDefault(z => z.Key.Equals(searchItemDtoKey, StringComparison.OrdinalIgnoreCase) && z.Status == TaskStatus.InProcess);
                var searchItemDto = _searchItemStore.GetOneByKeyInProcess(searchItemDtoKey);

                if (searchItemDto == null) return;
                var listContentDto = ReadContentDtosFromFile(e.FullPath);
                if (searchItemDto.Content == null)
                {
                    searchItemDto.SetContent(listContentDto);
                }
                else
                {
                    searchItemDto.SetContent(searchItemDto.Content.Where(z => z.PriceType != PriceType.Check));
                    searchItemDto.SetContent(searchItemDto.Content.Concat(listContentDto));
                }
            }
            catch (ArgumentException exception)
            {
                Logger.Log.Error($"{exception}");
            }
        }

        private static IEnumerable<ContentDto> ReadContentDtosFromFile(string fullPath)
        {
            var list = new List<ContentDto>();
            // read from scv to list
            try
            {
                list = File.ReadAllLines(fullPath, Encoding.Default)
                     //.Skip(1)
                     .Select(ContentDto.FromAnalystCsv)
                     .ToList();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                //throw;
            }
            return list;
        }
    }
}