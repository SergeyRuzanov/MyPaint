using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class Canvas : Form
    {
        private int oldX, oldY;
        public bool SavedAs = false;
        public string path = "";
        private Bitmap bmp;
        private ImageFormat[] imageFormats = { ImageFormat.Bmp, ImageFormat.Jpeg };
        private int indexExt;

        public Canvas(string fileName)
        {
            InitializeComponent();
            bmp = new Bitmap(fileName);
            Graphics g = Graphics.FromImage(bmp);
            pictureBox1.Width = bmp.Width;
            pictureBox1.Height = bmp.Height;
            pictureBox1.Image = bmp;
        }
        public void SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.Filter = "Windows Bitmap (*.bmp)|*.bmp|Файлы JPEG (*.jpg)|*.jpg";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                bmp.Save(saveFileDialog.FileName, imageFormats[saveFileDialog.FilterIndex - 1]);
                path = saveFileDialog.FileName;
                indexExt = saveFileDialog.FilterIndex - 1;
            }
            SavedAs = true;
        }
        public void Save()
        {
            bmp.Save(path, imageFormats[indexExt]);
        }
        public int CanvasWidth
        {
            get
            {
                return pictureBox1.Width;
            }
            set
            {
                pictureBox1.Width = value;
                Bitmap tbmp = new Bitmap(value, pictureBox1.Height);
                Graphics graphics = Graphics.FromImage(tbmp);
                graphics.Clear(Color.White);
                graphics.DrawImage(bmp, new Point(0, 0));
                ClientSize = new Size(value, pictureBox1.Height);
                bmp = tbmp;
                pictureBox1.Image = bmp;
            }
        }

        public int CanvasHeight
        {
            get
            {
                return pictureBox1.Height;
            }
            set
            {
                pictureBox1.Height = value;
                Bitmap tbmp = new Bitmap(pictureBox1.Width, value);
                Graphics graphics = Graphics.FromImage(tbmp);
                graphics.Clear(Color.White);
                graphics.DrawImage(bmp, new Point(0, 0));
                this.ClientSize = new Size(pictureBox1.Width, value);
                bmp = tbmp;
                pictureBox1.Image = bmp;
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!(((MainForm)MdiParent).toolStripButtonEllipce.Checked || ((MainForm)MdiParent).toolStripButtonLine.Checked || ((MainForm)MdiParent).toolStripButtonStar.Checked))
            {
                if (e.Button == MouseButtons.Left)
                {
                    Pen pencil;
                    if (!((MainForm)MdiParent).toolStripButtonLastic.Checked)
                        pencil = new Pen(MainForm.CurColor, MainForm.CurWidth);
                    else
                        pencil = new Pen(Color.White, MainForm.CurWidth);
                    pencil.DashCap = System.Drawing.Drawing2D.DashCap.Round;
                    pencil.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    pencil.EndCap = System.Drawing.Drawing2D.LineCap.Round;

                    Graphics graphics = Graphics.FromImage(bmp);
                    graphics.DrawLine(pencil, oldX, oldY, e.X, e.Y);
                    oldX = e.X;
                    oldY = e.Y;
                    pictureBox1.Invalidate();
                }
            }
        }
        private int oX, oY;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!(((MainForm)MdiParent).toolStripButtonEllipce.Checked || ((MainForm)MdiParent).toolStripButtonLine.Checked || ((MainForm)MdiParent).toolStripButtonStar.Checked))
            {
                oldX = e.X;
                oldY = e.Y;
            }
            else if (((MainForm)MdiParent).toolStripButtonLine.Checked)
            {
                oX = e.X;
                oY = e.Y;
            }
            else if (((MainForm)MdiParent).toolStripButtonEllipce.Checked)
            {
                oX = e.X;
                oY = e.Y;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

            if (((MainForm)MdiParent).toolStripButtonLine.Checked)
            {
                Pen pencil = new Pen(MainForm.CurColor, MainForm.CurWidth);
                pencil.DashCap = System.Drawing.Drawing2D.DashCap.Round;
                pencil.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                pencil.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                Graphics graphics = Graphics.FromImage(bmp);
                graphics.DrawLine(pencil, oX, oY, e.X, e.Y);
                pictureBox1.Invalidate();
            }
            else if (((MainForm)MdiParent).toolStripButtonEllipce.Checked)
            {
                Pen pencil = new Pen(MainForm.CurColor, MainForm.CurWidth);
                pencil.DashCap = System.Drawing.Drawing2D.DashCap.Round;
                pencil.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                pencil.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                Graphics graphics = Graphics.FromImage(bmp);
                graphics.DrawEllipse(pencil, new Rectangle(oX, oY, Math.Abs(oX - e.X), Math.Abs(oY - e.Y)));
                pictureBox1.Invalidate();
            }
        }

        private void Canvas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SavedAs == false)
            {
                var massage = MessageBox.Show("Файл не сохранен! Сохранить файл?", "Сохраниение перед выходом", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (massage == DialogResult.Yes)
                {
                    if (SavedAs)
                        Save();
                    else
                        SaveAs();
                }
            }
        }

        public Canvas()
        {
            InitializeComponent();
            bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
            CanvasWidth = 300;
            CanvasHeight = 200;
        }
    }
}
