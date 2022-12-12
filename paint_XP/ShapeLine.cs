using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace paint_XP
{
    class ShapeLine:IShape
    {
        Point start1;
        Point end1;
        Pen pen2;
        public void LocationStart(Point start1) 
        {
            this.start1 = start1;
            pen2 = MainPen.Pen1;
        }
        public void LocationMove(Point move1) 
        {
            this.end1 = move1;
            pen2 = MainPen.Pen1;
        }
        public void LocationEnd(Point end1) 
        {
            this.end1 = end1;
            pen2 = MainPen.Pen1;
        }
        public void Draw(Graphics g) 
        {
            g.DrawLine(pen2, start1, end1);
        }
        public void OpenFile(string filename) { }
    }
}
