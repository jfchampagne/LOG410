namespace PrototypeLog410
{
    partial class AnnotationPoint
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imgPoint = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.segment = new System.Windows.Forms.Panel();
            this.classification = new PrototypeLog410.CustomLabel();
            ((System.ComponentModel.ISupportInitialize)(this.imgPoint)).BeginInit();
            this.segment.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgPoint
            // 
            this.imgPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imgPoint.BackColor = System.Drawing.Color.Transparent;
            this.imgPoint.Image = global::PrototypeLog410.Properties.Resources.point;
            this.imgPoint.Location = new System.Drawing.Point(88, 51);
            this.imgPoint.Name = "imgPoint";
            this.imgPoint.Size = new System.Drawing.Size(20, 20);
            this.imgPoint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imgPoint.TabIndex = 25;
            this.imgPoint.TabStop = false;
            this.imgPoint.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgPoint_MouseDown);
            this.imgPoint.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imgPoint_MouseMove);
            this.imgPoint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imgPoint_MouseUp);
            // 
            // timer
            // 
            this.timer.Interval = 300;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // segment
            // 
            this.segment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.segment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.segment.Controls.Add(this.classification);
            this.segment.Location = new System.Drawing.Point(0, 3);
            this.segment.Name = "segment";
            this.segment.Size = new System.Drawing.Size(192, 120);
            this.segment.TabIndex = 30;
            // 
            // classification
            // 
            this.classification.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.classification.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.classification.ForeColor = System.Drawing.Color.Red;
            this.classification.Location = new System.Drawing.Point(2, 70);
            this.classification.Name = "classification";
            this.classification.OutlineForeColor = System.Drawing.Color.Black;
            this.classification.OutlineWidth = 1F;
            this.classification.Size = new System.Drawing.Size(185, 25);
            this.classification.TabIndex = 29;
            this.classification.Text = "Other - Rock - 22%";
            this.classification.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AnnotationPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.imgPoint);
            this.Controls.Add(this.segment);
            this.Name = "AnnotationPoint";
            this.Size = new System.Drawing.Size(192, 123);
            ((System.ComponentModel.ISupportInitialize)(this.imgPoint)).EndInit();
            this.segment.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox imgPoint;
        private PrototypeLog410.CustomLabel classification;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Panel segment;
    }
}
