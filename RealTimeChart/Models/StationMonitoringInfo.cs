using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealTimeChart.Models
{
    public class StationMonitoringInfo
    {
         [Key]
        public int StationMonitoringInfoID { get; set; }

        public string Name { get; set; }

        public int CPUValue { get; set; }

        public int MemoryValue { get; set; }
    
    }
}