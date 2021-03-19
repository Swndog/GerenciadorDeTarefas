using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeUsage.Domain
{
    public interface IRealtimeUsage
    {
        string getNameMachine(ClassRealtimeUsage classRealtimeUsage);
        int getUsageCPU(ClassRealtimeUsage classRealtimeUsage);
        float getUsageRAM(ClassRealtimeUsage classRealtimeUsage);
    }
}
