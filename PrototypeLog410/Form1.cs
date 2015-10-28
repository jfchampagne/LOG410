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
        private bool isPointRed;

        private AnnotationPoint selectedPoint;
        private Queue<AnnotationPoint> pointsToClassify;
        private Queue<Image> images;

        public Form1()
        {
            InitializeComponent();

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

            addAnnotationPoint(point1, segment1, classification1, 0.25f, 0.25f);
            addAnnotationPoint(point2, segment2, classification2, 0.25f, 0.75f);
            addAnnotationPoint(point3, segment3, classification3, 0.75f, 0.75f);
            addAnnotationPoint(point4, segment4, classification4, 0.75f, 0.25f);
            addAnnotationPoint(point5, segment5, classification5, 0.50f, 0.50f);

            AnnotationPoint annotation1 = new AnnotationPoint { point = point1, label = classification1 };
            AnnotationPoint annotation2 = new AnnotationPoint { point = point2, label = classification2 };

            pointsToClassify = new Queue<AnnotationPoint>();
            pointsToClassify.Enqueue(annotation1);
            pointsToClassify.Enqueue(annotation2);

            images = new Queue<Image>();
            images.Enqueue(Properties.Resources.fond_marin2);

            selectedPoint = pointsToClassify.Dequeue();

            isPointRed = false;
            Timer redPointTimer = new Timer();
            redPointTimer.Tick += new EventHandler(changeSelectedPointState);
            redPointTimer.Interval = 500;
            redPointTimer.Start();
        }

        private void changeSelectedPointState(object sender, EventArgs args)
        {
            if(selectedPoint == null)
            {
                return;
            }

            if(isPointRed)
            {
                selectedPoint.point.Image = Properties.Resources.point;
            }
            else
            {
                selectedPoint.point.Image = Properties.Resources.point_rouge;
            }

            isPointRed = !isPointRed;
        }

        private void addAnnotationPoint(PictureBox pictureBox, PictureBox segmentPictureBox, Label classificationLabel, float xFraction, float yFraction)
        {
            Point pointCoordinates = new Point((int)(pictureBox1.Size.Width * xFraction), (int)(pictureBox1.Size.Height * yFraction));

            pictureBox.Parent = pictureBox1;
            pictureBox.BackColor = Color.Transparent;
            pictureBox.Location = pointCoordinates;

            segmentPictureBox.Parent = pictureBox1;
            segmentPictureBox.BackColor = Color.Transparent;
            segmentPictureBox.Location = new Point(pointCoordinates.X - (segmentPictureBox.Size.Width - pictureBox.Size.Width) / 2, pointCoordinates.Y - (segmentPictureBox.Size.Height - pictureBox.Size.Height) / 2);

            classificationLabel.Parent = segmentPictureBox;
            classificationLabel.BackColor = Color.Transparent;
            classificationLabel.Location = new Point((segmentPictureBox.Size.Width - classificationLabel.Size.Width) / 2, (segmentPictureBox.Size.Height + pictureBox.Size.Height) / 2 + 5);
            classificationLabel.BringToFront();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public class ClassChoice
        {
            public string Level1 { get; set; }
            public string Level2 { get; set; }
            public float Pourcentage { get; set; }
        }

        public class AnnotationPoint
        {
            public PictureBox point { get; set; }
            public Label label { get; set; }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_keyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyValue >= 37 && e.KeyValue <= 40) //Arrows
            {
                classChoices.Focus();
            }
            else if(e.KeyValue == 13) //Enter
            {
                saveSelectedChoice();
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

        private void filterTextBox_keyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
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

            selectFirstVisibleClassChoice();
        }

        private void selectFirstVisibleClassChoice()
        {
            bool firstAlreadySelected = false;

            foreach (DataGridViewRow row in classChoices.Rows)
            {
                if(firstAlreadySelected || !row.Visible)
                {
                    row.Selected = false;
                }
                else if(row.Visible)
                {
                    firstAlreadySelected = true;
                    row.Selected = true;
                    row.Cells[0].Selected = true;
                    classChoices.CurrentCell = row.Cells[0];
                }
            }
        }

        private void saveSelectedChoice()
        {
            if(selectedPoint == null)
            {
                return;
            }

            string level1 = classChoices.CurrentRow.Cells[0].Value.ToString();
            string level2 = classChoices.CurrentRow.Cells[1].Value.ToString();
            string pct = classChoices.CurrentRow.Cells[2].Value.ToString();

            selectedPoint.label.Text = level1 + " - " + level2 + " - " + pct + "%";
            selectedPoint.label.ForeColor = Color.Lime;
            selectedPoint.point.Image = Properties.Resources.point;

            if (pointsToClassify.Count != 0)
            {
                selectedPoint = pointsToClassify.Dequeue();
            }
            else if(images.Count != 0)
            {
                pictureBox1.Image = images.Dequeue();
                selectedPoint = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(images.Count == 0)
            {
                return;
            }

            selectedPoint.point.Image = Properties.Resources.point;
            selectedPoint.label.ForeColor = Color.Lime;

            if(pointsToClassify.Count != 0)
            {
                selectedPoint = pointsToClassify.Dequeue();
                selectedPoint.point.Image = Properties.Resources.point;
                selectedPoint.label.ForeColor = Color.Lime;
            }

            pictureBox1.Image = images.Dequeue();
            selectedPoint = null;
        }
    }
}
