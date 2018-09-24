using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Common.Dto;
using Topol.UseApi.Interfaces.Common;

namespace Topol.UseApi.Data.Common
{
    public abstract class TypedDataMànager<T, TK> : ITypedDataMànager<T, TK> where T : class, IDto<TK>
    {
        protected readonly string EndPoint;
        protected readonly HttpClient HttpClient;

        protected TypedDataMànager(string endPoint)
        {
            var baseApi = ConfigurationManager.AppSettings["BaseApi"];
            var token = ConfigurationManager.AppSettings["ExternalToken"];
            EndPoint = $"{baseApi}{endPoint}/";

            HttpClient = new HttpClient(new LoggingHandler());
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
        }

        public virtual async Task<IEnumerable<T>> GetItems()
        {
            using (var response = await HttpClient.GetAsync($"{EndPoint}"))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsAsync<IEnumerable<T>>();
                //#if DEBUG
                //using (var stream = await response.Content.ReadAsStreamAsync())
                //{
                //    stream.Position = 0;
                //    using (var reader = new StreamReader(stream, Encoding.UTF8))
                //    {
                //        Debug.WriteLine(reader.ReadToEnd());
                //    }
                //}
                //#endif
                return result;
            }
        }

        public async Task<T> GetItem(TK id)
        {
            using (var response = await HttpClient.GetAsync($"{EndPoint}{id}"))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsAsync<T>();
                return result;
            }
        }

        public async Task<T> PostItem(T item)
        {
            using (var response = await HttpClient.PostAsJsonAsync($"{EndPoint}", item))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsAsync<T>();
                return result;
            }
        }

        public async Task<T> PutItem(T item)
        {
            using (var response = await HttpClient.PutAsJsonAsync($"{EndPoint}", item))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsAsync<T>();
                return result;
            }
        }

        public async Task<bool> DeleteItem(T item)
        {
            //HttpRequestMessage request = new HttpRequestMessage
            //{
            //    Content = new StringContent("[YOUR JSON GOES HERE]", Encoding.UTF8, "application/json"),
            //    Method = HttpMethod.Delete,
            //    RequestUri = new Uri("[YOUR URL GOES HERE]")
            //};
            //httpClient.SendAsync(request);

            using (var response = await HttpClient.DeleteAsJsonAsync($"{EndPoint}", item))
            {
                return response.IsSuccessStatusCode;
            }

            //using (var response = await _httpClient.DeleteAsync($"{_endPoint}{item.Id}"))
            //{
            //    return response.IsSuccessStatusCode;
            //}
        }
    }
}