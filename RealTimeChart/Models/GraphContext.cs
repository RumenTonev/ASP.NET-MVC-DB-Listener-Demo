using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RealTimeChart.Models
{
    public class GraphContext:DbContext
    {
      
            public GraphContext()
                : base("DefaultConnection")
            {

            }

            public DbSet<StationMonitoringInfo> StationMonitoringInfos { get; set; }
        
    }
}