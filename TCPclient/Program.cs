// Это программа клиента
using System;
using System.Net.Sockets;
using System.Text;

namespace TCPclient
{
    public static class HUD
    {
        public static String serverText;
        public static String clientText;
        public static String error;
        public static bool connected;
        /// <summary>
        ///  Сброс ошибок
        /// </summary>
        public static void ErrorReset()
        {
            error = "--------------------------------";
            Update();
        }
        /// <summary>
        ///  Обновление окна консоли
        /// </summary>
        public static void Update()
        {
            Console.Clear();
            Write(0, 2, "Server:");
            Write(2, 4, serverText);
            Write(0, 8, "Client:");
            Write(2, 10, clientText);
            Write(0, 20, "Errors:");
            Write(2, 22, error);
            if (connected)
                Write(60, 2, "Connected");
            else
                Write(60, 2, "Disconnected");
        }
        /// <summary>
        ///  Метод позволяет вывести строку по указанным координатам
        /// </summary>
        static void Write(int x, int y, String text)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }
        /// <summary>
        ///  Метод позволяет вывести строку по указанным координатам
        /// </summary>
        public static void New(int x, int y, String text)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "<<TcpClient>>";
            TcpClient client = null;
            NetworkStream stream = null;
            int clientCount = 1;
            ConsoleKey pressedKey;                                          //  нажатая клавиша
            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            byte[] sendBuffer = Encoding.UTF8.GetBytes($"Hello, it's №{clientCount} message from client");
            while (client == null)
            {
                clientCount++;
                try
                {
                    client = new TcpClient("192.168.0.100", 12345);         // 1) клиент пытается достучаться до сервера
                    HUD.connected = true;
                    HUD.ErrorReset();
                    stream = client.GetStream();
                    byte[] receiveBuffer = new byte[client.ReceiveBufferSize];
                    while (Console.KeyAvailable==false)
                    {
                        stream.Write(sendBuffer, 0, sendBuffer.Length);     // 2) отправляет серверу сообщение
                        HUD.serverText = sendBuffer.ToString();
                        if (stream.CanRead)                                 //  возможность чтения из потока
                        {
                            StringBuilder message = new StringBuilder();
                            int bytes = 0;
                            while (stream.DataAvailable)                    //  доступность данных для чтения
                            {                                               // 3) получает данные от сервера
                                bytes = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                                message.AppendFormat(Encoding.UTF8.GetString(receiveBuffer, 0, bytes));
                            }
                            HUD.clientText = message.ToString();
                            sendBuffer = Encoding.UTF8.GetBytes(message.ToString());
                        }
                        else
                        {
                            HUD.error = "I cannot read at this time :(";
                        }
                        HUD.Update();
                    }
                    cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Escape)
                        break;
                }
                catch (Exception ex)
                {
                    HUD.connected = false;
                    HUD.clientText = $"{clientCount} попытка...";
                    HUD.error = ex.Message;
                    HUD.Update();
                }
                finally
                {
                    if (client != null)
                    {
                        stream.Close();
                        client.Close();
                    }
                }
            }
            Console.Clear();
            Console.Title = "Good bye!";
            HUD.New(60, 20, "Good bye!\nPress ENTER for EXIT...");
            Console.Read();
        }
    }
}
