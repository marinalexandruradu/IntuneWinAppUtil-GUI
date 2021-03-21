using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace IntuneWin32Converter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        public static bool DownloadFromGitHub() {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = Path.Combine(Environment.SystemDirectory, "curl.exe");

            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = " -LJO https://github.com/microsoft/Microsoft-Win32-Content-Prep-Tool/raw/master/IntuneWinAppUtil.exe";

            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        static void Main()
        {
               

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
