using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login.UserControls
{
    public partial class Gauge : UserControl
    {
        public int angle = -30;
        public Gauge()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawGauge(e.Graphics);
            DrawArrow(e.Graphics, angle);
        }
        public void DrawGauge(Graphics g)
        {
            int[] array = { -30, -10, 10, 30, 50, 70, 90, 110, 130, 150, 170, 190, 210 };
            Pen pen = new Pen(Color.AntiqueWhite, 2);
            foreach (int angle in array)
            {
                float x1, x2, x3, x4, y1, y2;
                x1 = Convert.ToSingle(99 - (float)(Math.Cos((float)(angle * Math.PI / 180)) * 60));
                x2 = Convert.ToSingle(99 - (float)(Math.Cos((float)(angle * Math.PI / 180)) * 55));

                x3 = Convert.ToSingle(99 + (float)(Math.Cos((float)((180 - angle) * Math.PI / 180)) * 60));
                x4 = Convert.ToSingle(99 + (float)(Math.Cos((float)((180 - angle) * Math.PI / 180)) * 55));
                                       
                y1 = Convert.ToSingle(82 - (float)(Math.Sin((float)(angle * Math.PI / 180)) * 60));
                y2 = Convert.ToSingle(82 - (float)(Math.Sin((float)(angle * Math.PI / 180)) * 55));

                if (angle < 90)
                {
                    g.DrawLine(pen, x2, y2, x1, y1);
                }
                if (angle > 90)
                {
                    g.DrawLine(pen, x4, y2, x3, y1);
                }
                if (angle == 90)
                {
                    g.DrawLine(pen, 99, 22, 99, 29);
                }
                g.DrawArc(pen, 39, 22, 120, 120, 150, 240);
            }
        }
        public void DrawArrow(Graphics g, int angle1)
        {
            Pen pen = new Pen(Color.AntiqueWhite, 2);
            g.FillEllipse(Brushes.AntiqueWhite, 96, 79, 6, 6);
            if (angle1 < 90)
            {
                float x1 = Convert.ToSingle(99 - (float)(Math.Cos((float)(angle1 * Math.PI / 180)) * 50)),
                      y1 = Convert.ToSingle(82 - (float)(Math.Sin((float)(angle1 * Math.PI / 180)) * 50));
                g.DrawLine(pen, x1, y1, 99, 82);
            }
            if (angle1 == 90)
            {
                g.DrawLine(pen, 99, 32, 99, 82);
            }
            if (angle1 > 90)
            {
                float x2 = Convert.ToSingle(99 + (float)(Math.Cos((float)((180 - angle1) * Math.PI / 180)) * 50)),
                      y2 = Convert.ToSingle(82 - (float)(Math.Sin((float)((180 - angle1) * Math.PI / 180)) * 50));
                g.DrawLine(pen, x2, y2, 99, 82);
            }
        }
    }
}