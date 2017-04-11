using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace TCPserverWinForms
{
    public partial class ServerForm : Form
    {
        TcpListener server = null;
        TcpClient client = null;
        NetworkStream stream = null;
        Apple apple;
        String message;

        public ServerForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ожидание клиента
        /// </summary>
        private void Connect()
        {
            client = server.AcceptTcpClient();              // 1) сервер ожидает входящее подключение
            txtStatus.Text += "Client connected. Listen...";
            btnStart.Text = "Ok!";
            stream = client.GetStream();                            // поток чтения/записи клиента
            do
            {
                byte[] receiveBuffer = new byte[client.ReceiveBufferSize];
                StringBuilder receivedMessage = new StringBuilder();
                int bytes = 0;                                      // количество принятых байт
                if (stream.CanRead)                                 // возможность чтения из потока
                {
                    do
                    {                                               // 2) ЧТЕНИЕ из потока
                        bytes = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                        receivedMessage.AppendFormat(Encoding.UTF8.GetString(receiveBuffer, 0, bytes));
                    } while (stream.DataAvailable);                 // читает, пока в потоке есть данные
                    txtStatus.Text += receivedMessage;
                    message = receivedMessage.ToString();
                }
                apple = new Apple();
                if (receivedMessage.ToString() == "Apple")          // клиент запросил новое яблоко
                {
                    apple = new Apple();
                    message = "X" + apple.x.ToString() + "Y" + apple.y.ToString();
                    txtStatus.Text +="Sending new apple coordinates :\n" + message;
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
                txtStatus.Text += "Message sent!";
            } while (client.Connected);
            btnStart.Text = "Disconnected";
            btnStart.Enabled = true;
        }
        /// <summary>
        /// Запуск сервера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                btnStart.Text = "Online";
                btnStart.BackColor = System.Drawing.Color.DarkSeaGreen;
                server = new TcpListener(IPAddress.Any, 12345);             // сервер 
                server.Start();
                btnStop.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
            Connect();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (server != null)
            {
                server.Stop();                                      // сервер остановлен
                stream.Close();                                     // поток чтения/записи закрыт
                client.Close();                                     // подключение к клиенту закрыто
            }
            btnStart.Enabled = true;
            btnStart.BackColor = System.Drawing.Color.DeepSkyBlue;
            btnStart.Text = "Start server";
            btnStop.Enabled = false;
        }
    }
}
