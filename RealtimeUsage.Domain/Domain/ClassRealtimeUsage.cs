using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeUsage.Domain
{
    public class ClassRealtimeUsage
    {
        public string machineName { get; set; }
        public int UsageCPU { get; set; }
        public float UsageRAM { get; set; }
    }
}
