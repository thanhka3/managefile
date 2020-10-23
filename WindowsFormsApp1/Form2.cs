using Nest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
               .DefaultIndex("filesmanager");
            var client = new ElasticClient(settings);
            var textsearch = Searchbar.Text;
            var list = Support.SearchFile(textsearch, client).Result;
            // taoạo
            SearchView.DataSource = list;
            //SearchView.DataBindings;
            //lay data
            //them dong hien len grid
        }

        private void SearchView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var url = SearchView.Rows[e.RowIndex].Cells[2].Value.ToString();
            Process.Start(url);
        }
    }
}