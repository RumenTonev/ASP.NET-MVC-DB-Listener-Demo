using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using RealTimeChart.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChart.Hubs
{
    [HubName("chartData")]
    public class ChartDataHub : Hub
    {
        private readonly ChartData _pointer;

        public ChartDataHub() : this(new ChartData().Instance) { }

        public ChartDataHub(ChartData pointer)
        {
            _pointer = pointer;
        }
        public Task joinRoom(string roomName)
        {
            return Groups.Add(Context.ConnectionId, roomName);
        }
        public IEnumerable<StationMonitoringInfo> initData(string machineId)
        {           
            return _pointer.initData(machineId);
        }
        
    }
}
