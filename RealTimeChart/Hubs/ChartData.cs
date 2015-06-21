using Microsoft.AspNet.SignalR;
using RealTimeChart.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;

namespace RealTimeChart.Hubs
{
    public class ChartData
    {
        private readonly  Lazy<ChartData> _instance = new Lazy<ChartData>(() => new ChartData());
        private readonly ConcurrentQueue<StationMonitoringInfo> _points = new ConcurrentQueue<StationMonitoringInfo>();      
        private StationMonitoringInfo _startPoint;
        private int _machineId;
        
        public ChartData()
        { 
        }

        public  ChartData Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        /// <summary>
        /// To initialize data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StationMonitoringInfo> initData(string machineId)
        {
            _machineId = Int32.Parse(machineId);
            using (var context = new GraphContext())
            {
                _startPoint = context.StationMonitoringInfos.Find(_machineId);
            }
            GetDataSql(machineId);
            _points.Enqueue(_startPoint);
            BroadcastChartData(UpdateData());
            return _points.ToArray();
        }

        /// <summary>
        /// Timer callback method
        /// </summary>
        /// <param name="state"></param>
        private void TimerCallBack(object state)
        {           
          BroadcastChartData(UpdateData());    
        }

        /// <summary>
        /// To update data 
        /// </summary>
        /// <returns></returns>
        private StationMonitoringInfo UpdateData()
        {
            StationMonitoringInfo point =new StationMonitoringInfo(){
                 Name=_startPoint.Name,
                  CPUValue=_startPoint.CPUValue,
                   MemoryValue=_startPoint.MemoryValue,
                    StationMonitoringInfoID=_startPoint.StationMonitoringInfoID
            } ;
            if (_points.TryDequeue(out point))
            {
                using (var context = new GraphContext())
                {
                    point = context.StationMonitoringInfos.Find(_machineId); ;
                }
                _points.Enqueue(point);
            }       
            return point;
        }

        private void BroadcastChartData(StationMonitoringInfo point)
        {
            GlobalHost.ConnectionManager.GetHubContext<ChartDataHub>().Clients.Group(_machineId.ToString()).updateData(point);
        }

        public InfoShort GetDataSql(string machineId)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [CPUValue],[MemoryValue]
               FROM [dbo].[StationMonitoringInfoes] WHERE [StationMonitoringInfoID]="+machineId, connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        if  (reader.Read())
                        {
                            return new InfoShort()
                            {
                                CPUValue = reader.GetInt32(0),
                                MemoryValue = reader.GetInt32(1),

                            };  // read data for first record here
                        }
                        else
                        {
                            return new InfoShort()
                            {
                                CPUValue = 0,
                                MemoryValue =0,

                            };
                        }
                        
                }
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            BroadcastChartData(UpdateData());
            GetDataSql(_machineId.ToString());
        }       
    }
}