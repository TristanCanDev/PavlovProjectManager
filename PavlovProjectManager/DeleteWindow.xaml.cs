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
    /// Interaction logic for DeleteWindow.xaml
    /// </summary>
    public partial class DeleteWindow : Window
    {
        public DeleteWindow()
        {
            InitializeComponent();

            List<DirButton> button = new List<DirButton>();
            foreach (string file in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PavlovProjects\\"))
            {
                button.Add(new DirButton() { dirName = file.Replace(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PavlovProjects\\", "") });
            }


            DirButtons.ItemsSource = button;
        }
        class DirButton
        {
            public string dirName { get; set; }
        }

        void Refresh()
        {
            DirButtons.ItemsSource = null;

            List<DirButton> button = new List<DirButton>();
            foreach (string file in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PavlovProjects\\"))
            {
                button.Add(new DirButton() { dirName = file.Replace(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PavlovProjects\\", "") });
            }


            DirButtons.ItemsSource = button;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = ((Button)sender).Content.ToString();

            foreach (string dir in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PavlovProjects\\"))
            {
                string namename = dir.Replace(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PavlovProjects\\", "");
                if (namename == name)
                {
                    Directory.Delete(dir, true);
                    Refresh();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
