using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace paint_XP
{
    class ShapeRectangle:IShape
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
            Rectangle rect = new Rectangle(start1.X, start1.Y, end1.X - start1.X, end1.Y - start1.Y);
            if (end1.X - start1.X >= 0 && end1.Y - start1.Y >= 0) rect = new Rectangle(start1.X, start1.Y, Math.Abs(end1.X - start1.X), Math.Abs(end1.Y - start1.Y));
            else if (end1.X - start1.X < 0 && end1.Y - start1.Y >= 0) rect = new Rectangle(end1.X, start1.Y, Math.Abs(end1.X - start1.X), Math.Abs(end1.Y - start1.Y));
            else if (end1.X - start1.X >= 0 && end1.Y - start1.Y < 0) rect = new Rectangle(start1.X, end1.Y, Math.Abs(end1.X - start1.X), Math.Abs(end1.Y - start1.Y));
            else rect = new Rectangle(end1.X,end1.Y, Math.Abs(end1.X - start1.X), Math.Abs(end1.Y - start1.Y));
            g.DrawRectangle(pen2, rect);
        }
        public void OpenFile(string filename) { }
    }
}
