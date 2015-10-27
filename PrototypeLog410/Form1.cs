﻿using System;
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
        public Form1()
        {
            InitializeComponent();

            ClassChoice[] classChoicesArray = new ClassChoice[]{
            new ClassChoice{Niveau1 = "Coral", Niveau2="Pink", Pourcentage=72 }
            , new ClassChoice{Niveau1 = "Coral", Niveau2="Red", Pourcentage=21 }
            , new ClassChoice{Niveau1 = "Coral", Niveau2="Blue", Pourcentage=3 }
            , new ClassChoice{Niveau1 = "Coral", Niveau2="Green", Pourcentage=2 }
            , new ClassChoice{Niveau1 = "Fish", Niveau2="Pike", Pourcentage=0.5f }
            , new ClassChoice{Niveau1 = "Fish", Niveau2="Shark", Pourcentage=0.5f }f
            , new ClassChoice{Niveau1 = "Fish", Niveau2="Dolphin", Pourcentage=0.4f }
            , new ClassChoice{Niveau1 = "Other", Niveau2="Sand", Pourcentage=0.2f }
            , new ClassChoice{Niveau1 = "Other", Niveau2="Alga", Pourcentage=0.2f }
            , new ClassChoice{Niveau1 = "Other", Niveau2="Rock", Pourcentage=0.2f }
            };

            int classChoicesCount = classChoicesArray.Length;
            for(int i = 0; i < classChoicesCount; ++i)
            {
                ClassChoice currChoice = classChoicesArray[i];

                if(i < classChoicesCount - 1)
                    classChoices.Rows.Add();

                classChoices.Rows[i].Cells[0].Value = currChoice.Niveau1;
                classChoices.Rows[i].Cells[1].Value = currChoice.Niveau2;
                classChoices.Rows[i].Cells[2].Value = currChoice.Pourcentage;
            }

            addAnnotationPoint(point1, 0.25f, 0.25f);
            addAnnotationPoint(point2, 0.25f, 0.75f);
            addAnnotationPoint(point3, 0.75f, 0.75f);
            addAnnotationPoint(point4, 0.75f, 0.25f);
            addAnnotationPoint(point5, 0.50f, 0.50f);
        }

        private void addAnnotationPoint(PictureBox pictureBox, float xFraction, float yFraction)
        {
            pictureBox.Parent = pictureBox1;
            pictureBox.BackColor = Color.Transparent;
            pictureBox.Location = new Point((int)(pictureBox1.Size.Width * xFraction), (int)(pictureBox1.Size.Height * yFraction));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public class ClassChoice
        {
            public string Niveau1 { get; set; }
            public string Niveau2 { get; set; }
            public float Pourcentage { get; set; }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}