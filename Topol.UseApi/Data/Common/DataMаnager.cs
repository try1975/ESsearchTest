﻿using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common.Dto.Model;
using Common.Dto.Model.NewApi;
using Common.Dto.Model.Packet;
using PriceCommon.Model;
using Topol.UseApi.Interfaces.Common;

namespace Topol.UseApi.Data.Common
{
    public class DataMаnager : IDataMаnager
    {
        private readonly string _endpointPostPacketAsync;
        private readonly string _endpointSearchItemStatus;
        private readonly string _endpointSearchItemContent;
        private readonly string _endpointMaybe;
        private readonly string _endpointOkpd2;
        private readonly string _endpointInternet;
        private readonly HttpClient _apiHttpClient;

        public DataMаnager()
        {
            #region Endpoints

            var baseApi = ConfigurationManager.AppSettings["BaseApi"];
            var token = ConfigurationManager.AppSettings["ExternalToken"];

            //_endpointPostPacketAsync = $"{baseApi}api/simpleprice/packet/";
            _endpointPostPacketAsync = $"{baseApi}api/packet/";
            _endpointSearchItemStatus = $"{baseApi}api/searchitem/status/";
            _endpointSearchItemContent = $"{baseApi}api/searchitem/content/";
            _endpointMaybe = $"{baseApi}api/simpleprice/maybe/";
            _endpointOkpd2 = $"{baseApi}api/common/okpd2reverse/";
            _endpointInternet = $"{baseApi}api/internet/";

            #endregion

            _apiHttpClient = new HttpClient(new LoggingHandler());
            _apiHttpClient.DefaultRequestHeaders.Accept.Clear();
            _apiHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
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

        public async Task<SearchItemHeaderDto> GetSearchItemStatus(string id)
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointSearchItemStatus}{id}"))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsAsync<SearchItemHeaderDto>();
                return result;
            }
        }

        public async Task<List<ContentExtDto>> GetSearchItemContent(string id)
        {
            using (var response = await _apiHttpClient.GetAsync($"{_endpointSearchItemContent}{id}"))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsAsync<List<ContentExtDto>>();
                return result;
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
    }
}
