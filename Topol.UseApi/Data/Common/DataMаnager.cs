using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common.Dto;
using Common.Dto.Model;
using Common.Dto.Model.NewApi;
using Common.Dto.Model.Packet;
using PriceCommon.Model;
using Topol.UseApi.Administration;
using Topol.UseApi.Interfaces.Common;

namespace Topol.UseApi.Data.Common
{
    public class DataMаnager : IDataMаnager
    {
        private readonly string _endpointPostPacketAsync;
        private readonly string _endpointSearchItemByCondition;
        private readonly string _endpointSearchItemStatus;
        private readonly string _endpointSearchItemContent;
        private readonly string _endpointMaybe;
        private readonly string _endpointOkpd2;
        private readonly string _endpointInternet;
        private readonly string _endpointContentItem;
        private readonly string _endpointSearchItem;
        private readonly string _endpointMove;
        private readonly string _endpointGetSellerCount;
        private readonly string _endpointGetSourceCounts;
        private readonly string _endpointGzDocs;
        private readonly HttpClient _apiHttpClient;

        public DataMаnager()
        {
            #region Endpoints

            var baseApi = ConfigurationManager.AppSettings["BaseApi"];
            var token = ConfigurationManager.AppSettings["ExternalToken"];
            var gzApi = ConfigurationManager.AppSettings["GzApi"];

            _endpointPostPacketAsync = $"{baseApi}api/packet/";
            _endpointSearchItemByCondition = $"{baseApi}api/searchitem/bycondition/";
            _endpointSearchItemStatus = $"{baseApi}api/searchitem/status/";
            _endpointSearchItemContent = $"{baseApi}api/searchitem/content/";
            _endpointMaybe = $"{baseApi}api/simpleprice/maybe/";
            _endpointOkpd2 = $"{baseApi}api/common/okpd2reverse/";
            _endpointInternet = $"{baseApi}api/internet/";
            _endpointContentItem = $"{baseApi}api/contentitem/";
            _endpointSearchItem = $"{baseApi}api/searchitem/";
            _endpointMove = $"{baseApi}api/searchitem/move/";
            _endpointGetSellerCount = $"{baseApi}api/simpleprice/sellerCount/";
            _endpointGetSourceCounts = $"{baseApi}api/common/counts/";

            _endpointGzDocs = $"{gzApi}api/docs/";
            #endregion

            _apiHttpClient = new HttpClient(new LoggingHandler());
            _apiHttpClient.DefaultRequestHeaders.Accept.Clear();
            _apiHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue($"{token}");
            _apiHttpClient.DefaultRequestHeaders.Add(CustomHeaders.UserName, $"{CurrentUser.Login}");
        }

        public async Task<List<SearchItemHeaderDto>> PostPacketAsync(List<SearchItemParam> searchItemsParam, string source = "", string keywords="")
        {
            using (var response = await _apiHttpClient.PostAsJsonAsync($"{_endpointPostPacketAsync}?source={source}&keywords={keywords}", searchItemsParam))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsAsync<List<SearchItemHeaderDto>>();
                return result;
            }
        }

