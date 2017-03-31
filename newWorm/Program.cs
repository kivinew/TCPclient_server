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
            Process proc = Process.Start("C:\\Users\\user\\Documents\\SharpDevelop Projects\\server\\bin\\Debug\\server.exe");            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mainForm());
            //proc.Close();
        }
    }
}
