/* Сервер. Класс потока клиента. */
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace gameHost
{
    public class ClientHandler
    {
        public TcpClient ClientSocket;

        public void RunClient()
        {
            var reader = new StreamReader(ClientSocket.GetStream());
            var writer = new StreamWriter(ClientSocket.GetStream());
            var returnData = reader.ReadLine();
            var user = returnData;
            Console.WriteLine(@"Welcome " + user + @" to the server");
            while (true)
            {
                returnData = reader.ReadLine();
                if (returnData != null && returnData.IndexOf("exit", StringComparison.Ordinal) > -1)
                {
                    Console.WriteLine(@"Bye " + user);
                    break;
                }
                Console.WriteLine(user + @": " + returnData);
                returnData += "\r\n";                                       // отправка данных серверу
                var dataWrite = Encoding.ASCII.GetBytes(returnData);
                writer.Write(dataWrite.ToString().ToCharArray(), 0, dataWrite.Length);
            }
            ClientSocket.Close();
        }
    }
}
