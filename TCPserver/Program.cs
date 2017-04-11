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
            TcpListener server = null;
            TcpClient client = null;
            NetworkStream stream = null;
            Apple apple;

            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");         // локальный адрес сервера
                server = new TcpListener(IPAddress.Any, 12345);                 // сервер 
                server.Start();
                do
                {
                    client = server.AcceptTcpClient();                      // 1) сервер ожидает входящее подключение
                    Console.WriteLine("Client connected. Listen...");
                    stream = client.GetStream();                            // поток чтения/записи клиента
                    do
                    {
                        byte[] receiveBuffer = new byte[client.ReceiveBufferSize];
                        StringBuilder receivedMessage = new StringBuilder();
                        int bytes = 0;                                      // количество принятых байт
                        string message = "Server: ";
                        if (stream.CanRead)                                 // возможность чтения из потока
                        {
                            do
                            {                                               // 2) ЧТЕНИЕ из потока
                                bytes = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                                receivedMessage.AppendFormat(Encoding.UTF8.GetString(receiveBuffer, 0, bytes));
                            } while (stream.DataAvailable);                 // читает, пока в потоке есть данные
                            Console.WriteLine("Получено сообщение от клиента:\n" + receivedMessage);
                        }
                        apple = new Apple();
                        if (receivedMessage.ToString() == "Apple")          // клиент запросил новое яблоко
                        {
                            apple = new Apple();
                            message = "X" + apple.x.ToString() + "Y" + apple.y.ToString();
                            Console.WriteLine("Sending new apple coordinates :\n" + message);
                        }
                        else                                                // проверяем: съел или нет?
                        {
                            int snakeX = message[message.IndexOf('X') + 1];
                            if (snakeX == apple.x)
                            {
                                int snakeY = message[message.IndexOf('Y') + 1];

                            }
                        }

                        byte[] data = Encoding.UTF8.GetBytes(message);      // преобразуем сообщение в массив байтов
                        stream.Write(data, 0, data.Length);                 // 3) отправляет массив байтов клиенту
                        Console.WriteLine("Message sent!");
                    } while (true);
                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("-------------------------------------\n"+ex.Message+"\n------------------------------------");
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
