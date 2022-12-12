using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace paint_XP
{
    interface IShape
    {
        void LocationStart(Point start1);
        void LocationMove(Point move1);
        void LocationEnd(Point end1);
        void Draw(Graphics g);
        void OpenFile(string filename);
    }
}
