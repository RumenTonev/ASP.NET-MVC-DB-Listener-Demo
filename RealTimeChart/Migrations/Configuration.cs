namespace RealTimeChart.Migrations
{
    using RealTimeChart.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RealTimeChart.Models.GraphContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(RealTimeChart.Models.GraphContext context)
        {
            for (int i = 0; i < 5; i++)
            {
                context.StationMonitoringInfos.AddOrUpdate(x=>x.Name,new StationMonitoringInfo
              {
                    Name = "VM " + i,
                    
                });
            }
            
        }
    }
}
