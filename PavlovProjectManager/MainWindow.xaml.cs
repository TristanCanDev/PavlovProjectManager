using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PavlovProjectManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string latestProj;
        public string currentProj;
        public bool adbinst;
        public bool debugger = false;

        public MainWindow()
        {
            if (debugger)
            {
                try
                {
                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\BluSoft\\", true);
                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PavlovProjects\\", true);
                }
                catch
                {
                    Console.WriteLine("poopoo");
                }
            }
            InitializeComponent();
            beginInit();
        }

        public void beginInit()
        {
            if (!checkConnect())
            {
                NoConnectMsg.Visibility = Visibility.Visible;
                TryConnButton.Visibility = Visibility.Visible;
            }
            else if (checkConnect())
            {
                try
                {
                    foreach (string line in File.ReadAllLines(mainPath + "\\config.txt"))
                    {
                        if (line.Contains("initialized=True"))
                        {
                            MainPrg prg = new MainPrg();
                            prg.Show();
                            Close();
                        }
                    }
                }
                catch
                {
                    Init();
                }
            }
        }
        string mainPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\BluSoft\\PavlovHandler";
        public void Init()
        {
            // Will initialize things such as ADB and making sure the project is downloaded etc etc
            NoConnectMsg.Visibility = Visibility.Hidden;
            TryConnButton.Visibility = Visibility.Hidden;
            StartingStatus.Visibility = Visibility.Visible;
            StartingStatus.Text = "Beginning Initialization";
            
            

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\BluSoft\\"))
            {
                StartingStatus.Text = "Creating Master Directory";
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\BluSoft\\");
            }

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\BluSoft\\PavlovHandler"))
            {
                StartingStatus.Text = "Creating Sub Directory";
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\BluSoft\\PavlovHandler");
            }
            if (!File.Exists(mainPath + "\\config.txt"))
            {
                StartingStatus.Text = "Setting up config.txt";
                string[] Configuration =
                {
                    "initialized=True"
                };
                File.WriteAllLinesAsync(mainPath + "\\config.txt", Configuration);
                StartingStatus.Text = "Config set up, Checking ADB";
            }
            if (!Directory.Exists(mainPath + "\\adb\\"))
            {
                Directory.CreateDirectory(mainPath + "\\adb\\");
                StartingStatus.Text = "Created adb directory";
            }
            if (File.Exists(mainPath + "\\adb\\zippedADB.zip"))
            {
                File.Delete(mainPath + "\\adb\\zippedADB.zip");
            }
            if(Directory.Exists(mainPath + "\\adb\\latest\\"))
            {
                Directory.Delete(mainPath + "\\adb\\latest\\", true);
            }

            string[] temp = File.ReadAllLines(mainPath + "\\config.txt");
            foreach(string line in temp)
            {
                if (line.Contains("adbinst="))
                {
                    adbinst = true;
                }
                if (line.Contains("latestProj="))
                {
                    currentProj = line.Substring(line.IndexOf("latestProj=")).Replace("latestProj=", "");
                }
            }

            if (!adbinst)
            {
                WebClient adbdown = new WebClient();
                Uri adburl = new Uri("https://dl.google.com/android/repository/platform-tools-latest-windows.zip");

                adbdown.DownloadFileAsync(adburl, mainPath + "\\adb\\zippedADB.zip");
                adbdown.DownloadProgressChanged += new DownloadProgressChangedEventHandler(progress);
                adbdown.DownloadFileCompleted += new AsyncCompletedEventHandler(extract);
                void progress(object sender, DownloadProgressChangedEventArgs a)
                {
                    StartingStatus.Text = "Downloading ADB. " + a.BytesReceived + " out of " + a.TotalBytesToReceive + " bytes downloaded";
                }
                async void extract(object sender, AsyncCompletedEventArgs a)
                {
                    StartingStatus.Text = "Completed ADB download. Extracting";
                    await Task.Run(() => ZipFile.ExtractToDirectory(mainPath + "\\adb\\zippedADB.zip", mainPath + "\\adb\\latest\\"));
                    StartingStatus.Text = "File Unzipped! Installing the pavlov project if it hasn't been installed!";
                    string[] temp =
                    {
                        "adbinst=True"
                    };

                    File.AppendAllLines(mainPath + "\\config.txt", temp);

                    CheckThings();
                }
            }

            if (!Directory.Exists(mainPath + "\\baseproj"))
            {
                Directory.CreateDirectory(mainPath + "\\baseproj");
            }

        }

        public void CheckThings()
        {
            if (currentProj != null)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent",
                        "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

                    using (var response = client.GetAsync("https://api.github.com/repos/vankruptgames/PavlovVR-ModKit/commits").Result)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;

                        dynamic commits = JArray.Parse(json);
                        string lastCommit = commits[0].commit.message;
                        latestProj = lastCommit.Replace("\n","");
                    }
                }

                if (currentProj != latestProj)
                {

                    InstallProj();

                }
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent",
                        "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

                    using (var response = client.GetAsync("https://api.github.com/repos/vankruptgames/PavlovVR-ModKit/commits").Result)
                    {
                        var json = response.Content.ReadAsStringAsync().Result;

                        dynamic commits = JArray.Parse(json);
                        string lastCommit = commits[0].commit.message;
                        latestProj = lastCommit.Replace("\n","");
                    }
                }
                currentProj = latestProj;
                string[] temp1 =
                {
                    "latestProj=" + currentProj
                };
                File.AppendAllLines(mainPath + "\\config.txt", temp1);
                InstallProj();
            }
        }
        
        public void InstallProj()
        {
            WebClient projDown = new WebClient();
            Uri projurl = new Uri("http://github.com/vankruptgames/PavlovVR-ModKit/archive/refs/heads/master.zip");

            projDown.DownloadProgressChanged += new DownloadProgressChangedEventHandler(progress);
            projDown.DownloadFileCompleted += new AsyncCompletedEventHandler(extract);
            projDown.DownloadFileAsync(projurl, mainPath + "\\baseproj\\base.zip");

            async void progress(object sender, DownloadProgressChangedEventArgs a)
            {
                StartingStatus.Text = "Installing project file. " + a.BytesReceived + " out of " + a.TotalBytesToReceive + " bytes recieved";
            }

            async void extract(object sender, AsyncCompletedEventArgs a)
            {
                StartingStatus.Text = "Unzipping project";
                await Task.Run(() => ZipFile.ExtractToDirectory(mainPath + "\\baseproj\\base.zip", mainPath + "\\baseproj"));
                StartingStatus.Text = "Project installed. Setting up a couple more things and moving to the main interface!";

                MainPrg prg = new();
                Close();
                prg.Show();
            }

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PavlovProjects"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\PavlovProjects");
            }
        }

        public static bool checkConnect()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;

            }
            catch
            {
                return false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!checkConnect())
            {
                NoConnectMsg.Visibility = Visibility.Visible;
                TryConnButton.Visibility = Visibility.Visible;
                return;
            }
            else if (checkConnect())
            {
                beginInit();
            }
        }
    }
}
