using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Price.WebApi.Logic
{
    public static class AnalistService
    {
        public static void TerminateSession(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId)) return;
            using (var client = new WebClient())
            {
                var en = Encoding.UTF8;
                var uri = new Uri($"{AppGlobal.InternetSearchHost}/TerminateSession/{sessionId}");
                var result = client.DownloadData(uri);
                var text = en.GetString(result);
                //var o = JsonConvert.DeserializeObject<AnalystSessionProgress>(text);
                //return o.result[0].percent == "100";
            }
        }

        public static bool IsInternetSessionCompleted(string sessionId)
        {
            using (var client = new WebClient())
            {
                var en = Encoding.UTF8;
                var uri = new Uri($"{AppGlobal.InternetSearchHost}/GetSessionProgress/{sessionId}");
                var result = client.DownloadData(uri);
                var text = en.GetString(result);
                var o = JsonConvert.DeserializeObject<AnalystSessionProgress>(text);
                return o.result[0].percent == "100";
            }
        }

        private class AnalystSessionProgress
        {
            public AnalystSessionProgressResult[] result { get; set; }
        }

        private class AnalystSessionProgressResult
        {
            public string percent { get; set; }
        }
    }
}