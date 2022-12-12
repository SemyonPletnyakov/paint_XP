using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Drawing2D;

namespace paint_XP
{
    public partial class Form1 : Form
    {
        List<IShape> shapes;
        bool mouseDown;

        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        string filename = "Безымянный";
        bool fileLocation = false;
        public Form1()
        {
            InitializeComponent();

            selectShapeComboBox.Items.Add("Линия");
            selectShapeComboBox.Items.Add("Прямоугольник");
            selectShapeComboBox.Items.Add("Эллипс");
            selectShapeComboBox.Items.Add("Карандаш");
            selectShapeComboBox.Items.Add("Ластик");
            selectShapeComboBox.SelectedItem = "Карандаш";

            this.Text = "Безымянный - Paint";
            shapes = new List<IShape>();
            MainPen.Color1 = Color.Black;
            MainPen.Size1 = 5;
            mouseDown = false;

            saveFileDialog1.Filter = "BMP файлы (*.bmp)|*.bmp|Все файлы(*.*)|*.*";
            openFileDialog1.Filter = "BPM файлы (*.bmp)|*.bmp|Все файлы(*.*)|*.*";
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (!openFile)
            {
                foreach (IShape shape in shapes)
                {
                    shape.Draw(e.Graphics);
                    //e.Graphics.DrawLine(pen, new Point(100, 100), new Point(200, 200));
                }
            }
            else
            {
                shapes.Clear();
                Bitmap bmp = new Bitmap(filename);
                e.Graphics.DrawImage(bmp, new Point(panel1.Location.X, panel1.Location.Y));
                shapes.Add(new ShapeOpenImage(filename));
                openFile = false;
            }
        }
        IShape AddIShape()
        {
            if (selectShapeComboBox.SelectedItem.ToString() == "Линия")
            {
                return new ShapeLine();
            }
            else if(selectShapeComboBox.SelectedItem.ToString() == "Прямоугольник")
            {
                return new ShapeRectangle();
            }
            else if (selectShapeComboBox.SelectedItem.ToString() == "Эллипс")
            {
                return new ShapeEllipse();
            }
            else if (selectShapeComboBox.SelectedItem.ToString() == "Карандаш")
            {
                return new ShapePencil();
            }
            else if (selectShapeComboBox.SelectedItem.ToString() == "Ластик")
            {
                return new ShapeRubber();
            }
            else return new ShapeLine();
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!pipetteBool)
            {
                if (e.Button == MouseButtons.Left)
                {
                    shapes.Add(AddIShape());
                    shapes.Last().LocationStart(e.Location);
                    shapes.Last().LocationEnd(e.Location);
                    panel1.Invalidate();
                    mouseDown = true;
                }
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!pipetteBool)
            {
                if (e.Button == MouseButtons.Left||e.Button == MouseButtons.Right && e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle && e.Button == MouseButtons.Right && e.Button == MouseButtons.Left)
                {
                    if (mouseDown)
                    {
                        shapes.Last().LocationEnd(e.Location);
                        panel1.Invalidate();
                    }
                }
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!pipetteBool)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (mouseDown)
                    {
                        mouseDown = false;
                        shapes.Last().LocationEnd(e.Location);
                        panel1.Invalidate();
                    }
                }
            }
            else
            {
                Bitmap bmp = new Bitmap(panel1.Width,panel1.Height);
                panel1.DrawToBitmap(bmp, panel1.ClientRectangle);
                colorButton.BackColor = bmp.GetPixel(e.X, e.Y);
                MainPen.Color1 = bmp.GetPixel(e.X, e.Y);
                panel1.Invalidate();
                pipetteButton.Checked = false;
                pipetteBool = false;
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            pipetteButton.Checked = false;
            pipetteBool = false;
            ColorDialog cd = new ColorDialog();
            if(cd.ShowDialog() == DialogResult.OK)
            {
                MainPen.Color1 = cd.Color;
                colorButton.BackColor = cd.Color;
            }
        }

        private void numericUpDown1_Scroll(object sender, ScrollEventArgs e)
        {
            MainPen.Size1 = Convert.ToInt32(numericUpDown1.Value);
        }

        private void numericUpDown1_Scroll(object sender, EventArgs e)
        {
            MainPen.Size1 = Convert.ToInt32(numericUpDown1.Value);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            pipetteButton.Checked = false;
            pipetteBool = false;
            shapes.Clear();
            panel1.Invalidate();
        }

        bool pipetteBool = false;

        private void pipetteButton_Click(object sender, EventArgs e)
        {
            if (!pipetteBool)
            {
                pipetteButton.Checked = true;
                pipetteBool = true;
            }
            else
            {
                pipetteButton.Checked = false;
                pipetteBool = false;
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shapes.RemoveAt(shapes.Count-1);
            panel1.Invalidate();
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string k = "Безымянный";
            if (fileLocation) k = filename.Remove(0, filename.LastIndexOf(@"\")).Remove(0, 1);

            DialogResult dialogResult = MessageBox.Show("Сохранить изменения в файле \"" + k + "\"?", "Paint", MessageBoxButtons.YesNoCancel);
            if (dialogResult == DialogResult.Yes)
            {
                saveToolStripMenuItem_Click(sender, e);
                MainPen.Color1 = Color.Black;
                MainPen.Size1 = 5;
                mouseDown = false;
                clearButton_Click(sender, e);
                this.Text = "Безымянный - Paint";
                fileLocation = false;
                filename = "Безымянный";
            }
            else if (dialogResult == DialogResult.No)
            {
                MainPen.Color1 = Color.Black;
                MainPen.Size1 = 5;
                mouseDown = false;
                clearButton_Click(sender, e);
                this.Text = "Безымянный - Paint";
                fileLocation = false;
                filename = "Безымянный";
            }
            else if (dialogResult == DialogResult.Cancel)
            {

            }
        }
        bool openFile = false;
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string k = "Безымянный";
            if (fileLocation) k = filename.Remove(0, filename.LastIndexOf(@"\")).Remove(0, 1);

            DialogResult dialogResult = MessageBox.Show("Сохранить изменения в файле \"" + k + "\"?", "Paint", MessageBoxButtons.YesNoCancel);
            if (dialogResult == DialogResult.Yes)
            {
                saveToolStripMenuItem_Click(sender, e);
                openFileDialog1.FileName = "";
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                filename = openFileDialog1.FileName;
                openFile = true;
                panel1.Invalidate();
                this.Text = filename.Remove(0, filename.LastIndexOf(@"\")).Remove(0, 1) + "- Paint";
                fileLocation = true;
                mouseDown = false;
            }
            else if (dialogResult == DialogResult.No)
            {
                openFileDialog1.FileName = "";
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                filename = openFileDialog1.FileName;
                openFile = true;
                panel1.Invalidate();
                this.Text = filename;//.Remove(0, filename.LastIndexOf(@"\")).Remove(0, 1) + "- Paint";
                fileLocation = true;
                mouseDown = false;
            }
            else if (dialogResult == DialogResult.Cancel)
            {

            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*mouseDown = false;
            if (!fileLocation)
            {
                saveFileDialog1.FileName = "Безымянный";
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                // получаем выбранный файл
                filename = saveFileDialog1.FileName;
            }
            else
            {
                //filename = filename.Remove(filename.Length-5,4);
            }
            // сохраняем текст в файл
            Bitmap bmp = new Bitmap(panel1.Width, panel1.Height);
            panel1.DrawToBitmap(bmp, panel1.ClientRectangle);
            bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
            this.Text = filename + "- Paint";
            //this.Text = filename.Remove(0, filename.LastIndexOf(@"\")).Remove(0, 1) + "- Paint";
            fileLocation = true;*/

            mouseDown = false;
            if (!fileLocation)
            {
                saveFileDialog1.FileName = "Безымянный";
            }
            else
            {
                saveFileDialog1.FileName = filename.Remove(0, filename.LastIndexOf(@"\")).Remove(0, 1);
            }
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            filename = saveFileDialog1.FileName;
            // сохраняем текст в файл
            Bitmap bmp = new Bitmap(panel1.Width, panel1.Height);
            panel1.DrawToBitmap(bmp, panel1.ClientRectangle);
            bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
            this.Text = filename + "- Paint";
            this.Text = filename.Remove(0, filename.LastIndexOf(@"\")).Remove(0, 1) + " -Paint";
            fileLocation = true;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mouseDown = false;
            if (!fileLocation)
            {
                saveFileDialog1.FileName = "Безымянный";
            }
            else
            {
                saveFileDialog1.FileName = filename.Remove(0, filename.LastIndexOf(@"\")).Remove(0, 1);
            }
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            filename = saveFileDialog1.FileName;
            // сохраняем текст в файл
            Bitmap bmp = new Bitmap(panel1.Width, panel1.Height);
            panel1.DrawToBitmap(bmp, panel1.ClientRectangle);
            bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
            this.Text = filename + "- Paint";
            this.Text = filename.Remove(0, filename.LastIndexOf(@"\")).Remove(0, 1) + " -Paint";
            fileLocation = true;
        }

        private void selectShapeComboBox_Click(object sender, EventArgs e)
        {
            pipetteButton.Checked = false;
            pipetteBool = false;
        }

        private void numericUpDown1_Click(object sender, EventArgs e)
        {
            pipetteButton.Checked = false;
            pipetteBool = false;
        }
    }
}
