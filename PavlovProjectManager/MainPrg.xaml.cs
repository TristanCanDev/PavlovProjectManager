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
using Microsoft.VisualBasic;

namespace PavlovProjectManager
{
    /// <summary>
    /// Interaction logic for MainPrg.xaml
    /// </summary>
    public partial class MainPrg : Window
    {
        public string UEPath;
        public static string SelectedProj;

        public MainPrg()
        {
            InitializeComponent();

            RegistryFunctions regfunc = new();
            UEPath = regfunc.GetUEPath();

            List<DirButton> button = new List<DirButton>();
            foreach(string file in Directory.GetDirectories($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\PavlovProjects\\"))
            {
                button.Add(new DirButton() { dirName = file.Replace($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\PavlovProjects\\", "") });
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
            foreach (string file in Directory.GetDirectories($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\PavlovProjects\\"))
            {
                button.Add(new DirButton() { dirName = file.Replace($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\PavlovProjects\\", "") });
            }


            DirButtons.ItemsSource = button;
        }

        class DirButton
        {
            public string dirName { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // First we set the selected project name so we can open it later as well as set the project label for the UI
            SelectedProj = ((Button)sender).Content.ToString();
            ProjectLabel.Text = $"{SelectedProj}";
            // We now set some UI elements to invisible so we can make room for new elements
            Welcome.Visibility = Visibility.Hidden;
            RefYes.Visibility = Visibility.Hidden;
            settings.Visibility = Visibility.Hidden;
            New.Visibility = Visibility.Hidden;
            // We enable the elements for the selected project
            Open.Visibility = Visibility.Visible;
            Delete.Visibility = Visibility.Visible;
            ProjectLabel.Visibility = Visibility.Visible;
            Push.Visibility = Visibility.Visible;
            Back.Visibility = Visibility.Visible;
        }

        private void Open_Button(object sender, RoutedEventArgs e)
        {

            OpenThingy();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Creates new project!
            Welcome.Visibility = Visibility.Hidden;
            New.Visibility = Visibility.Hidden;
            RefYes.Visibility = Visibility.Hidden;
            settings.Visibility = Visibility.Hidden;
            Create.Visibility = Visibility.Visible;
            FileName.Visibility = Visibility.Visible;
            Cancel.Visibility = Visibility.Visible;
            
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if(FileName.Text != "Name")
            {

                CopyDir cop = new();

                cop.DirectoryCopy($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\BluSoft\\PavlovHandler\\baseproj\\PavlovVR-ModKit-master", $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\PavlovProjects\\{FileName.Text}", true);

                StatusBock.Visibility = Visibility.Hidden;
                Create.Visibility = Visibility.Hidden;
                FileName.Visibility = Visibility.Hidden;
                Cancel.Visibility = Visibility.Hidden;
                Welcome.Visibility = Visibility.Visible;
                New.Visibility = Visibility.Visible;
                RefYes.Visibility = Visibility.Visible;
                settings.Visibility = Visibility.Visible;
                Refresh();
            }
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
            Refresh();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Settings settings = new();
            settings.Show();
        }

        void OpenThingy()
        {
            string name = SelectedProj;
            string currentDir;

            foreach (string dir in Directory.GetDirectories($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\PavlovProjects\\"))
            {
                string namename = dir.Replace($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\PavlovProjects\\", "");
                if (namename == name)
                {
                    currentDir = dir;
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        if (file.Contains("Pavlov.uproject"))
                        {

                            string[] towrite = {
                                $"start \"\" \"{UEPath}\" \"{file}\""
                            };

                            File.WriteAllLines($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\BluSoft\\PavlovHandler\\runbatch.bat", towrite);
                            System.Diagnostics.Process.Start($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\BluSoft\\PavlovHandler\\runbatch.bat");
                        }
                    }

                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Directory.Delete($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\PavlovProjects\\{SelectedProj}", true);
            Refresh();
            BackFromProj();
        }

        void BackFromProj()
        {
            Welcome.Visibility = Visibility.Visible;
            RefYes.Visibility = Visibility.Visible;
            settings.Visibility = Visibility.Visible;
            New.Visibility = Visibility.Visible;

            Delete.Visibility = Visibility.Hidden;
            Open.Visibility = Visibility.Hidden;
            ProjectLabel.Visibility = Visibility.Hidden;
            Push.Visibility = Visibility.Hidden;
            Back.Visibility = Visibility.Hidden;
        }

        private void Push_Click(object sender, RoutedEventArgs e)
        {
            PushPush.Visibility = Visibility.Visible;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            BackFromProj();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}