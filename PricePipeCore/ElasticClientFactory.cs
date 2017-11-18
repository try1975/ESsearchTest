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

        public static ElasticClient GetElasticClient(string source = "")
        {
            ElasticClient elasticClient;
            ClientsPool.TryGetValue(source, out elasticClient);
            if (elasticClient != null) return ClientsPool[source];
            string address = null;
            string defaultIndex = null;
            string userName = null;
            string password = null;
            if (source.Equals(nameof(ElacticIndexName.Md5), StringComparison.InvariantCultureIgnoreCase) || string.IsNullOrEmpty(source))
            {
                address = AppSettings.Host;
                defaultIndex = AppSettings.DefaultIndex.ToLower();
                userName = AppSettings.UserName;
                password = AppSettings.Password;
            }
            else if (source.Equals(nameof(ElacticIndexName.Gz), StringComparison.InvariantCultureIgnoreCase))
            {
                address = AppSettings.Host;
                defaultIndex = nameof(ElacticIndexName.Gz).ToLower();
                userName = AppSettings.UserName;
                password = AppSettings.Password;
            }
            else
            {
                address = AppSettings.Host;
                defaultIndex = source.Trim().ToLower();
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
            ClientsPool[source] = new ElasticClient(connectionSettings);
            return ClientsPool[source];
        }
    }
}