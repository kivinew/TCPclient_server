using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace newWorm
{
    /// <summary>
    /// Главная форма игрового клиента
    /// </summary>
    public partial class mainForm : Form
    {
        int ID;                                                 // ID клиента
        TcpClient client = null;                                // объект клиента
        NetworkStream stream = null;                            // поток для связи с сервером
        byte[] sendBuffer;                                      // буфер передачи
        byte[] receiveBuffer;                                   // буфер чтения
        StringBuilder message = new StringBuilder();            // сообщение от сервера

        public mainForm()                                       // конструктор
        {
            InitializeComponent();
        }
        /// <summary>
        /// Метод чтения данных с сервера
        /// </summary>
        public void ServerRead()
        {
            int bytes = 0;
            while (stream.DataAvailable)                        //  пока есть данные для чтения
            {                                                   //  получаем ответ от сервера
                bytes = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                message.AppendFormat(Encoding.UTF8.GetString(receiveBuffer, 0, bytes));
            }
        }
        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient("127.0.0.1", 12345); // 1)   подлючается к серверу
                sendBuffer = Encoding.UTF8.GetBytes("ID");      // текст запроса у сервера
                stream = client.GetStream();                    // получаем поток чтения-записи
                stream.Write(sendBuffer, 0, sendBuffer.Length); // 2)   отправляет запрос на присвоение ID
                receiveBuffer = new byte[client.ReceiveBufferSize];
                connectButton.Text = "Connected";
                connectButton.Enabled = false;
                if (stream.CanRead)                             //  возможность чтения из потока
                {
                    ServerRead();                               // 3)   читает ответ
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Сервер не доступен\nПопробуйте ещё раз\n__________________\n" + ex.Message);
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show("Возникла ошибка, не связанная с сетью..."+ ex.Message);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Ошибка инициализации"+ ex.StackTrace);
            }
            finally
            {

            }
        }
        public void pictureBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (Direction != direction.Down)
                        Direction = direction.Up;
                    break;
                case Keys.Down:
                    if (Direction != direction.Up)
                        Direction = direction.Down;
                    break;
                case Keys.Left:
                    if (Direction != direction.Right)
                        Direction = direction.Left;
                    break;
                case Keys.Right:
                    if (Direction != direction.Left)
                        Direction = direction.Right;
                    break;
            }
        }
    }

    
}
