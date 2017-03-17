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
            int bytes = 0;                                              // количество принятых байт
            int connectFlag = 0;                                        // отметка о подключении
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");     // локальный адрес сервера
                server = new TcpListener(localAddr, 12345);             // сервер 
                server.Start();
                do
                {
                    var client = server.AcceptTcpClient();              // входящее подключение
                    connectFlag = 1;
                    Console.WriteLine("Client connected.");
                    NetworkStream stream = client.GetStream();          // поток чтения/записи клиента
                    byte[] receiveBuffer = new byte[client.ReceiveBufferSize];
                    if (stream.CanRead)                                 // возможность чтения из потока
                    {
                        StringBuilder receivedMessage = new StringBuilder();
                        do
                        {
                            bytes = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                            receivedMessage.AppendFormat($"{0}" + Encoding.UTF8.GetString(receiveBuffer, 0, bytes));
                        } while (stream.DataAvailable);                 // доступность данных для чтения
                        Console.WriteLine($"Получено сообщение от клиента:\n{receivedMessage}");
                    }
                    string message = "Ваше сообщение получено"+ bytes;   // Это тестовое сообщение от сервера
                    byte[] data = Encoding.UTF8.GetBytes(message);      // преобразуем сообщение в массив байтов
                    stream.Write(data, 0, data.Length);                 // запись массива байтов в поток
                    Console.WriteLine("Сообщение отправлено!");
                    stream.Close();
                    client.Close();                                     // подключение к клиенту закрыто
                    Console.WriteLine("Disconnected");
                } while (true);
            }
            catch(Exception ex)
            {
                if (connectFlag == 1)
                    Console.WriteLine("...lost connection");
                Console.WriteLine($"-------------------------------------\n{ex.Message}\n------------------------------------");
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }
        }
    }
}
