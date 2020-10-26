using Nest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Searchbtt_Click(object sender, EventArgs e)
        {
            /* DialogResult dialogResult = MessageBox.Show("Test", "Alert", MessageBoxButtons.YesNo);
             if (dialogResult == DialogResult.Yes)
             { }*/

            // taoạo
            Thread thread = new Thread(new ThreadStart(this.SearchFile));
            // Thread thread = new Thread( Support.DirSearch("D:\\test\\", client));
            thread.IsBackground = true;
            thread.Start();

            //SearchView.DataBindings;
            //lay data
            //them dong hien len grid
        }

        private async void SearchFile()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("filesmanager");
            var client = new ElasticClient(settings);
            string textsearch = Searchbar.Text.ToString();
            Console.WriteLine(textsearch);
            var list = await Support.SearchFile(textsearch, client);

            //Console.WriteLine(list[0].url);
            SearchView.Invoke(new MethodInvoker(delegate ()
            {
                SearchView.DataSource = list;
            }
            ));
        }

        private void SearchView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var url = SearchView.Rows[e.RowIndex].Cells[2].Value.ToString();
            Process.Start(url);
        }
    }
}