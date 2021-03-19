using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace GerenciadorDeTarefas.InterfaceConsole
{
    class Program
    {
        private static PerformanceCounter PC = new PerformanceCounter();
        private static PerformanceCounter cpuCounter;
        private static PerformanceCounter ramCounter;


        static void Main(string[] args)
        {
            PC.CategoryName = "Process";
            PC.MachineName = Environment.MachineName;

            var counterShow = 0;

            string machineName = PC.MachineName;

            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");


            while (counterShow<3)
            {
                Console.WriteLine("Machine: " + machineName);
                Console.WriteLine("CPU Usage: " + cpuCounter.NextValue());
                Console.WriteLine("RAM Usage: " + ramCounter.NextValue());
                System.Threading.Thread.Sleep(10000);
                counterShow++;
            }



            Console.WriteLine("!!!!FINISH!!!!!");
            Console.ReadLine();
            return;
        }
    }
}
