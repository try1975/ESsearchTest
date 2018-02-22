using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Dto.Model.NewApi;
using Newtonsoft.Json;
using Quartz;
using TaskStatus = PriceCommon.Enums.TaskStatus;

namespace Price.WebApi.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckInternetSearchJob : IJob
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            const string apiSearchitemBycondition = "http://localhost:52620/api/SearchItem/ByCondition";
            const string apiSearchitemSetCompleted = "http://localhost:52620/api/SearchItem/SetCompleted";
            var dtos = GetSearchItemsInProgress(apiSearchitemBycondition).Result;
            foreach (var dto in dtos)
            {
                if (!string.IsNullOrEmpty(dto.InternetSessionId) && IsInternetSessionCompleted(dto.InternetSessionId))
                {
                    PostSearchItemCompleted($"{apiSearchitemSetCompleted}/{dto.Id}");
                }
            }
        }

        private static bool IsInternetSessionCompleted(string sessionId)
        {
            using (var client = new WebClient())
            {
                var en = Encoding.UTF8;
                var uri = new Uri($"http://localhost:8080/datasnap/rest/TAnalyst_Service/GetSessionProgress/{sessionId}");
                var result = client.DownloadData(uri);
                var text = en.GetString(result);
                var o = JsonConvert.DeserializeObject<AnalystSessionProgress>(text);
                return o.result[0].percent == "100";
            }
        }

        private async Task<IEnumerable<SearchItemExtDto>> GetSearchItemsInProgress(string uri)
        {
            using (var client = new HttpClient())
            {
                var searchItemCondition = new SearchItemCondition()
                {
                    Status = TaskStatus.InProcess,
                    IsInternet = true
                };

                var stringContent = new StringContent(JsonConvert.SerializeObject(searchItemCondition), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, stringContent).ConfigureAwait(false);

                //var str = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var content = await response.Content
                    .ReadAsAsync<IEnumerable<SearchItemExtDto>>().ConfigureAwait(false);

                return content;
            }
        }
        private async void PostSearchItemCompleted(string uri)
        {
            using (var client = new HttpClient())
            {

                var stringContent = new StringContent("", Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, stringContent).ConfigureAwait(false);

                //var str = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                Debug.WriteLine($"{response.StatusCode}");
            }
        }
    }



    public class AnalystSessionProgress
    {
        public AnalystSessionProgressResult[] result { get; set; }
    }

    public class AnalystSessionProgressResult
    {
        public string percent { get; set; }
    }


}