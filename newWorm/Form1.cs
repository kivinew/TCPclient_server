using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace newWorm
{
    public partial class Form1 : Form
    {
        TcpClient client = null;
        NetworkStream stream = null;
        byte[] sendBuffer = Encoding.UTF8.GetBytes("ping");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient("192.168.0.100", 12345);
                button1.Text = "Connected";
            }
            catch(Exception ex)
            {

            }
        }
    }
}
