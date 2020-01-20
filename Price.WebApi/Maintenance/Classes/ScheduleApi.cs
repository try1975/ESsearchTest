using System;
using System.IO;
using Common.Dto.Model;
using Common.Dto.Model.NewApi;
using Price.Db.Entities.Entities;
using Price.Db.Entities.QueryProcessors;
using Price.WebApi.Logic;
using Price.WebApi.Maintenance.Interfaces;
using PriceCommon.Enums;


namespace Price.WebApi.Maintenance.Classes
{
    public class ScheduleApi : TypedApi<MonitoringScheduleDto, ScheduleEntity, int>, IScheduleApi
    {
        public ScheduleApi(IScheduleQuery query) : base(query)
        {

        }
    }
}