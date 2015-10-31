using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypeLog410
{
    public class Stage
    {
        public AnnotationPoint[] AnnotationPoints { get; set; }
        public Image Picture { get; set; }
        public bool Rejected { get; set; }

        public Stage()
        {
            AnnotationPoints = new AnnotationPoint[5];
        }
    }
}
