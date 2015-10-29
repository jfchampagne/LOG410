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
        private bool changingPoint;

        private AnnotationPointDrag annotationPointDrag;

        public Form1()
        {
            InitializeComponent();

            Application.AddMessageFilter(new MouseUpEventFilter(onMouseRelease));

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
            AnnotationPoint annotation3 = new AnnotationPoint { point = point3, label = classification3 };
            AnnotationPoint annotation4 = new AnnotationPoint { point = point4, label = classification4 };
            AnnotationPoint annotation5 = new AnnotationPoint { point = point5, label = classification5 };

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

            changingPoint = false;

            annotationPointDrag = null;
        }

        private void changeSelectedPointState(object sender, EventArgs args)
        {
            if (selectedPoint == null)
            {
                return;
            }

            if (isPointRed)
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
            pictureBox.Parent = pictureBox1;
            pictureBox.BackColor = Color.Transparent;

            segmentPictureBox.Parent = pictureBox1;
            segmentPictureBox.BackColor = Color.Transparent;

            classificationLabel.Parent = segmentPictureBox;
            classificationLabel.BackColor = Color.Transparent;

            positionAnnotationPoint(xFraction, yFraction, pictureBox, segmentPictureBox, classificationLabel);

            classificationLabel.BringToFront();
        }

        private void positionAnnotationPoint(float xFraction, float yFraction, PictureBox pictureBox, PictureBox segmentPictureBox, Label classificationLabel)
        {
            Point pointCoordinates = new Point((int)(pictureBox1.Size.Width * xFraction), (int)(pictureBox1.Size.Height * yFraction));
            pictureBox.Location = pointCoordinates;

            positionSegmentAndLabel(pointCoordinates, pictureBox, segmentPictureBox, classificationLabel);
        }

        private void positionSegmentAndLabel(Point pointCoordinates, PictureBox pictureBox, PictureBox segmentPictureBox, Label classificationLabel)
        {
            segmentPictureBox.Location = new Point(pointCoordinates.X - (segmentPictureBox.Size.Width - pictureBox.Size.Width) / 2, pointCoordinates.Y - (segmentPictureBox.Size.Height - pictureBox.Size.Height) / 2);

            classificationLabel.Location = new Point((segmentPictureBox.Size.Width - classificationLabel.Size.Width) / 2, (segmentPictureBox.Size.Height + pictureBox.Size.Height) / 2 + 5);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_keyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyValue >= 37 && e.KeyValue <= 40) //Arrows
            {
                classChoices.Focus();
            }
            else if (e.KeyValue == 13) //Enter
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

            if (!changingPoint)
            {
                selectFirstVisibleClassChoice();
            }

            changingPoint = false;
        }

        private void selectFirstVisibleClassChoice()
        {
            bool firstAlreadySelected = false;

            foreach (DataGridViewRow row in classChoices.Rows)
            {
                if (firstAlreadySelected || !row.Visible)
                {
                    row.Selected = false;
                }
                else if (row.Visible)
                {
                    firstAlreadySelected = true;
                    selectRow(row);
                }
            }
        }

        private void selectRow(DataGridViewRow row)
        {
            row.Selected = true;
            row.Cells[0].Selected = true;
            classChoices.CurrentCell = row.Cells[0];
        }

        private void saveSelectedChoice()
        {
            if (selectedPoint == null)
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
            else if (images.Count != 0)
            {
                loadNextImage();
            }

            selectCurrClassification();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (images.Count == 0)
            {
                return;
            }

            selectedPoint.point.Image = Properties.Resources.point;
            selectedPoint.label.ForeColor = Color.Lime;

            if (pointsToClassify.Count != 0)
            {
                selectedPoint = pointsToClassify.Dequeue();
                selectedPoint.point.Image = Properties.Resources.point;
                selectedPoint.label.ForeColor = Color.Lime;
            }

            loadNextImage();
        }

        private void loadNextImage()
        {
            pictureBox1.Image = images.Dequeue();
            selectedPoint = null;

            positionAnnotationPoint(0.25f, 0.25f, point1, segment1, classification1);
            positionAnnotationPoint(0.25f, 0.75f, point2, segment2, classification2);
            positionAnnotationPoint(0.75f, 0.75f, point3, segment3, classification3);
            positionAnnotationPoint(0.75f, 0.25f, point4, segment4, classification4);
            positionAnnotationPoint(0.50f, 0.50f, point5, segment5, classification5);
        }

        private void segment1_Click(object sender, EventArgs e)
        {
            if (classification1.ForeColor == Color.Lime)
            {
                AnnotationPoint annotationPoint = new AnnotationPoint { point = point1, label = classification1 };
                onPointSelectedManually(annotationPoint);
            }
        }

        private void segment2_Click(object sender, EventArgs e)
        {
            if (classification2.ForeColor == Color.Lime)
            {
                AnnotationPoint annotationPoint = new AnnotationPoint { point = point2, label = classification2 };
                onPointSelectedManually(annotationPoint);
            }
        }

        private void segment3_Click(object sender, EventArgs e)
        {
            AnnotationPoint annotationPoint = new AnnotationPoint { point = point3, label = classification3 };
            onPointSelectedManually(annotationPoint);
        }

        private void segment4_Click(object sender, EventArgs e)
        {
            AnnotationPoint annotationPoint = new AnnotationPoint { point = point4, label = classification4 };
            onPointSelectedManually(annotationPoint);
        }

        private void segment5_Click(object sender, EventArgs e)
        {
            AnnotationPoint annotationPoint = new AnnotationPoint { point = point5, label = classification5 };
            onPointSelectedManually(annotationPoint);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (classification1.ForeColor == Color.Lime)
            {
                AnnotationPoint annotationPoint = new AnnotationPoint { point = point1, label = classification1 };
                onPointSelectedManually(annotationPoint);
            }
        }

        private void classification2_Click(object sender, EventArgs e)
        {
            if (classification2.ForeColor == Color.Lime)
            {
                AnnotationPoint annotationPoint = new AnnotationPoint { point = point2, label = classification2 };
                onPointSelectedManually(annotationPoint);
            }
        }

        private void classification3_Click(object sender, EventArgs e)
        {
            AnnotationPoint annotationPoint = new AnnotationPoint { point = point3, label = classification3 };
            onPointSelectedManually(annotationPoint);
        }

        private void classification4_Click(object sender, EventArgs e)
        {
            AnnotationPoint annotationPoint = new AnnotationPoint { point = point4, label = classification4 };
            onPointSelectedManually(annotationPoint);
        }

        private void classification5_Click(object sender, EventArgs e)
        {
            AnnotationPoint annotationPoint = new AnnotationPoint { point = point5, label = classification5 };
            onPointSelectedManually(annotationPoint);
        }

        private void point1_Click(object sender, MouseEventArgs e)
        {
            if (classification1.ForeColor == Color.Lime)
            {
                AnnotationPoint annotationPoint = new AnnotationPoint { point = point1, label = classification1 };
                onPointSelectedManually(annotationPoint);
            }
        }

        private void point2_Click(object sender, MouseEventArgs e)
        {
            AnnotationPoint annotationPoint = new AnnotationPoint { point = point2, label = classification2 };

            if (classification2.ForeColor == Color.Lime)
            {
                onPointSelectedManually(annotationPoint);
            }

            startDraggingPoint(annotationPoint, e);
        }

        private void point3_Click(object sender, MouseEventArgs e)
        {
            AnnotationPoint annotationPoint = new AnnotationPoint { point = point3, label = classification3 };
            onPointSelectedManually(annotationPoint);

            startDraggingPoint(annotationPoint, e);
        }

        private void point4_Click(object sender, MouseEventArgs e)
        {
            AnnotationPoint annotationPoint = new AnnotationPoint { point = point4, label = classification4 };
            onPointSelectedManually(annotationPoint);

            startDraggingPoint(annotationPoint, e);
        }

        private void point5_Click(object sender, MouseEventArgs e)
        {
            AnnotationPoint annotationPoint = new AnnotationPoint { point = point5, label = classification5 };
            onPointSelectedManually(annotationPoint);

            startDraggingPoint(annotationPoint, e);
        }

        private void mouseDownOnPoint1(object sender, MouseEventArgs e)
        {
            AnnotationPoint annotationPoint = new AnnotationPoint { point = point1, label = classification1, segment = segment1 };
            startDraggingPoint(annotationPoint, e);
        }

        private void mouseDownOnPoint2(object sender, MouseEventArgs e)
        {
            AnnotationPoint annotationPoint = new AnnotationPoint { point = point2, label = classification2, segment = segment2 };
            startDraggingPoint(annotationPoint, e);
        }

        private void mouseDownOnPoint3(object sender, MouseEventArgs e)
        {
            AnnotationPoint annotationPoint = new AnnotationPoint { point = point3, label = classification3, segment = segment3 };
            startDraggingPoint(annotationPoint, e);
        }

        private void mouseDownOnPoint4(object sender, MouseEventArgs e)
        {
            AnnotationPoint annotationPoint = new AnnotationPoint { point = point4, label = classification4, segment = segment4 };
            startDraggingPoint(annotationPoint, e);
        }

        private void mouseDownOnPoint5(object sender, MouseEventArgs e)
        {
            AnnotationPoint annotationPoint = new AnnotationPoint { point = point5, label = classification5, segment = segment5 };
            startDraggingPoint(annotationPoint, e);
        }

        private void startDraggingPoint(AnnotationPoint annotationPoint, MouseEventArgs e)
        {
            annotationPointDrag = new AnnotationPointDrag { startX = Cursor.Position.X, startY = Cursor.Position.Y, point = annotationPoint };
        }

        private void onMouseRelease(int mouseX, int mouseY)
        {
            if(annotationPointDrag != null)
            {
                int xTranslation = mouseX - annotationPointDrag.startX;
                int yTranslation = mouseY - annotationPointDrag.startY;

                Point currLocation = annotationPointDrag.point.point.Location;
                Point newPointCoordinates = new Point(currLocation.X + xTranslation, currLocation.Y + yTranslation);
                annotationPointDrag.point.point.Location = newPointCoordinates;
                positionSegmentAndLabel(newPointCoordinates, annotationPointDrag.point.point, annotationPointDrag.point.segment, annotationPointDrag.point.label);

                annotationPointDrag = null;
            }
        }

        private void onPointSelectedManually(AnnotationPoint annotationPoint)
        {
            if(selectedPoint != null)
            {
                selectedPoint.point.Image = Properties.Resources.point;

                if (selectedPoint.label.ForeColor != Color.Lime)
                {
                    pointsToClassify.Enqueue(selectedPoint);
                }
            }

            selectedPoint = annotationPoint;

            selectCurrClassification();
        }

        private void selectCurrClassification()
        {
            changingPoint = true;

            filterTextBox.Text = "";

            if(selectedPoint != null)
            {
                foreach (DataGridViewRow row in classChoices.Rows)
                {
                    row.Selected = false;
                }

                string[] classInfo = selectedPoint.label.Text.Split(new char[] { '-' });
                foreach(DataGridViewRow row in classChoices.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(classInfo[0].Trim()) &&
                       row.Cells[1].Value.ToString().Equals(classInfo[1].Trim()))
                    {
                        selectRow(row);
                        break;
                    }
                }
            }
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

        public class AnnotationPoint
        {
            public PictureBox point { get; set; }
            public Label label { get; set; }
            public PictureBox segment { get; set; }
        }

        public class AnnotationPointDrag
        {
            public int startX { get; set; }
            public int startY { get; set; }
            public AnnotationPoint point { get; set; }
        }
    }
}
