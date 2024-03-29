﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Topol.UseApi.Data.Common
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, string requestUri,
            T data)
        {
            return httpClient.SendAsync(
                new HttpRequestMessage(HttpMethod.Delete, requestUri) {Content = Serialize(data)});
        }

        public static Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, string requestUri,
            T data, CancellationToken cancellationToken)
        {
            return httpClient.SendAsync(
                new HttpRequestMessage(HttpMethod.Delete, requestUri) {Content = Serialize(data)}, cancellationToken);
        }

        public static Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, Uri requestUri, T data)
        {
            return httpClient.SendAsync(
                new HttpRequestMessage(HttpMethod.Delete, requestUri) {Content = Serialize(data)});
        }

        public static Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, Uri requestUri, T data,
            CancellationToken cancellationToken)
        {
            return httpClient.SendAsync(
                new HttpRequestMessage(HttpMethod.Delete, requestUri) {Content = Serialize(data)}, cancellationToken);
        }

        private static HttpContent Serialize(object data)
        {
            return new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }
    }
}