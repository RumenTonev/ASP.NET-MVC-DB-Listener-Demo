using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace RealTimeChart.Models
{
    public class MachineShortModel
    {
        public static Expression<Func<StationMonitoringInfo, MachineShortModel>> FromMachine
        {
            get
            {
                return machine => new MachineShortModel
                {
                    Id = machine.StationMonitoringInfoID,
                    Name = machine.Name,
                    
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        
    }
}