using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototypeLog410
{
    public partial class AnnotationPoint : UserControl
    {
        private bool isPointRed;
        private bool isDragging;
        private Point previousLocation;

        public string Taxonomie
        {
            get { return classification.Text; }
            set
            {
                classification.Text = value;

                string[] classificationStrParts = value.Split(new char[] { '-' });
                string pctStr = classificationStrParts[2].Split(new char[] { '%' })[0].Trim();

                float pourcentage = float.Parse(pctStr);
                if (pourcentage < 25)
                    classification.ForeColor = Color.Red;
                else if (pourcentage < 50)
                    classification.ForeColor = Color.Orange;
                else if (pourcentage < 75)
                    classification.ForeColor = Color.Yellow;
                else
                    classification.ForeColor = Color.Green;
            }
        }
        public float XFraction { get; set; }
        public float YFraction { get; set; }

        public AnnotationPoint()
        {
            InitializeComponent();
        }

        public AnnotationPoint(string taxonomietext, float xFraction, float yFraction)
        {
            InitializeComponent();
            Taxonomie = taxonomietext;
            XFraction = xFraction;
            YFraction = yFraction;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            isPointRed = !isPointRed;
            imgPoint.Image = isPointRed ? Properties.Resources.point : Properties.Resources.point_rouge;            
        }

        public void StartTimer()
        {
            isPointRed = true;
            imgPoint.Image = Properties.Resources.point_rouge;
            timer.Start();
        }

        public void StopTimer()
        {
            timer.Stop();
            isPointRed = false;
            imgPoint.Image = Properties.Resources.point;
        }

        private void imgPoint_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            previousLocation = e.Location;
            Cursor = Cursors.Hand;
        }

        public void RePossitionImage()
        {
            Point point = new Point();
            point.X = (int)(XFraction * Parent.Width) - imgPoint.Location.X;
            point.Y = (int)(YFraction * Parent.Height) - imgPoint.Location.Y;
            Location = point;
        }

        private void imgPoint_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDragging)
                return;
            if (this.imgPoint != sender)
                return;
            Point point = Location;
            point.Offset(e.Location.X - previousLocation.X, e.Location.Y - previousLocation.Y);
            Location = point;

            isDragging = false;
            Cursor = Cursors.Default;
        }
        
    }
}
