// Это программа сервера
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TCPserver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "<<TcpServer>>";
            int bytes = 0;                                                  // количество принятых байт
            int connectFlag = 0;                                            // отметка о подключении
            TcpListener server = null;
            ConsoleKey choice;
            TcpClient client = null;
            NetworkStream stream = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("192.168.0.100");         // локальный адрес сервера
                server = new TcpListener(localAddr, 12345);                 // сервер 
                server.Start();
                do
                {
                    client = server.AcceptTcpClient();                      // входящее подключение
                    connectFlag = 1;
                    Console.WriteLine("Client connected. Listen...");
                    stream = client.GetStream();                            // поток чтения/записи клиента
                    byte[] receiveBuffer = new byte[client.ReceiveBufferSize];
                    do                                                      // ЧТЕНИЕ из потока
                    {
                        if (stream.CanRead)                                 // возможность чтения из потока
                        {
                            StringBuilder receivedMessage = new StringBuilder();
                            do
                            {
                                bytes = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                                receivedMessage.AppendFormat($"{0}" + Encoding.UTF8.GetString(receiveBuffer, 0, bytes));
                            } while (stream.DataAvailable);                 // доступность данных для чтения
                            Console.WriteLine("Получено сообщение от клиента:\n" + receivedMessage);
                        }
                        string message = "Ваше сообщение получено (" + bytes + " байт)";  // сервер сообщает сколько байтов получил
                        byte[] data = Encoding.UTF8.GetBytes(message);      // преобразуем сообщение в массив байтов
                        stream.Write(data, 0, data.Length);                 // ЗАПИСЬ массива байтов в поток
                        Console.WriteLine($"Сообщение: \n{message}\nклиенту отправлено!");
                    } while (true);
                } while (true);
            }
            catch (Exception ex)
            {
                if (connectFlag == 1)
                    Console.WriteLine("...lost connection");
                Console.WriteLine($"-------------------------------------\n{ex.Message}\n------------------------------------");
            }
            finally
            {
                if (server != null)
                {
                    server.Stop();
                    stream.Close();
                    client.Close();                                     // подключение к клиенту закрыто
                }
            }
        }
    }
}
