using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Monitoring
{
    internal class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler()
            : this(new HttpClientHandler())
        {
        }

        private LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Debug.WriteLine(request.ToString());
            //Console.WriteLine(request.ToString());
            if (request.Content != null)
            {
                var line = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                Debug.WriteLine(line);
                //Console.WriteLine(line);
            }
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);


            return response;
        }
    }
}