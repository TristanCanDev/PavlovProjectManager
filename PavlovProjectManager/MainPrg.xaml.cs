using System;
using System.Collections.Generic;
using System.IO;
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

namespace PavlovProjectManager
{
    /// <summary>
    /// Interaction logic for MainPrg.xaml
    /// </summary>
    public partial class MainPrg : Window
    {
        public MainPrg()
        {
            InitializeComponent();
            

            List<DirButton> button = new List<DirButton>();
            foreach(string file in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\PavlovProjects\\"))
            {
                button.Add(new DirButton() { dirName = file.Replace(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\PavlovProjects\\", "") });
            }
            

            DirButtons.ItemsSource = button;
        }

        public class DirButton
        {
            public string dirName { get; set; }
        }
    }
}
