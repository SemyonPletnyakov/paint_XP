using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace paint_XP
{
    static class MainPen
    {
        static Color color1 = Color.Black;
        static float size1 = 10;
        static Pen pen1 = new Pen(color1, size1);
        public static Pen Pen1
        {
            get
            {
                return pen1;
            }
        }
        public static Color Color1
        {
            get
            {
                return color1;
            }
            set
            {
                color1 = value;
                pen1 = new Pen(color1, size1);
            }
        }
        public static float Size1
        {
            get
            {
                return size1;
            }
            set
            {
                size1 = value;
                pen1 = new Pen(color1, size1);
            }
        }
    }
}
