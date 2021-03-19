using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace ConsoleSocketServer
{
    class ProgramGuardarCode
    {
        public static string dataReceiver = null;
        static byte[] bytes = new Byte[1024];


        static void Main(string[] args)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            byte[] bytes = new Byte[1024];


            Socket socket = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);


            try
            {
                socket.Bind(localEndPoint);
                socket.Listen(10);



                //sistema esperando conexao
                while (true)
                {
                    Console.WriteLine("Esperando por uma conexao...");
                    Socket connectionEstabilish = socket.Accept();
                    Console.WriteLine("Nova conexao estabelecida");




                    //conexao estabelecida
                    while (true)
                    {
                        dataReceiver = null;
                        int bytesRec = connectionEstabilish.Receive(bytes);
                        dataReceiver += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        Console.WriteLine(dataReceiver);


                        if (dataReceiver.IndexOf("desligar servidor") != -1)
                        {
                            //comando de desligando enviado com sucesso


                            var timer = 10;

                            while (timer > 0)
                            {
                                var msgShutdown = "Servidor desligando em " + timer + " segundos";
                                connectionEstabilish.Send(Encoding.ASCII.GetBytes(msgShutdown));
                                timer--;
                                Thread.Sleep(1000);
                            }

                            Thread.Sleep(10000);


                            byte[] msg = Encoding.ASCII.GetBytes(dataReceiver);

                            connectionEstabilish.Send(msg);
                            connectionEstabilish.Shutdown(SocketShutdown.Both);
                            connectionEstabilish.Close();
                            return;
                        }
                        int bytesSender = connectionEstabilish.Send(Encoding.ASCII.GetBytes("informacoes recebidas com sucesso"));
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }

}
