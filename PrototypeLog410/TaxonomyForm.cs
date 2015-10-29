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
    public partial class TaxonomyForm : Form
    {
        private List<TaxonomyAssociation> taxonomyAssociations;
        private TaxonomyTransformation currTaxonomyTransformation;

        public TaxonomyForm()
        {
            InitializeComponent();

            currTaxonomyTransformation = null;

            taxonomyAssociations = new List<TaxonomyAssociation>();

            taxonomyAssociations.Add(new TaxonomyAssociation { level1Label = label1, level2Label = label4 });
            taxonomyAssociations.Add(new TaxonomyAssociation { level1Label = label1, level2Label = label5 });
            taxonomyAssociations.Add(new TaxonomyAssociation { level1Label = label1, level2Label = label6 });
            taxonomyAssociations.Add(new TaxonomyAssociation { level1Label = label1, level2Label = label7 });
            taxonomyAssociations.Add(new TaxonomyAssociation { level1Label = label2, level2Label = label8 });
            taxonomyAssociations.Add(new TaxonomyAssociation { level1Label = label2, level2Label = label9 });
            taxonomyAssociations.Add(new TaxonomyAssociation { level1Label = label2, level2Label = label10 });
            taxonomyAssociations.Add(new TaxonomyAssociation { level1Label = label3, level2Label = label11 });
            taxonomyAssociations.Add(new TaxonomyAssociation { level1Label = label3, level2Label = label12 });
            taxonomyAssociations.Add(new TaxonomyAssociation { level1Label = label3, level2Label = label13 });
        }

        private void onPaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black);

            foreach(TaxonomyAssociation association in taxonomyAssociations)
            {
                Point start = new Point(association.level1Label.Location.X + association.level1Label.Size.Width / 2,
                    association.level1Label.Location.Y + association.level1Label.Size.Height);
                Point end = new Point(association.level2Label.Location.X + association.level2Label.Size.Width / 2,
                    association.level2Label.Location.Y);

                e.Graphics.DrawLine(pen, start, end);
            }
        }

        private void addParentButton_Click(object sender, EventArgs e)
        {
            resetLabelBackColors();
            currTaxonomyTransformation = new TaxonomyTransformation { isAddition = true, level1Label = null, level2Label = null };
        }

        private void removeParentButton_Click(object sender, EventArgs e)
        {
            resetLabelBackColors();
            currTaxonomyTransformation = new TaxonomyTransformation { isAddition = false, level1Label = null, level2Label = null };
        }

        private void resetLabelBackColors()
        {
            label4.BackColor = Control.DefaultBackColor;
            label5.BackColor = Control.DefaultBackColor;
            label6.BackColor = Control.DefaultBackColor;
            label7.BackColor = Control.DefaultBackColor;
            label8.BackColor = Control.DefaultBackColor;
            label9.BackColor = Control.DefaultBackColor;
            label10.BackColor = Control.DefaultBackColor;
            label11.BackColor = Control.DefaultBackColor;
            label12.BackColor = Control.DefaultBackColor;
            label13.BackColor = Control.DefaultBackColor;
        }

        public class TaxonomyAssociation
        {
            public Label level1Label { get; set; }
            public Label level2Label { get; set; }
        }

        public class TaxonomyTransformation
        {
            public bool isAddition { get; set; }
            public Label level1Label { get; set; }
            public Label level2Label { get; set; }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            onLevel2LabelClicked(label4);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            onLevel2LabelClicked(label5);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            onLevel2LabelClicked(label6);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            onLevel2LabelClicked(label7);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            onLevel2LabelClicked(label8);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            onLevel2LabelClicked(label9);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            onLevel2LabelClicked(label10);
        }

        private void label11_Click(object sender, EventArgs e)
        {
            onLevel2LabelClicked(label11);
        }

        private void label12_Click(object sender, EventArgs e)
        {
            onLevel2LabelClicked(label12);
        }

        private void label13_Click(object sender, EventArgs e)
        {
            onLevel2LabelClicked(label13);
        }

        private void onLevel2LabelClicked(Label label)
        {
            if(currTaxonomyTransformation == null || currTaxonomyTransformation.level2Label != null)
            {
                return;
            }

            label.BackColor = Color.Lime;
            currTaxonomyTransformation.level2Label = label;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            onLevel1LabelClicked(label1);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            onLevel1LabelClicked(label2);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            onLevel1LabelClicked(label3);
        }

        private void onLevel1LabelClicked(Label label)
        {
            if (currTaxonomyTransformation == null || currTaxonomyTransformation.level2Label == null)
            {
                return;
            }

            if (currTaxonomyTransformation.isAddition)
            {
                if (!isParent(label, currTaxonomyTransformation.level2Label))
                {
                    taxonomyAssociations.Add(new TaxonomyAssociation { level1Label = label, level2Label = currTaxonomyTransformation.level2Label });
                }
            }
            else
            {
                int indexToRemove = -1;
                int associationsCount = taxonomyAssociations.Count;
                for(int i = 0; i < associationsCount; ++i)
                {
                    TaxonomyAssociation currAssociation = taxonomyAssociations[i];
                    if(currAssociation.level1Label == label && currAssociation.level2Label == currTaxonomyTransformation.level2Label)
                    {
                        indexToRemove = i;
                        break;
                    }
                }

                if(indexToRemove != -1)
                {
                    taxonomyAssociations.RemoveAt(indexToRemove);
                }
            }

            currTaxonomyTransformation = null;
            resetLabelBackColors();

            Invalidate();
            Update();
        }

        private bool isParent(Label level1Label, Label level2Label)
        {
            foreach(TaxonomyAssociation association in taxonomyAssociations)
            {
                if(association.level1Label == level1Label && association.level2Label == level2Label)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
