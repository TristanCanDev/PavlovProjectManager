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
using System.IO;
using Microsoft.VisualBasic;

namespace PavlovProjectManager
{
    /// <summary>
    /// Interaction logic for MainPrg.xaml
    /// </summary>
    public partial class MainPrg : Window
    {
        public string UEPath;

        public MainPrg()
        {
            InitializeComponent();

            RegistryFunctions regfunc = new();
            UEPath = regfunc.GetUEPath();

            List<DirButton> button = new List<DirButton>();
            foreach(string file in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\PavlovProjects\\"))
            {
                button.Add(new DirButton() { dirName = file.Replace(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+"\\PavlovProjects\\", "") });
            }
            

            DirButtons.ItemsSource = button;
        }

        private void RefreshProj(object sender, RoutedEventArgs e)
        {
            Refresh();
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

        class DirButton
        {
            public string dirName { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = ((Button)sender).Content.ToString();
            string currentDir;

            foreach (string dir in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PavlovProjects\\"))
            {
                string namename = dir.Replace(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PavlovProjects\\", "");
                if (namename == name)
                {
                    currentDir = dir;
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        if (file.Contains("Pavlov.uproject"))
                        {
                            
                            string[] towrite = {
                                "start \"\" \"" + UEPath + "\" \"" + file + "\""
                            };

                            File.WriteAllLines(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BluSoft\\PavlovHandler\\runbatch.bat", towrite);
                            System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BluSoft\\PavlovHandler\\runbatch.bat");
                        }
                    }

                }
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Creates new project!
            Welcome.Visibility = Visibility.Hidden;
            New.Visibility = Visibility.Hidden;
            RefYes.Visibility = Visibility.Hidden;
            delete.Visibility = Visibility.Hidden;
            Push.Visibility = Visibility.Hidden;
            settings.Visibility = Visibility.Hidden;
            Create.Visibility = Visibility.Visible;
            FileName.Visibility = Visibility.Visible;
            Cancel.Visibility = Visibility.Visible;
            
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if(FileName.Text != "Name")
            {
                //try
                //{
                //    File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\BluSoft\\PavlovHandler\\baseproj\\PavlovVR-ModKit-master", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PavlovProjects\\" + FileName.Text);
                //    StatusBock.Visibility = Visibility.Hidden;
                //}
                //catch
                //{
                //    StatusBock.Visibility = Visibility.Visible;
                //    StatusBock.Text = "Cannot name the files the same";
                //}

                CopyDir cop = new();

                cop.DirectoryCopy(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\BluSoft\\PavlovHandler\\baseproj\\PavlovVR-ModKit-master", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PavlovProjects\\" + FileName.Text, true);

                StatusBock.Visibility = Visibility.Hidden;
                Create.Visibility = Visibility.Hidden;
                FileName.Visibility = Visibility.Hidden;
                Cancel.Visibility = Visibility.Hidden;
                Welcome.Visibility = Visibility.Visible;
                New.Visibility = Visibility.Visible;
                RefYes.Visibility = Visibility.Visible;
                delete.Visibility = Visibility.Visible;
                Push.Visibility = Visibility.Visible;
                settings.Visibility = Visibility.Visible;
                Refresh();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DeleteWindow del = new();
            del.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Cancel.Visibility = Visibility.Hidden;
            StatusBock.Visibility = Visibility.Hidden;
            Create.Visibility = Visibility.Hidden;
            FileName.Visibility = Visibility.Hidden;
            Welcome.Visibility = Visibility.Visible;
            New.Visibility = Visibility.Visible;
            RefYes.Visibility = Visibility.Visible;
            delete.Visibility = Visibility.Visible;
            Push.Visibility = Visibility.Visible;
            Refresh();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Settings settings = new();
            settings.Show();
        }
    }
}
