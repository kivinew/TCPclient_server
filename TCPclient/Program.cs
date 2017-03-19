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
        public static bool retry;

        public static void Reset()
        {
            error = "--------------------------------";
            Update();
        }

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

        static void Write(int x, int y, String text)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }
        //  Метод позволяет указать координаты и строку сообщения
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
            int clientCount = 0;
            ConsoleKey pressedKey;                                          //  нажатая клавиша
            byte[] sendBuffer = Encoding.UTF8.GetBytes($"Hello, it's №{clientCount} message from client");
            while (client == null)
            {
                clientCount++;
                try
                {
                    client = new TcpClient("192.168.0.100", 12345);
                    HUD.connected = true;
                    HUD.Reset();
                    NetworkStream stream = client.GetStream();
                    byte[] receiveBuffer = new byte[client.ReceiveBufferSize];
                    do
                    {
                        stream.Write(sendBuffer, 0, sendBuffer.Length);
                        HUD.serverText = sendBuffer.ToString();
                        HUD.Update();
                        if (stream.CanRead)                                 //  возможность чтения из потока
                        {
                            StringBuilder message = new StringBuilder();
                            int bytes = 0;
                            do
                            {
                                bytes = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                                message.AppendFormat(Encoding.UTF8.GetString(receiveBuffer, 0, bytes));
                            } while (stream.DataAvailable);                 //  доступность данных для чтения
                            HUD.clientText = message.ToString();
                        }
                        else
                        {
                            HUD.error = "I cannot read at this time :(";
                            HUD.Update();
                        }
                        HUD.New(60, 4, "Continue?");
                        pressedKey = Console.ReadKey().Key;
                        sendBuffer = Encoding.UTF8.GetBytes(pressedKey.ToString());
                    } while (pressedKey != ConsoleKey.Escape);
                    stream.Close();
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
                        client.Close();
                }
            }
            Console.Clear();
            Console.Title = "Good bye!";
            HUD.New(60, 20, "Good bye!\nPress ENTER for EXIT...");
            Console.Read();
        }
    }
}
