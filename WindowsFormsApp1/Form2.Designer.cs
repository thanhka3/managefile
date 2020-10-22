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
            this.Searchbar = new System.Windows.Forms.TextBox();
            this.Searchbtt = new System.Windows.Forms.Button();
            this.SearchView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.SearchView)).BeginInit();
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
            this.SearchView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SearchView.Location = new System.Drawing.Point(12, 54);
            this.SearchView.Name = "SearchView";
            this.SearchView.Size = new System.Drawing.Size(776, 352);
            this.SearchView.TabIndex = 2;
            this.SearchView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
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
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.SearchView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Searchbar;
        private System.Windows.Forms.Button Searchbtt;
        private System.Windows.Forms.DataGridView SearchView;
    }
}