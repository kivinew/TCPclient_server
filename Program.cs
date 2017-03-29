using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace newWorm
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Process proc = Process.Start("E:\\Users\\kivinew\\Documents\\Visual Studio 2017\\Projects\\TCPclient_server\\TCPserver\\bin\\Debug\\TCPserver.exe");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new mainForm());
            //proc.Close();
        }
    }
}
