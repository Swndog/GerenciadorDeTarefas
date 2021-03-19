using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealtimeUsage.Domain;


namespace RealtimeUsage.Application
{
    public class ApplicationRealtimeUsage : IRealtimeUsage
    {
        private readonly IRealtimeUsage _realtimeUsage;

        public ApplicationRealtimeUsage(IRealtimeUsage realtimeUsage) =>
            _realtimeUsage = realtimeUsage;


        public string getNameMachine(ClassRealtimeUsage classRealtimeUsage)
        {
            return _realtimeUsage.getNameMachine(classRealtimeUsage);
        }

        public int getUsageCPU(ClassRealtimeUsage classRealtimeUsage)
        {
            return _realtimeUsage.getUsageCPU(classRealtimeUsage);
        }

        public float getUsageRAM(ClassRealtimeUsage classRealtimeUsage)
        {
            return _realtimeUsage.getUsageRAM(classRealtimeUsage);
        }
    }
}
