using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace TCPserverWinForms
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
        }
    }
}
