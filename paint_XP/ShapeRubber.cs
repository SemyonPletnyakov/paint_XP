using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace paint_XP
{
    class ShapeRubber:IShape
    {
        Point start1;
        Point end1;
        Pen pen2;
        List<Point> points = new List<Point>();

        public void LocationStart(Point start1)
        {
            this.start1 = start1;
            pen2 = new Pen(Color.White, MainPen.Size1);
            points.Add(start1);
        }
        public void LocationMove(Point move1) { }
        public void LocationEnd(Point end1)
        {
            this.end1 = end1;
            pen2 = new Pen(Color.White, MainPen.Size1);
            points.Add(end1);
        }
        public void Draw(Graphics g)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                g.DrawLine(pen2, points[i], points[i + 1]);
            }

        }
        public void OpenFile(string filename) { }
    }
}
