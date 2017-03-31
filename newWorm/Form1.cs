﻿using System;
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
        //TODO int ID; // ID клиента
        TcpClient client = null;                                // объект клиента
        NetworkStream stream = null;                            // поток для связи с сервером
        byte[] sendBuffer;                                      // буфер передачи
        byte[] receiveBuffer;                                   // буфер чтения
        StringBuilder message = new StringBuilder();            // сообщение от сервера

        public Snake snake = new Snake();

        Image snakePic = Image.FromFile("bodyPic.jpg");  // изображение элемента тела червя
        //Bitmap bitmap = new Bitmap(snakePic);            // 
        Graphics graph;

        public mainForm()                                       // конструктор
        {
            InitializeComponent();
            //bitmap.SetPixel(10, 10, Color.White);
            graph = pictureBox1.CreateGraphics();
            //DoubleBuffered = true;
            ShowInTaskbar = false;
            timer1.Enabled = true;
        }

        /// <summary>
        /// Сдвиг змейки и обновление pictureBox
        /// </summary>
        public void Upd()
        {
            snake.Move();
            DrawSnake();
        }

        /// <summary>
        /// Отрисовка червя
        /// </summary>
        void DrawSnake()                                      // отрисовка червя
        {
            int x, y;
            foreach (var part in snake.body)
            {
                x = part.X;
                y = part.Y;
                graph.DrawImage(snakePic, x, y);
            }
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

        void MainFormKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Application.Exit();
                    break;
                case Keys.Up:
                    if (Snake.direction != Snake.Direction.Down)
                    {
                        Snake.direction = Snake.Direction.Up;
                        snake.dY = -snake.Speed;
                        snake.dX = 0;
                    }
                    break;
                case Keys.Down:
                    if (Snake.direction != Snake.Direction.Up)
                    {
                        Snake.direction = Snake.Direction.Down;
                        snake.dY = snake.Speed;
                        snake.dX = 0;
                    }
                    break;
                case Keys.Left:
                    if (Snake.direction != Snake.Direction.Right)
                    {
                        Snake.direction = Snake.Direction.Left;
                        snake.dX = -snake.Speed;
                        snake.dY = 0;
                    }
                    break;
                case Keys.Right:
                    if (Snake.direction != Snake.Direction.Left)
                    {
                        Snake.direction = Snake.Direction.Right;
                        snake.dX = snake.Speed;
                        snake.dY = 0;
                    }
                    break;
            }
        }

        void ConnectButtonClick(object sender, EventArgs e)
        {
            try
            {
                //client = new TcpClient("127.0.0.1", 12345);     // 1)   подлючается к серверу
                //sendBuffer = Encoding.UTF8.GetBytes("ID");      // текст запроса у сервера
                //stream = client.GetStream();                    // получаем поток чтения-записи
                //stream.Write(sendBuffer, 0, sendBuffer.Length); // 2)   отправляет запрос на присвоение ID
                //receiveBuffer = new byte[client.ReceiveBufferSize];
                connectButton.Text = "playing...";
                connectButton.Enabled = false;
                //if (stream.CanRead)                             //  возможность чтения из потока
                //{
                //    ServerRead();                               // 3)   читает ответ
                //}
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Сервер не доступен\nПопробуйте ещё раз\n__________________\n" + ex.Message);
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show("Возникла ошибка, не связанная с сетью..." + ex.Message);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Ошибка инициализации" + ex.StackTrace);
            }
            finally
            {
            }
        }

        void Timer1Tick(object sender, EventArgs e)
        {
            Upd();
        }
    }


}
