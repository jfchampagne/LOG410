namespace PrototypeLog410
{
    partial class Form1
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
            this.classChoices = new System.Windows.Forms.DataGridView();
            this.Level1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.classChoices)).BeginInit();
            this.SuspendLayout();
            // 
            // classChoices
            // 
            this.classChoices.AccessibleName = "";
            this.classChoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.classChoices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Level1,
            this.Level2,
            this.pct});
            this.classChoices.Location = new System.Drawing.Point(1, 1);
            this.classChoices.Name = "classChoices";
            this.classChoices.RowHeadersWidth = 20;
            this.classChoices.Size = new System.Drawing.Size(322, 462);
            this.classChoices.TabIndex = 0;
            this.classChoices.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Level1
            // 
            this.Level1.HeaderText = "Level 1";
            this.Level1.Name = "Level1";
            this.Level1.ReadOnly = true;
            // 
            // Level2
            // 
            this.Level2.HeaderText = "Level 2";
            this.Level2.Name = "Level2";
            this.Level2.ReadOnly = true;
            // 
            // pct
            // 
            this.pct.HeaderText = "%";
            this.pct.Name = "pct";
            this.pct.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 466);
            this.Controls.Add(this.classChoices);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.classChoices)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView classChoices;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level2;
        private System.Windows.Forms.DataGridViewTextBoxColumn pct;
    }
}

