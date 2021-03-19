using System;
using System.Threading;
using RealtimeUsage.Application;
using RealtimeUsage.Domain;
using RealtimeUsage.Repository;


/*
 * DESCOBRIR COMO FAZER TER EM TEMPO REAL ESSES DADOS DE OUTROS COMPUTADORES
 * 
 */


namespace ViewConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassRealtimeUsage classRealtimeUsage = new ClassRealtimeUsage();
            var _applicationRealtimeUsage = ContainerApplicationInit();
            getsUsage(_applicationRealtimeUsage, classRealtimeUsage);

            var activator = true;
            var timerThreadSleep = 1000;



            Console.WriteLine("Nome da maquina: " + classRealtimeUsage.machineName);

            while (activator)
            {
                Console.WriteLine("Uso da CPU: " + classRealtimeUsage.UsageCPU + "%");
                Console.WriteLine("Uso da RAM: " + classRealtimeUsage.UsageRAM + "MB");
                getsUsage(_applicationRealtimeUsage, classRealtimeUsage);
                Thread.Sleep(timerThreadSleep);
            }


            Console.ReadLine();
            return;
        }
        


        public static ApplicationRealtimeUsage ContainerApplicationInit()
        {
            RepositoryRealtimeUsage repositoryRealtimeUsage = new RepositoryRealtimeUsage();
            ApplicationRealtimeUsage applicationRealtimeUsage = new ApplicationRealtimeUsage(repositoryRealtimeUsage);
            return applicationRealtimeUsage;
        }

        public static ClassRealtimeUsage getsUsage(ApplicationRealtimeUsage _applicationRealtimeUsage, 
                                                ClassRealtimeUsage classRealtimeUsage)
        {
            _applicationRealtimeUsage.getNameMachine(classRealtimeUsage);
            _applicationRealtimeUsage.getUsageCPU(classRealtimeUsage);
            _applicationRealtimeUsage.getUsageRAM(classRealtimeUsage);
            return classRealtimeUsage;
        }
    }
}
