using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavlovProjectManager
{
    class RegistryFunctions
    {
        public void init()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\PavlovManager");

            key.SetValue("UEPATH", $"{Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)}\\Epic Games\\UE_4.21\\Engine\\Binaries\\Win64\\UE4Editor.exe");
            key.Close();
        }

        public void SetUEPath(string path)
        {
            //accessing the CurrentUser root element  
            //and adding "OurSettings" subkey to the "SOFTWARE" subkey  
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\PavlovManager");

            //storing the values  
            key.SetValue("UEPATH", path);
            key.Close();
        }

        public string GetUEPath()
        {
            //opening the subkey  
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\PavlovManager");

            //if it does exist, retrieve the stored values  
            if (key != null)
            {
                string path = (string)key.GetValue("UEPATH");
                key.Close();
                return path;
            }
            else
            {
                return null;
            }
        }
    }
}
