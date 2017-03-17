// Это программа клиента
using System;
using System.Net.Sockets;
using System.Text;

namespace TCPclient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "<<TcpClient>>";
            TcpClient client = null;
            int clientCount = 0, connectFlag = 0;                       // флаг подключения
            ConsoleKey choice;
            byte[] sendBuffer = Encoding.UTF8.GetBytes($"Hello, it's №{clientCount} message from client");
            while (client == null)
            {
                clientCount++;
                try
                {
                    client = new TcpClient("127.0.0.1", 12345);
                    connectFlag = 1;                                    // флаг подключения
                    Console.WriteLine("Connected! Let's make a server query...");
                    NetworkStream stream = client.GetStream();
                    byte[] receiveBuffer = new byte[client.ReceiveBufferSize];
                    do
                    {
                        stream.Write(sendBuffer, 0, sendBuffer.Length);
                        Console.WriteLine("Message sent successfully!");
                        if (stream.CanRead)                                 // возможность чтения из потока
                        {
                            StringBuilder message = new StringBuilder();
                            int bytes = 0;
                            do
                            {
                                bytes = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                                message.AppendFormat($"{0}", Encoding.UTF8.GetString(receiveBuffer, 0, bytes));
                            } while (stream.DataAvailable);                 // доступность данных для чтения
                            Console.WriteLine($"Received message:\n{message}");
                        }
                        else
                        {
                            Console.WriteLine("I cannot read at this time :(");
                        }
                        Console.WriteLine("Do you want to continue communication? ");
                        choice = Console.ReadKey().Key;
                        sendBuffer = Encoding.UTF8.GetBytes(choice.ToString());
                    } while (choice!=ConsoleKey.Escape);
                    stream.Close();
                    connectFlag = 0;
                }
                catch (Exception ex)
                {
                    if (connectFlag == 1)
                        Console.WriteLine("Disconnected");
                    Console.WriteLine($"{clientCount} попытка:\n{ex.Message}\n");
                }
                finally
                {
                    Console.WriteLine("-------------------------------------------------------------\n\n");
                    if(client!=null)
                        client.Close();
                }
            }
            Console.WriteLine("Good bye!");
            Console.Title = "Good bye!";
            Console.Read();
        }
    }
}
