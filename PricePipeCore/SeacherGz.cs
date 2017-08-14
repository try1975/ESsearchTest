using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Nest;
using PriceCommon.Model.Gz;
using PriceCommon.Norm;

namespace PricePipeCore
{
    public class SeacherGz
    {
        private readonly ElasticClient _elasticClient;

        private readonly int _maxTake;
        private INorm _norm;
        private List<Content> _founded;

        public SeacherGz(string defaultIndex, string address, string userName, string password)
        {
            _maxTake = 200;
            try
            {
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
                _elasticClient = new ElasticClient(connectionSettings);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                throw;
            }
        }

        public IEnumerable<Content> Search(string name)
        {
            return new List<Content>();
        }
    }
}