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
            this.point1 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.point3 = new System.Windows.Forms.PictureBox();
            this.point5 = new System.Windows.Forms.PictureBox();
            this.point4 = new System.Windows.Forms.PictureBox();
            this.point2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.classChoices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.point1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.point3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.point5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.point4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.point2)).BeginInit();
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
            // point1
            // 
            this.point1.BackColor = System.Drawing.Color.Transparent;
            this.point1.Image = global::PrototypeLog410.Properties.Resources.point;
            this.point1.Location = new System.Drawing.Point(573, 225);
            this.point1.Name = "point1";
            this.point1.Size = new System.Drawing.Size(20, 20);
            this.point1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.point1.TabIndex = 2;
            this.point1.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PrototypeLog410.Properties.Resources.corail1;
            this.pictureBox1.Location = new System.Drawing.Point(330, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1008, 600);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // point3
            // 
            this.point3.BackColor = System.Drawing.Color.Transparent;
            this.point3.Image = global::PrototypeLog410.Properties.Resources.point;
            this.point3.Location = new System.Drawing.Point(771, 357);
            this.point3.Name = "point3";
            this.point3.Size = new System.Drawing.Size(20, 20);
            this.point3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.point3.TabIndex = 3;
            this.point3.TabStop = false;
            // 
            // point5
            // 
            this.point5.BackColor = System.Drawing.Color.Transparent;
            this.point5.Image = global::PrototypeLog410.Properties.Resources.point;
            this.point5.Location = new System.Drawing.Point(968, 502);
            this.point5.Name = "point5";
            this.point5.Size = new System.Drawing.Size(20, 20);
            this.point5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.point5.TabIndex = 4;
            this.point5.TabStop = false;
            // 
            // point4
            // 
            this.point4.BackColor = System.Drawing.Color.Transparent;
            this.point4.Image = global::PrototypeLog410.Properties.Resources.point;
            this.point4.Location = new System.Drawing.Point(583, 502);
            this.point4.Name = "point4";
            this.point4.Size = new System.Drawing.Size(20, 20);
            this.point4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.point4.TabIndex = 5;
            this.point4.TabStop = false;
            // 
            // point2
            // 
            this.point2.BackColor = System.Drawing.Color.Transparent;
            this.point2.Image = global::PrototypeLog410.Properties.Resources.point;
            this.point2.Location = new System.Drawing.Point(968, 225);
            this.point2.Name = "point2";
            this.point2.Size = new System.Drawing.Size(20, 20);
            this.point2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.point2.TabIndex = 6;
            this.point2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 730);
            this.Controls.Add(this.point2);
            this.Controls.Add(this.point4);
            this.Controls.Add(this.point5);
            this.Controls.Add(this.point3);
            this.Controls.Add(this.point1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.classChoices);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.classChoices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.point1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.point3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.point5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.point4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.point2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView classChoices;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level2;
        private System.Windows.Forms.DataGridViewTextBoxColumn pct;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox point1;
        private System.Windows.Forms.PictureBox point3;
        private System.Windows.Forms.PictureBox point5;
        private System.Windows.Forms.PictureBox point4;
        private System.Windows.Forms.PictureBox point2;
    }
}

