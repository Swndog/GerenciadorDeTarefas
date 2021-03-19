using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace ConsoleWebSocket
{
   public class Program
    {
        // Incoming data from the client.  
        public static string data = null;

        public static void StartListening()
        {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the
            // host running the application.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and
            // listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true)
                {
                    Console.WriteLine("Esperando por uma conexao...");

                    // Program is suspended while waiting for an incoming connection.  
                    Socket handler = listener.Accept();


                    // An incoming connection needs to be processed.  
                    while (true)
                    {
                        data = null;
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        Console.WriteLine("Texto enviado: {0}", data);


                        //byte[] msg = Encoding.ASCII.GetBytes("texto recebido");
                        
                        if (data.IndexOf("desligar servidor") != -1)
                        {
                            break;
                        }
                        int bytesSender = handler.Send(Encoding.ASCII.GetBytes("texto enviado"));
                    }

                    // Show the data on the console.  
                    handler.Send(Encoding.ASCII.GetBytes("Servidor desligando..."));
                    Thread.Sleep(10000);

                    // Echo the data back to the client.  
                    byte[] msg = Encoding.ASCII.GetBytes(data);

                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                    break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        static void Main(string[] args)
        {
            StartListening();
            return;
        }
    }
}
