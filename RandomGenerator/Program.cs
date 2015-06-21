
using RealTimeChart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RandomGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            GraphContext ct = new GraphContext();
            var machines = ct.StationMonitoringInfos.ToList();
            var machinesRandomUpLimit = machines.Count - 1;
            var valuesRandomUpLimit=100;
            var randomMachine = new Random();
            var randomValue = new Random();
            while(true)
            {
                var randomUnit = randomMachine.Next(machinesRandomUpLimit);
                var randomCPU = randomValue.Next(valuesRandomUpLimit);
                machines[randomUnit].CPUValue = randomCPU;
                ct.SaveChanges();
                var randomMemory = randomValue.Next(valuesRandomUpLimit);
                machines[randomUnit].MemoryValue = randomMemory;
                ct.SaveChanges();
               Thread.Sleep(1000);
            }
        }
    }
}
