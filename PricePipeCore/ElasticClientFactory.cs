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
            ClientsPool.TryGetValue(source, out var elasticClient);
            if (elasticClient != null) return ClientsPool[source];
            string address;
            string defaultIndex;
            string userName;
            string password;
            if (source.Equals(nameof(ElacticIndexName.Md5), StringComparison.InvariantCultureIgnoreCase) || string.IsNullOrEmpty(source))
            {
                address = AppSettings.Md5Host;
                defaultIndex = AppSettings.DefaultIndex.ToLower();
                userName = AppSettings.Md5UserName;
                password = AppSettings.Md5Password;
            }
            else if (source.Equals(nameof(ElacticIndexName.Gz), StringComparison.InvariantCultureIgnoreCase))
            {
                address = AppSettings.GzHost;
                defaultIndex = nameof(ElacticIndexName.Gz).ToLower();
                userName = AppSettings.GzUserName;
                password = AppSettings.GzPassword;
            }
            else
            {
                address = AppSettings.OtherHost;
                defaultIndex = source.Trim().ToLower();
                userName = AppSettings.OtherUserName;
                password = AppSettings.OtherPassword;
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