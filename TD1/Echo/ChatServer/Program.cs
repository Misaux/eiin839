using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace Echo
{
    class EchoServer
    {
        [Obsolete]
        static void Main(string[] args)
        {

            Console.CancelKeyPress += delegate
            {
                System.Environment.Exit(0);
            };

            TcpListener ServerSocket = new TcpListener(5000);
            ServerSocket.Start();

            Console.WriteLine("Server started.");
            while (true)
            {
                TcpClient clientSocket = ServerSocket.AcceptTcpClient();
                handleClient client = new handleClient();
                client.startClient(clientSocket);
            }


        }
    }

    public class handleClient
    {
        TcpClient clientSocket;
        public void startClient(TcpClient inClientSocket)
        {
            this.clientSocket = inClientSocket;
            Thread ctThread = new Thread(Echo);
            ctThread.Start();
        }



        private void Echo()
        {
            NetworkStream stream = clientSocket.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);

            string post = "http/1.0 200 OK";
            string output = "<TITLE> L'exemple HTML le plus simple</TITLE>  \n<H1> Ceci est un sous-titre de niveau 1</H1>\n Bienvenue dans le monde HTML. Ceci est un paragraphe.\n  <P> Et ceci en est un second. </P>\n <A HREF=\"index.html\">cliquez ici</A> pour réafficher";

            while (true)
            {
                string str = reader.ReadString();
                if (str.Equals("GET /index.html"))
                {
                    Console.Write("index OK");
                    writer.Write(post + "\n\n" + output);
                    writer.Write(output);
                }
                else
                {
                    Console.WriteLine(str);
                    writer.Write(str);
                }
            }
        }



    }

}