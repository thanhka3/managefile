﻿using Microsoft.WindowsAPICodePack.Controls;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using Nest;
using System.Diagnostics;
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

        public Form1()
        {
            InitializeComponent();
            explorerBrowser.NavigationLog.NavigationLogChanged += new EventHandler<NavigationLogEventArgs>(NavigationLog_NavigationLogChanged);
            uiDecoupleTimer.Tick += new EventHandler(uiDecoupleTimer_Tick);

            explorerBrowser.ItemsChanged += new EventHandler(explorerBrowser_ItemsChanged);
            explorerBrowser.SelectionChanged += new EventHandler(explorerBrowser_SelectionChanged);

            uiDecoupleTimer.Interval = 100;
            uiDecoupleTimer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

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
            DialogResult dialogResult = MessageBox.Show("THIS MAY TAKE HOURS TO ADD", "Alert", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                    .DefaultIndex("filesmanager");
                var client = new ElasticClient(settings);
                // list ổ đĩa
                string[] drives = Environment.GetLogicalDrives();
                Support.DirSearch("E:\\", client); // đọc riêng ổ E
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f2 = new Form2();
            f2.Show();
        }
    }
}