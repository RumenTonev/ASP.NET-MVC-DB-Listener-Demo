using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using RealTimeChart.Models;
using System.Threading;

namespace RandomValuesGenerator
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("machinerequest")] string message, TextWriter log)
        {
           log.WriteLine("Following message will be written on the Queue={0}", message);
           DemoValuesGenarator();
        }
        private static void DemoValuesGenarator()
        {
            GraphContext ct = new GraphContext();
            var machines = ct.StationMonitoringInfos.ToList();
            var machinesRandomUpLimit = machines.Count - 1;
            var valuesRandomUpLimit = 100;
            var randomMachine = new Random();
            var randomValue = new Random();
            var startTime = DateTime.UtcNow;

            while (DateTime.UtcNow - startTime < TimeSpan.FromMinutes(2))
            {             
                var randomUnit = randomMachine.Next(machinesRandomUpLimit);
                var randomCPU = randomValue.Next(valuesRandomUpLimit);
                machines[randomUnit].CPUValue = randomCPU;
                ct.SaveChanges();
                var randomMemory = randomValue.Next(valuesRandomUpLimit);
                machines[randomUnit].MemoryValue = randomMemory;
                ct.SaveChanges();
                Thread.Sleep(2000);
            }

        }
    }
}
