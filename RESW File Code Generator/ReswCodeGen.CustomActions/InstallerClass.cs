using System;
using System.Configuration.Install;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ReswCodeGen.CustomActions
{
    public class InstallerClass : Installer
    {
        protected override void OnAfterInstall(System.Collections.IDictionary savedState)
        {
            SetupVisualStudio();
        }

        public static void SetupVisualStudio()
        {
            string devenvPath;
            using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\VisualStudio\11.0"))
            {
                if (key == null)
                {
                    MessageBox.Show("Unable to locate devenv.exe", "Error");
                    return;
                }
                devenvPath = string.Format("{0}devenv.exe", key.GetValue("InstallDir", string.Empty));
            }

            var proc = new ProcessStartInfo
                           {
                               UseShellExecute = true,
                               WorkingDirectory = Environment.CurrentDirectory,
                               FileName = devenvPath,
                               Arguments = "/setup",
                               Verb = "runas"
                           };
            try
            {
                var process = Process.Start(proc);
                process.WaitForExit();
            }
            catch
            {
                MessageBox.Show("Unable to execute devenv.exe /setup", "Error");
            }
        }
    }
}
