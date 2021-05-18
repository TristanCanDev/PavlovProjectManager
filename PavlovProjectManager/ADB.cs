using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavlovProjectManager
{
    class ADB
    {

        public string adbLocation;

        public void Kill()
        {
            foreach (var yes in Process.GetProcessesByName("adb"))
            {
                yes.Kill();
            }
        }

        public void Start()
        {
            Process process = new Process();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = adbLocation;
            process.StartInfo.Arguments = "devices";
            process.Start();
            process.WaitForExit();
        }

    }
}
