using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Support.SearchFile(textsearch, client);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // var url = SearchView.SelectedRows.Cells[3].Text;
            //Process.Start(url);
        }
    }
}