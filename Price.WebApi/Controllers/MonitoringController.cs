using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.Dto.Model;
using Price.WebApi.Maintenance.Interfaces;

namespace Price.WebApi.Controllers
{
    /// <summary>
    ///     Мониторинг цен
    ///  </summary>
    [RoutePrefix("api/Monitoring")]
    public class MonitoringController : ApiController
    {
        private readonly IScheduleApi _scheduleApi;


        public MonitoringController(IScheduleApi scheduleApi )
        {
            _scheduleApi = scheduleApi;

        }


        /// <summary>
        ///  Список мониторинга
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("schedule", Name = nameof(GetMonitoringSchedules) + "Route")]
        public IEnumerable<ScheduleDto> GetMonitoringSchedules()
        {
            Logger.Log.Info($"{nameof(GetMonitoringSchedules)}");
            return  _scheduleApi.GetItems();
        }

        /// <summary>
        /// Элемент мониторинга
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("schedule/{id}", Name = nameof(GetMonitoringSchedule) + "Route")]
        public ScheduleDto GetMonitoringSchedule(int id)
        {
            Logger.Log.Info($"{nameof(GetMonitoringSchedule)}: {id}");
            return _scheduleApi.GetItem(id);
        }

        /// <summary>
        ///  Постановка на мониторинг по расписанию
        /// </summary>
        /// <param name="monitoringScheduleDto">Структура для задания параметров мониторинга</param>
        /// <returns>Возвращаемая структура</returns>
        [HttpPost]
        [Route("schedule", Name = nameof(PostMonitoringSchedule) + "Route")]
        public ScheduleDto PostMonitoringSchedule([FromBody]ScheduleDto monitoringScheduleDto)
        {
            Logger.Log.Info($"{nameof(PostMonitoringSchedule)}: {JsonConvert.SerializeObject(monitoringScheduleDto)}");
            return  _scheduleApi.AddItem(monitoringScheduleDto);
        }

        /// <summary>
        /// Удаление мониторинга
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("schedule/{id}", Name = nameof(DeleteMonitoringSchedule) + "Route")]
        public bool DeleteMonitoringSchedule(int id)
        {
            Logger.Log.Info($"{nameof(DeleteMonitoringSchedule)}: {id}");
            return _scheduleApi.RemoveItem(id);
        }


        /// <summary>
        /// Цены по результатам мониторинга
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("PricesByMonitoringSchedule/{id:int}", Name = nameof(GetPricesByMonitoringSchedule) + "Route")]
        public IEnumerable<ContentDto> GetPricesByMonitoringSchedule(int id)
        {
            Logger.Log.Info($"{nameof(GetPricesByMonitoringSchedule)}: {id}");
            return new List<ContentDto>();
        }

        /// <summary>
        /// Цены по результатам мониторинга
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("PricesByTag/{tag}", Name = nameof(GetPricesByTag) + "Route")]
        public IEnumerable<ContentDto> GetPricesByTag(string tag)
        {
            Logger.Log.Info($"{nameof(GetPricesByTag)}: {tag}");
            return new List<ContentDto>();
        }

    }
}
