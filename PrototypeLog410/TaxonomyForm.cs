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

        public TaxonomyForm()
        {
            InitializeComponent();

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

        }

        private void removeParentButton_Click(object sender, EventArgs e)
        {

        }

        public class TaxonomyAssociation
        {
            public Label level1Label { get; set; }
            public Label level2Label { get; set; }
        }
    }
}
