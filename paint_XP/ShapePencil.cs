using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace paint_XP
{
    class ShapePencil:IShape
    {
        Point start1;
        Point end1;
        Pen pen2;
        List<Point> points = new List<Point>();
        
        public void LocationStart(Point start1)
        {
            pen2 = MainPen.Pen1;
            points.Add(start1);
        }
        public void LocationMove(Point move1) { }
        public void LocationEnd(Point end1)
        {
            pen2 = MainPen.Pen1;
            points.Add(end1);
        }
        public void Draw(Graphics g)
        {
            g.DrawLines(pen2, points.ToArray());
        }
        public void OpenFile(string filename) { }
    }
}
