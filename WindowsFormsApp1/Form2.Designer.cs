namespace WindowsFormsApp1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Searchbar = new System.Windows.Forms.TextBox();
            this.Searchbtt = new System.Windows.Forms.Button();
            this.SearchView = new System.Windows.Forms.DataGridView();
            this.filenameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.urlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bodyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SearchView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Searchbar
            // 
            this.Searchbar.Location = new System.Drawing.Point(85, 26);
            this.Searchbar.Name = "Searchbar";
            this.Searchbar.Size = new System.Drawing.Size(462, 20);
            this.Searchbar.TabIndex = 0;
            // 
            // Searchbtt
            // 
            this.Searchbtt.Location = new System.Drawing.Point(563, 24);
            this.Searchbtt.Name = "Searchbtt";
            this.Searchbtt.Size = new System.Drawing.Size(75, 23);
            this.Searchbtt.TabIndex = 1;
            this.Searchbtt.Text = "Search";
            this.Searchbtt.UseVisualStyleBackColor = true;
            this.Searchbtt.Click += new System.EventHandler(this.Searchbtt_Click);
            // 
            // SearchView
            // 
            this.SearchView.AllowUserToAddRows = false;
            this.SearchView.AllowUserToDeleteRows = false;
            this.SearchView.AutoGenerateColumns = false;
            this.SearchView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SearchView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.filenameDataGridViewTextBoxColumn,
            this.typesDataGridViewTextBoxColumn,
            this.urlDataGridViewTextBoxColumn,
            this.bodyDataGridViewTextBoxColumn});
            this.SearchView.DataSource = this.filesBindingSource;
            this.SearchView.Location = new System.Drawing.Point(85, 53);
            this.SearchView.Name = "SearchView";
            this.SearchView.ReadOnly = true;
            this.SearchView.Size = new System.Drawing.Size(570, 301);
            this.SearchView.TabIndex = 2;
            this.SearchView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SearchView_CellDoubleClick);
            // 
            // filenameDataGridViewTextBoxColumn
            // 
            this.filenameDataGridViewTextBoxColumn.DataPropertyName = "filename";
            this.filenameDataGridViewTextBoxColumn.HeaderText = "filename";
            this.filenameDataGridViewTextBoxColumn.Name = "filenameDataGridViewTextBoxColumn";
            this.filenameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // typesDataGridViewTextBoxColumn
            // 
            this.typesDataGridViewTextBoxColumn.DataPropertyName = "types";
            this.typesDataGridViewTextBoxColumn.HeaderText = "types";
            this.typesDataGridViewTextBoxColumn.Name = "typesDataGridViewTextBoxColumn";
            this.typesDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // urlDataGridViewTextBoxColumn
            // 
            this.urlDataGridViewTextBoxColumn.DataPropertyName = "url";
            this.urlDataGridViewTextBoxColumn.HeaderText = "url";
            this.urlDataGridViewTextBoxColumn.Name = "urlDataGridViewTextBoxColumn";
            this.urlDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bodyDataGridViewTextBoxColumn
            // 
            this.bodyDataGridViewTextBoxColumn.DataPropertyName = "body";
            this.bodyDataGridViewTextBoxColumn.HeaderText = "body";
            this.bodyDataGridViewTextBoxColumn.Name = "bodyDataGridViewTextBoxColumn";
            this.bodyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // filesBindingSource
            // 
            this.filesBindingSource.DataSource = typeof(WindowsFormsApp1.Files);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SearchView);
            this.Controls.Add(this.Searchbtt);
            this.Controls.Add(this.Searchbar);
            this.Name = "Form2";
            this.Text = "Search";
            ((System.ComponentModel.ISupportInitialize)(this.SearchView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Searchbar;
        private System.Windows.Forms.Button Searchbtt;
        private System.Windows.Forms.DataGridView SearchView;
        private System.Windows.Forms.DataGridViewTextBoxColumn filenameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn urlDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bodyDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource filesBindingSource;
    }
}