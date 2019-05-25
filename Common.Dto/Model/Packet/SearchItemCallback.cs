using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Common.Dto.Model.Packet
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchItemCallback : ISearchItemCallback
    {
        private readonly HttpClient _apiHttpClient;

        public SearchItemCallback()
        {
            _apiHttpClient = new HttpClient();
            _apiHttpClient.DefaultRequestHeaders.Accept.Clear();
            //_apiHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public void FireCallback(string url, string id)
        {
            if(string.IsNullOrWhiteSpace(url)) return;
            var uri = new Uri(url);

            dynamic flexible = new ExpandoObject();
            flexible.id = id;
            var dictionary = (IDictionary<string, object>)flexible;
            var serialized = JsonConvert.SerializeObject(dictionary);

            var content = new StringContent(serialized, Encoding.UTF8, "application/json");
            Debug.WriteLine(serialized);
            var result = _apiHttpClient.PostAsync(uri.AbsoluteUri, content).Result;
            Debug.WriteLine(result.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="id"></param>
        //public void FireCallback(Uri uri, string id)
        //{
        //    if(string.IsNullOrWhiteSpace(uri.AbsoluteUri)) return;
        //    using (var webClient = new WebClient())
        //    {
        //        webClient.Headers.Add("Content-type", "application/x-www-form-urlencoded");
        //        // Создаём коллекцию параметров
        //        // Добавляем необходимые параметры в виде пар ключ, значение
        //        var pars = new NameValueCollection {{nameof(id), id}};

        //        // Посылаем параметры на сервер
        //        // Может быть ответ в виде массива байт
        //        var response = webClient.UploadValues(uri, pars);
        //        Debug.WriteLine("\nResponse received was :\n{0}", Encoding.ASCII.GetString(response));
        //    }
        //}
    }
}