        public async Task<List<SearchItemHeaderDto>> GetByConditionAsync(SearchItemCondition searchItemCondition)
        {
            using (var response = await _apiHttpClient.PostAsJsonAsync($"{_endpointSearchItemByCondition}", searchItemCondition))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsAsync<List<SearchItemHeaderDto>>();
                return result;
            }
        }

        public async Task<SearchItemHeaderDto> GetSearchItemStatus(string id)
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointSearchItemStatus}{id}"))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsAsync<SearchItemHeaderDto>();
                return result;
            }
        }

        public async Task<List<ContentExtDto>> GetSearchItemContent(string id, string filterValue)
        {
            string url;
            if (string.IsNullOrWhiteSpace(filterValue))
            {
                url = $"{_endpointSearchItemContent}{id}";
            }
            else
            {
                url = $"{_endpointSearchItemContent}filtered/{id}?$filter={filterValue}"; 
            }
            using (var response = await _apiHttpClient.GetAsync(url))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsAsync<List<ContentExtDto>>();
                return result;
            }
        }

        public async Task<bool> PostSearchItemBreak(string id)
        {
            using (var response = await _apiHttpClient.PostAsync($"{_endpointSearchItem}break/{id}", null))
            {
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> PostSearchItemChecked(string id)
        {
            using (var response = await _apiHttpClient.PostAsync($"{_endpointSearchItem}checked/{id}", null))
            {
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> PostContentItemChecked(string id, string elasticId)
        {
            using (var response = await _apiHttpClient.PostAsync($"{_endpointContentItem}checked/{id}?elasticId={elasticId}", null))
            {
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> PostContentItemNotChecked(string id, string elasticId)
        {
            using (var response = await _apiHttpClient.PostAsync($"{_endpointContentItem}notchecked/{id}?elasticId={elasticId}", null))
            {
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> PostContentItemPrice(string id, string elasticId, string price)
        {
            using (var response = await _apiHttpClient.PostAsync($"{_endpointContentItem}setprice/{id}?elasticId={elasticId}&price={price}", null))
            {
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> DeleteSearchItem(string id)
        {
            using (var response = await _apiHttpClient.DeleteAsync($"{_endpointSearchItem}{id}"))
            {
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> DeleteContentItem(string id, string elasticId)
        {
            using (var response = await _apiHttpClient.DeleteAsync($"{_endpointContentItem}{id}?elasticId={elasticId}"))
            {
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<IEnumerable<ContentDto>> GetMaybe(string must = "", string should = "", string mustNot = "", string source = "")
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointMaybe}?must={must}&should={should}&mustNot={mustNot}&source={source}"))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsAsync<IEnumerable<ContentDto>>();
                return result;
            }
        }

        public async Task<IEnumerable<Okpd2Reverse>> GetOkpd2Reverse(string text)
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointOkpd2}?text={text}"))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsAsync<IEnumerable<Okpd2Reverse>>();
                return result;
            }
        }

        public async Task Post2InternetIndex(IEnumerable<BasicContentDto> list)
        {
            using (var response = await _apiHttpClient.PostAsJsonAsync($"{_endpointInternet}", list))
            {
                if (!response.IsSuccessStatusCode) return;
                await response.Content.ReadAsAsync<SearchPacketTaskDto>();
            }
        }

        public async Task<SearchItemHeaderDto> MoveResults(List<ContentMoveDto> list, string id, string name, string extId)
        {
            using (var response = await _apiHttpClient.PostAsJsonAsync($"{_endpointMove}?{nameof(id)}={id}&{nameof(name)}={name}&{nameof(extId)}={extId}", list))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsAsync<SearchItemHeaderDto>();
                return result;
            }
        }

        public async Task<int> GetSellerCount()
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointGetSellerCount}"))
            {
                if (!response.IsSuccessStatusCode) return 0;
                var result = await response.Content.ReadAsAsync<int>();
                return result;
            }
        }

        public async Task<Dictionary<string, string>> GetSourceCounts()
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointGetSourceCounts}"))
            {
                if (!response.IsSuccessStatusCode) return new Dictionary<string, string>();
                var result = await response.Content.ReadAsAsync<Dictionary<string, string>>();
                return result;
            }
        }

        public async Task<Dictionary<string, string>> GetGzDocsAcync(string regNum)
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointGzDocs}{regNum}"))
            {
                if (!response.IsSuccessStatusCode) return new Dictionary<string, string>();
                var result = await response.Content.ReadAsAsync<Dictionary<string, string>>();
                return result;
            }
        }

        public Dictionary<string, string> GetGzDocs(string regNum)
        {
            using (var response = _apiHttpClient.GetAsync($"{_endpointGzDocs}{regNum}").Result)
            {
                if (!response.IsSuccessStatusCode) return new Dictionary<string, string>();
                var result = response.Content.ReadAsAsync<Dictionary<string, string>>().Result;
                return result;
            }
        }
    }
}
