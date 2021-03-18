using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace KlientSever
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                string address = "127.0.0.1";
                int port = 8001;

                //Ansluter till servern:
                Console.WriteLine("Ansluter...");
                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect(address, port);
                Console.WriteLine("Ansluten!");

                //Skriv in medelande att skicka:
                Console.Write("Skriv in meddelande: ");
                String message = Console.ReadLine();

                //Konvertera meddelande till ASCII-bytes
                Byte[] bMessage = System.Text.Encoding.ASCII.GetBytes(message);

                //SKicka iväg meddelandet:
                Console.WriteLine("Skickar...");
                NetworkStream tcpStream = tcpClient.GetStream();
                tcpStream.Write(bMessage, 0, bMessage.Length);

                //tag emot meddelande från servern
                byte[] bRead = new byte[256];
                int bReadSize = tcpStream.Read(bRead, 0, bRead.Length);

                //Konvertera meddelandet till ett string objekt och skriv ut
                string read = "";
                for (int i = 0; i < bReadSize; i++)
                    read += Convert.ToChar(bRead[i]);
                Console.WriteLine("Servern säger: " + read);

                //Stäng anslutningen
                tcpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Console.ReadKey();

        }
    }
}
