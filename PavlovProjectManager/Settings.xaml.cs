using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace PavlovProjectManager
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            RegistryFunctions regfunc = new();
            InitializeComponent();

            Destination.Text = regfunc.GetUEPath();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegistryFunctions regfunc = new();
            string filename;
            OpenFileDialog dlg = new();
            dlg.FileName = "UE4Editor.exe";
            dlg.DefaultExt = ".exe";
            dlg.Filter = "Executeable File (.exe)|*.exe";

            Nullable<bool> result = dlg.ShowDialog();

            if(result == true)
            {
                filename = dlg.FileName;
                regfunc.SetUEPath(filename);
            }

            Destination.Text = regfunc.GetUEPath();
        }
    }
}
