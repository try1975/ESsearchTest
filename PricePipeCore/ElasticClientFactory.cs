using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Nest;

namespace PricePipeCore
{
    //Install-Package nest-factory
    public static class ElasticClientFactory
    {
        private static readonly Dictionary<string, ElasticClient> ClientsPool = new Dictionary<string, ElasticClient>();

        public static ElasticClient GetElasticClient(string name = "")
        {
            ElasticClient elasticClient;
            ClientsPool.TryGetValue(name, out elasticClient);
            if (elasticClient != null) return ClientsPool[name];
            string address = null;
            string defaultIndex = null;
            string userName = null;
            string password = null;
            if (name.Equals(nameof(ElacticIndexName.Md5), StringComparison.InvariantCultureIgnoreCase) || string.IsNullOrEmpty(name))
            {
                address = AppSettings.Host;
                defaultIndex = AppSettings.DefaultIndex.ToLower();
                userName = AppSettings.UserName;
                password = AppSettings.Password;
            }
            if (name.Equals(nameof(ElacticIndexName.Gz), StringComparison.InvariantCultureIgnoreCase))
            {
                address = AppSettings.Host;
                defaultIndex = nameof(ElacticIndexName.Gz).ToLower();
                userName = AppSettings.UserName;
                password = AppSettings.Password;
            }
            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(defaultIndex) || string.IsNullOrEmpty(userName) ||
                string.IsNullOrEmpty(password)) return null;
            var connectionSettings = new ConnectionSettings(new Uri(address))
                .OnRequestCompleted(details =>
                {
                    var s = details.RequestBodyInBytes != null
                        ? Encoding.UTF8.GetString(details.RequestBodyInBytes)
                        : null;
                    Debug.WriteLine($"{s}");
                })
                .DisableDirectStreaming()
                .DefaultIndex(defaultIndex)
                .BasicAuthentication(userName, password);
            ClientsPool[name] = new ElasticClient(connectionSettings);
            return ClientsPool[name];
        }
    }
}