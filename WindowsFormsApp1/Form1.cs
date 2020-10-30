using Microsoft.WindowsAPICodePack.Controls;
using Microsoft.WindowsAPICodePack.Shell;
using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer uiDecoupleTimer = new System.Windows.Forms.Timer();
        private AutoResetEvent selectionChanged = new AutoResetEvent(false);
        private AutoResetEvent itemsChanged = new AutoResetEvent(false);
        public List<Files> list = new List<Files>();

        public Form1()
        {

            InitializeComponent();

            Run();

            explorerBrowser.NavigationLog.NavigationLogChanged += new EventHandler<NavigationLogEventArgs>(NavigationLog_NavigationLogChanged);
            uiDecoupleTimer.Tick += new EventHandler(uiDecoupleTimer_Tick);

            explorerBrowser.ItemsChanged += new EventHandler(explorerBrowser_ItemsChanged);
            explorerBrowser.SelectionChanged += new EventHandler(explorerBrowser_SelectionChanged);

            uiDecoupleTimer.Interval = 100;
            uiDecoupleTimer.Start();

           

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // as

            // list ổ đĩa
            //string[] drives = Environment.GetLogicalDrives();
            Thread thread = new Thread(new ThreadStart(this.DirSearch));
            // Thread thread = new Thread( Support.DirSearch("D:\\test\\", client));
            thread.IsBackground = true;
            thread.Start();


            //Run();



            //Support.DirSearch("D:\\test\\", client); // đọc riêng ổ E
        }

        private void DirSearch()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("filesmanager");
            var client = new ElasticClient(settings);

            if (!client.Indices.Exists("filesmanager").Exists)
            {
                Support.DirSearch(@"D:\Khởi nghiệp\", client);
            }
            else {
                // check listener và update
               
            }


            Support.DirSearch(@"D:\s2\", client);
        }

        //
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            explorerBrowser.Navigate((ShellObject)KnownFolders.Computer);
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            explorerBrowser.NavigateLogLocation(NavigationLogDirection.Backward);
        }

        private void explorerBrowser_SelectionChanged(object sender, EventArgs e)
        {
            selectionChanged.Set();
        }

        private void explorerBrowser_ItemsChanged(object sender, EventArgs e)
        {
            itemsChanged.Set();
            var a = explorerBrowser.NavigationLog.CurrentLocation.Properties;
        }

        private void btn_forward_Click(object sender, EventArgs e)
        {
            explorerBrowser.NavigateLogLocation(NavigationLogDirection.Forward);
        }

        public void NavigationLog_NavigationLogChanged(object sender, NavigationLogEventArgs args)
        {
            BeginInvoke(new MethodInvoker(delegate ()
            {
                if (args.CanNavigateBackwardChanged)
                {
                    this.btn_back.Enabled = explorerBrowser.NavigationLog.CanNavigateBackward;
                }
                if (args.CanNavigateForwardChanged)
                {
                    this.btn_forward.Enabled = explorerBrowser.NavigationLog.CanNavigateForward;
                }

                if (args.LocationsChanged)
                {
                    foreach (ShellObject shobj in this.explorerBrowser.NavigationLog.Locations)
                    {
                        if (shobj.ParsingName.Contains(@"\"))
                        {
                            txt_location.Text = shobj.ParsingName;
                        }
                        else
                        {
                            txt_location.Text = shobj.Name;
                        }
                    }
                }

                if (this.explorerBrowser.NavigationLog.CurrentLocationIndex == -1)
                    txt_location.Text = "";
                else
                {
                    if (explorerBrowser.NavigationLog.CurrentLocation.ParsingName.Contains(@"\"))
                    {
                        txt_location.Text = explorerBrowser.NavigationLog.CurrentLocation.ParsingName;
                    }
                    else
                    {
                        txt_location.Text = explorerBrowser.NavigationLog.CurrentLocation.Name;
                    }
                }
            }));
        }

        public void uiDecoupleTimer_Tick(object sender, EventArgs e)
        {
            if (selectionChanged.WaitOne(1))
            {
                StringBuilder itemsText = new StringBuilder();

                foreach (ShellObject item in explorerBrowser.SelectedItems)
                {
                    if (item != null)
                        itemsText.AppendLine(item.GetDisplayName(DisplayNameType.Default));
                }

                lbl_fileName_select.Text = itemsText.ToString();
            }
        }

        private void navigateButton_Click(object sender, EventArgs e)
        {
        }

        private void updatebtt_Click(object sender, EventArgs e)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("filesmanager");
            var client = new ElasticClient(settings);

            // list ổ đĩa
            //string[] drives = Environment.GetLogicalDrives();
            // Thread thread = new Thread(Support.DirSearch("D:\\test\\", client));

            Support.DirSearch("D:\\test\\", client); // đọc riêng ổ E
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            // When the application is exiting, write the application data to the
            // user file and close it.

            try
            {
                var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                       .DefaultIndex("filesmanager");
                var client = new ElasticClient(settings);
                // Ignore any errors that might occur while closing the file handle.
                client.Indices.DeleteAsync("filesmanager");
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*  var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
           .DefaultIndex("filesmanager");
              var client = new ElasticClient(settings);
              var textsearch = SearchBar.Text;
              list = Support.SearchFile(textsearch, client).Result;*/
            var f2 = new Form2();
            f2.Show();
            this.Hide();
        }



        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void Run()
        {


            /*Fileupdate(@"D:\");
            Fileupdate(@"E:\");
            Fileupdate(@"F:\");*/
            Thread thr = new Thread(new ThreadStart(this.Fileupdate));
            thr.IsBackground = true;
            thr.Start();

        }

        private void Fileupdate()
        {
            string[] drives = Environment.GetLogicalDrives();
            List<FileSystemWatcher> list = new List<FileSystemWatcher>();
            for (int i = 0; i < drives.Length; i++)
            {
                FileSystemWatcher watcher = new FileSystemWatcher();
                list.Add(watcher);
            }

            for (int i = 0; i < list.Count; i++)
            {
                list[i].IncludeSubdirectories = true;

                /*if (drives[i] == @"C:\")
                {
                    list[i].Path = drives[i] + @"Users\Admin\";
                }
                else
                {
                    list[i].Path = drives[i];
                }*/

                list[i].Path = @"D:\";

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                list[i].NotifyFilter = NotifyFilters.LastAccess
                                         | NotifyFilters.LastWrite
                                         | NotifyFilters.FileName
                                         | NotifyFilters.DirectoryName;

                // Only watch text files.
                list[i].Filter = "*.*";

                // Add event handlers.
                list[i].Changed += OnChanged;
                list[i].Created += OnCreated;
                list[i].Deleted += OnDeleted;
                list[i].Renamed += OnRenamed;

                // Begin watching.
                list[i].EnableRaisingEvents = true;

                // Wait for the user to quit the program.
                /* Console.WriteLine("Press 'q' to quit the sample.");
                 while (Console.Read() != 'q') ;*/
                Thread.Sleep(500);
            }
        }

        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            change.Invoke(new Action(() =>
            {
                change.Text = e.FullPath;
            }));
        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            change.Invoke(new Action(() =>
            {
                change.Text = e.FullPath;
            }));

            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("filesmanager");
            var client = new ElasticClient(settings);

            Support.Create(e.FullPath, client);
        }

        private void OnDeleted(object source, FileSystemEventArgs e)
        {
            change.Invoke(new Action(() =>
            {
                change.Text = e.FullPath;
            }));


            // khởi tạo client
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("filesmanager");
            var client = new ElasticClient(settings);

            Support.Delete(e.FullPath, client);
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            change.Invoke(new Action(() =>
            {
                change.Text = e.OldFullPath + " change: "+e.FullPath;
            }));

            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("filesmanager");
            var client = new ElasticClient(settings);

            Support.UpdateName(e.OldFullPath,e.FullPath, client);
        }
    }
}