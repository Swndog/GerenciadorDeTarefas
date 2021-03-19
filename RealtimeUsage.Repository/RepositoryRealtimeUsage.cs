using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealtimeUsage.Domain;


namespace RealtimeUsage.Repository
{
    public class RepositoryRealtimeUsage : IRealtimeUsage
    {
        //cpuCounter = new PerformanceCounter("Processor Information","% Processor Utility","_Total",true);

        PerformanceCounter UsageCPU = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total", true);
        PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");

        public string getNameMachine(ClassRealtimeUsage classRealtimeUsage)
        {
            return classRealtimeUsage.machineName = Environment.MachineName;
        }

        public int getUsageCPU(ClassRealtimeUsage classRealtimeUsage)
        {
            classRealtimeUsage.UsageCPU = (int)UsageCPU.NextValue();
            return classRealtimeUsage.UsageCPU;
        }

        public float getUsageRAM(ClassRealtimeUsage classRealtimeUsage)
        {
            classRealtimeUsage.UsageRAM = ramCounter.NextValue();
            return classRealtimeUsage.UsageRAM;
        }
    }
}
