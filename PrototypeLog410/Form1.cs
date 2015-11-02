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
        //private AnnotationPoint[] annotationPoints = new AnnotationPoint[5];
        private Stage[] stages = new Stage[3];
        public Image Picture { get; set; }
        private int currentPoint;
        private int currentStage;

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
            LoadData();
            SetUp();
        }

        private void LoadData()
        {
            for (int j = 0; j < 3; j++)
            {
                Stage stage = new Stage();

                currentPoint = 0;
                currentStage = j;
                AddAnnotationPoint(stage, new AnnotationPoint("Other - Rock - 22%", 0.25f, 0.25f));
                AddAnnotationPoint(stage, new AnnotationPoint("Coral - Pink - 72%", 0.25f, 0.75f));
                AddAnnotationPoint(stage, new AnnotationPoint("Coral - Pink - 92%", 0.75f, 0.75f));
                AddAnnotationPoint(stage, new AnnotationPoint("Other - Alga - 90%", 0.75f, 0.25f));
                AddAnnotationPoint(stage, new AnnotationPoint("Fish - Pike - 96%", 0.50f, 0.50f));

                stages[currentStage] = stage;
            }
            stages[0].Picture = Properties.Resources.corail1;
            stages[1].Picture = Properties.Resources.fond_marin2;
            stages[2].Picture = Properties.Resources.Tropical_fish_nocturnal_mirage_37596601_1000_633;
            currentPoint = 0;
            currentStage = 0;
            stages[0].AnnotationPoints[currentPoint].StartTimer();
            oldSize = pictureBox1.Size;
        }

        private void SetUp()
        {
            imgRejected.Visible = false;
            for (int i = 0; i < 5; i++)
            {
                pictureBox1.Controls.Add(stages[currentStage].AnnotationPoints[i]);
            }

            currentPoint = 0;
            stages[currentStage].AnnotationPoints[currentPoint].StartTimer();
            pictureBox1.Image = stages[currentStage].Picture;
            oldSize = pictureBox1.Size;

            if (stages[currentStage].Rejected)
                Reject();
        }

        private void AddAnnotationPoint(Stage stage, AnnotationPoint annotationPoint)
        {
            stage.AnnotationPoints[currentPoint++] = annotationPoint;
            //annotationPoints.Add(annotationPoint);
            //pictureBox1.Controls.Add(annotationPoint);
            Size size = pictureBox1.Image.Size;
            annotationPoint.Location = new Point((int)(annotationPoint.XFraction * (float)size.Width), (int)(annotationPoint.YFraction * size.Height));
        }
        

        private void Form1_keyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) //Arrows
            {
                SelectPreviousItem();
            }
            else if (e.KeyCode == Keys.Down) //Enter
            {
                SelectNextItem();
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

        private void SelectNextItem()
        {
            int selectedItem = classChoices.CurrentRow.Index;

            int rowsCount = classChoices.Rows.Count;
            for(int i = selectedItem + 1; i < rowsCount; ++i)
            {
                DataGridViewRow currRow = classChoices.Rows[i];
                if(currRow.Visible)
                {
                    classChoices.CurrentRow.Selected = false;
                    selectRow(currRow);
                    break;
                }
            }
        }

        private void SelectPreviousItem()
        {
            int selectedItem = classChoices.CurrentRow.Index;

            int rowsCount = classChoices.Rows.Count;
            for (int i = selectedItem - 1; i >= 0; --i)
            {
                DataGridViewRow currRow = classChoices.Rows[i];
                if (currRow.Visible)
                {
                    classChoices.CurrentRow.Selected = false;
                    selectRow(currRow);
                    break;
                }
            }
        }

        private void NextPoint()
        {
            stages[currentStage].AnnotationPoints[currentPoint].StopTimer();
            currentPoint = ++currentPoint == 5 ? 0 : currentPoint;
            stages[currentStage].AnnotationPoints[currentPoint].StartTimer();

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

            selectFirstVisibleClassChoice();
        }
        
        private void saveSelectedChoice()
        {
            AnnotationPoint selectedPoint = stages[currentStage].AnnotationPoints[currentPoint];
            if (selectedPoint == null)
                return;

            string level1 = classChoices.CurrentRow.Cells[0].Value.ToString();
            string level2 = classChoices.CurrentRow.Cells[1].Value.ToString();
            string pct = classChoices.CurrentRow.Cells[2].Value.ToString();

            filterTextBox.Clear();

            selectedPoint.Taxonomie = level1 + " - " + level2 + " - " + pct + "%";

            NextPoint();
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
            foreach (AnnotationPoint point in stages[currentStage].AnnotationPoints)
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

        private void button2_Click(object sender, EventArgs e)
        {
            Clean();
            currentStage = ++currentStage > 2 ? 0 : currentStage;
            SetUp();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clean();
            currentStage = --currentStage < 0 ? 2 : currentStage;
            SetUp();
        }

        private void Clean()
        {
            for (int i = 0; i < 5; i++)
            {
                pictureBox1.Controls.Remove(stages[currentStage].AnnotationPoints[i]);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            stages[currentStage].Rejected = true;
            Reject();
        }
        private void Reject()
        {
            pictureBox1.Controls.Clear();
            imgRejected.Parent = pictureBox1;
            imgRejected.BackColor = Color.Transparent;
            imgRejected.Visible = true;
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
}
}
