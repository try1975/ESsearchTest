using Common.Dto.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Monitoring
{
    public class MonitoringScheduleDataManager
    {
        private readonly string _apiMonitoringSchedule;
        private readonly HttpClient _httpClient;

        public MonitoringScheduleDataManager()
        {
            #region Endpoints

            var baseApi = ConfigurationManager.AppSettings["BaseApi"];
            var token = ConfigurationManager.AppSettings["ExternalToken"];
            _apiMonitoringSchedule = $"{baseApi}monitoring/schedule";

            #endregion

            _httpClient = new HttpClient(new LoggingHandler());
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
        }

        public async Task<IEnumerable<MonitoringScheduleDto>> GetMonitoringSchedules()
        {
            using (var response = await _httpClient.GetAsync($"{_apiMonitoringSchedule}"))
            {
                if (!response.IsSuccessStatusCode) return null;
                var result = await response.Content.ReadAsAsync<List<MonitoringScheduleDto>>();
                return result;
            }
        }
    }
}
