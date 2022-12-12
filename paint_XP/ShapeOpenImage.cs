using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace paint_XP
{
    class ShapeOpenImage : IShape
    {
        public ShapeOpenImage(string filename)
        {
            bmp = new Bitmap(filename);
        }
        Bitmap bmp;
        public void LocationStart(Point start1) { }
        public void LocationMove(Point move1) { }
        public void LocationEnd(Point end1) { }
        public void Draw(Graphics g)
        {
            g.DrawImage(bmp, new Point(0, 30));
        }
        public void OpenFile(string filename) 
        {
            //bmp2 = new Bitmap(filename);
            
        }
    }
}
