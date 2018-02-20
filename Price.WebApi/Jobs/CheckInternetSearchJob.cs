using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Common.Dto.Model.NewApi;
using Newtonsoft.Json;
using Quartz;
using TaskStatus = PriceCommon.Enums.TaskStatus;

namespace Price.WebApi.Jobs
{
    public class CheckInternetSearchJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Debug.WriteLine($"{DateTime.Now} - {nameof(CheckInternetSearchJob)}");
            const string uri = "http://localhost:52620/api/SearchItem/ByCondition";
            var dtos = GetCarsAsync(uri).Result;
            foreach (var dto in dtos)
            {
                Debug.WriteLine($"{dto.InternetSessionId}");
            }
        }

        private async Task<IEnumerable<SearchItemExtDto>> GetCarsAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                var searchItemCondition = new SearchItemCondition()
                {
                    Status = TaskStatus.InProcess,
                    IsInternet = true
                };

                var stringContent = new StringContent(JsonConvert.SerializeObject(searchItemCondition));
                //var m = new HttpRequestMessage();
                //m.Content = stringContent;
                //m.Headers.Add("Accept", "application/json");
                //m.Headers.Add("COntent-Type", "application/json");


                var response = await client.PostAsync(uri, stringContent, new JsonMediaTypeFormatter()).ConfigureAwait(false);

                var str = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var content = await response.Content
                    .ReadAsAsync<IEnumerable<SearchItemExtDto>>().ConfigureAwait(false);
 
                return content;
            }
        }
    }
}