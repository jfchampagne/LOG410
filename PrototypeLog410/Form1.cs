using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrototypeLog410
{
    public partial class Form1 : Form
    {
        private AnnotationPoint[] annotationPoints = new AnnotationPoint[5];
        private int currentPoint;

        public Form1()
        {
            InitializeComponent();

            //Application.AddMessageFilter(new MouseUpEventFilter(onMouseRelease));

            ClassChoice[] classChoicesArray = new ClassChoice[]{
            new ClassChoice{Level1 = "Coral", Level2="Pink", Pourcentage=72 }
            , new ClassChoice{Level1 = "Coral", Level2="Red", Pourcentage=21 }
            , new ClassChoice{Level1 = "Coral", Level2="Blue", Pourcentage=3 }
            , new ClassChoice{Level1 = "Coral", Level2="Green", Pourcentage=2 }
            , new ClassChoice{Level1 = "Fish", Level2="Pike", Pourcentage=0.5f }
            , new ClassChoice{Level1 = "Fish", Level2="Shark", Pourcentage=0.5f }
            , new ClassChoice{Level1 = "Fish", Level2="Dolphin", Pourcentage=0.4f }
            , new ClassChoice{Level1 = "Other", Level2="Sand", Pourcentage=0.2f }
            , new ClassChoice{Level1 = "Other", Level2="Alga", Pourcentage=0.2f }
            , new ClassChoice{Level1 = "Other", Level2="Rock", Pourcentage=0.2f }
            };
            classChoices.DataSource = classChoicesArray;

            SetUp();
        }

        private void SetUp()
        {
            if (annotationPoints[0] != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    pictureBox1.Controls.Remove(annotationPoints[i]);
                }
            }
            

            currentPoint = 0;
            AddAnnotationPoint(new AnnotationPoint("Other - Rock - 22%", 0.25f, 0.25f));
            AddAnnotationPoint(new AnnotationPoint("Coral - Pink - 72%", 0.25f, 0.75f));
            AddAnnotationPoint(new AnnotationPoint("Coral - Pink - 92%", 0.75f, 0.75f));
            AddAnnotationPoint(new AnnotationPoint("Other - Alga - 90%", 0.75f, 0.25f));
            AddAnnotationPoint(new AnnotationPoint("Fish - Pike - 96%", 0.50f, 0.50f));

            currentPoint = 0;
            annotationPoints[currentPoint].StartTimer();
            oldSize = pictureBox1.Size;
        }

        private void AddAnnotationPoint(AnnotationPoint annotationPoint)
        {
            annotationPoints[currentPoint++] = annotationPoint;
            //annotationPoints.Add(annotationPoint);
            pictureBox1.Controls.Add(annotationPoint);
            Size size = pictureBox1.Image.Size;
            annotationPoint.Location = new Point((int)(annotationPoint.XFraction * (float)size.Width), (int)(annotationPoint.YFraction * size.Height));
        }
        

        private void Form1_keyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) //Arrows
            {
                int selectedItem = classChoices.SelectedRows[0].Index;
                classChoices.Rows[selectedItem].Selected = false;
                classChoices.Rows[--selectedItem].Selected = true;
            }
            else if (e.KeyCode == Keys.Down) //Enter
            {
                int selectedItem = classChoices.SelectedRows[0].Index;
                classChoices.Rows[selectedItem].Selected = false;
                classChoices.Rows[++selectedItem].Selected = true;
            }
            else if (e.KeyCode == Keys.Enter) //Enter
            {
                saveSelectedChoice();
            }
            else if (e.KeyCode == Keys.Tab) //Enter
            {
                NextPoint();
            }
            else
            {
                if (!filterTextBox.Focused)
                {
                    filterTextBox.Focus();
                    filterTextBox.Text += Char.ToLower((char)e.KeyValue);

                    if (filterTextBox.Text.Length > 0)
                    {
                        filterTextBox.SelectionStart = filterTextBox.Text.Length;
                        filterTextBox.SelectionLength = 0;
                    }
                }
            }
        }

        private void NextPoint()
        {
            annotationPoints[currentPoint].StopTimer();
            currentPoint = ++currentPoint == 5 ? 0 : currentPoint;
            annotationPoints[currentPoint].StartTimer();

            classChoices.ClearSelection();
            classChoices.Rows[0].Selected = true;
        }

        private void filterTextBox_keyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void filterTextBox_TextChanged(object sender, EventArgs e)
        {
            filterTextBox.Text = filterTextBox.Text.ToLower();
            filterTextBox.SelectionStart = filterTextBox.Text.Length;
            filterTextBox.SelectionLength = 0;

            string[] filters = filterTextBox.Text.Split(new char[] { ' ' });

            foreach (DataGridViewRow classChoice in classChoices.Rows)
            {
                string level1 = classChoice.Cells[0].Value.ToString().ToLower();
                string level2 = classChoice.Cells[1].Value.ToString().ToLower();

                foreach (string filter in filters)
                {
                    if (level1.Contains(filter) || level2.Contains(filter))
                    {
                        classChoice.Visible = true;
                    }
                    else
                    {
                        BindingContext[classChoices.DataSource].SuspendBinding();
                        classChoice.Visible = false;
                        BindingContext[classChoices.DataSource].ResumeBinding();
                        break;
                    }
                }
            }
        }
        
        private void saveSelectedChoice()
        {
            AnnotationPoint selectedPoint = annotationPoints[currentPoint];
            if (selectedPoint == null)
                return;

            string level1 = classChoices.SelectedRows[0].Cells[0].Value.ToString();
            string level2 = classChoices.SelectedRows[0].Cells[1].Value.ToString();
            string pct = classChoices.SelectedRows[0].Cells[2].Value.ToString();

            selectedPoint.Taxonomie = level1 + " - " + level2 + " - " + pct + "%";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadNextImage();
        }

        private void loadNextImage()
        {
            pictureBox1.Image = Properties.Resources.fond_marin2;
            SetUp();
        }


        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaxonomyForm taxonomyForm = new TaxonomyForm();
            taxonomyForm.ShowDialog(this);
        }

        public class ClassChoice
        {
            public string Level1 { get; set; }
            public string Level2 { get; set; }
            public float Pourcentage { get; set; }
        }

        private Size oldSize;
        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            //Size offset = pictureBox1.Size - oldSize;
            foreach (AnnotationPoint point in annotationPoints)
            {
                point.RePossitionImage();

                point.Height = (int)(point.Height * ((float)pictureBox1.Height / (float)oldSize.Height));
                point.Width = (int)(point.Width * ((float)pictureBox1.Width / (float)oldSize.Width));
            }
            oldSize = pictureBox1.Size;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DiableTabStop(this);
        }

        private void DiableTabStop(Control ctrl)
        {
            ctrl.TabStop = false;
            foreach (Control item in ctrl.Controls)
            {
                DiableTabStop(item);
            }
        }
    }
}
