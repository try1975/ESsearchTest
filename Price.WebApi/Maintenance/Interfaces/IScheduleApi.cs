using Common.Dto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price.WebApi.Maintenance.Interfaces
{
    public interface IScheduleApi : ITypedApi<ScheduleDto, int>
    {
    }
}
