using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using RealtimeUsage.Application;
using RealtimeUsage.Domain;
using RealtimeUsage.Repository;
using Microsoft.SqlServer.Server;

namespace ConsoleSocketClient
{
    class Program
    {
        public static string dataReceiver = "";
        public static byte[] bytes = new byte[1024];
        public static bool ServerActive = true;
        public static int Port = 11000;
        public static IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        public static IPAddress ipAddress = ipHostInfo.AddressList[0];
        public static IPEndPoint remoteEP = new IPEndPoint(ipAddress, Port);
        public static Socket socket = new Socket(ipAddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp);


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


        static void Main(string[] args)
        {
            ClassRealtimeUsage classRealtimeUsage = new ClassRealtimeUsage();
            var _applicationRealtimeUsage = ContainerApplicationInit();


            Socket socket = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socket.Connect(remoteEP);
                Console.WriteLine("Conectado no servidor: {0}",
                       socket.RemoteEndPoint.ToString());

                while (ServerActive)
                {
                    var msgBox = "Nome da maquina: " + classRealtimeUsage.machineName + "\n" +
                                  "Uso da CPU: " + classRealtimeUsage.UsageCPU  + "% \n" +
                                  "Uso da RAM: " + classRealtimeUsage.UsageRAM  + "MB \n";


                    byte[] msg = Encoding.ASCII.GetBytes(msgBox);//transforma a mensagem em byte|menor tamanho
                    int bytesSent = socket.Send(msg);//envia a mensagem


                    int bytesRec = socket.Receive(bytes);//recebe uma resposta do servidor
                    Console.WriteLine("Servidor respondeu: {0}",
                    Encoding.ASCII.GetString(bytes, 0, bytesRec));


                    dataReceiver += Encoding.ASCII.GetString(bytes, 0, bytesRec);//verifica se servidor está offline
                    if (dataReceiver.IndexOf("Servidor desligando em 10 segundos") != -1)
                    {
                        while (dataReceiver.IndexOf("Servidor desligando em 1 segundos") == -1)
                        {
                            int bytesReceiverShutdown = socket.Receive(bytes);
                            Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRec));
                            dataReceiver += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        }
                        Console.WriteLine("Servidor Inativo.");
                        break;
                    }

                    getsUsage(_applicationRealtimeUsage, classRealtimeUsage);
                    Thread.Sleep(1000);
                }


                Thread.Sleep(10000);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }
}

