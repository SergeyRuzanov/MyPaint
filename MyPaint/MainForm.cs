using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class MainForm : Form
    {
        public static Color CurColor = Color.Black;
        public static int CurWidth = 3;
        public MainForm()
        {
            InitializeComponent();
            brushToolStripTextBox.Text = CurWidth.ToString();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutPaint aboutPaint = new AboutPaint();
            aboutPaint.ShowDialog();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Canvas formChild = new Canvas();
            formChild.MdiParent = this;
            formChild.Show();
        }

        private void pictureToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            sizeToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
        }

        private void sizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasSize cs = new CanvasSize();
            cs.CanvasWidth = ((Canvas)ActiveMdiChild).CanvasWidth;
            cs.CanvasHeight = ((Canvas)ActiveMdiChild).CanvasHeight;
            if (cs.ShowDialog() == DialogResult.OK)
            {
                ((Canvas)ActiveMdiChild).CanvasWidth = cs.CanvasWidth;
                ((Canvas)ActiveMdiChild).CanvasHeight = cs.CanvasHeight;
            }
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurColor = Color.Red;
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurColor = Color.Blue;
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurColor = Color.Green;
        }

        private void otherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                CurColor = cd.Color;
            }
        }

        private void brushToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            if (brushToolStripTextBox.Text.Trim() == "")
                return;
            bool errorFlag = int.TryParse(brushToolStripTextBox.Text, out int curWidth);
            if (!errorFlag || curWidth < 1)
            {
                MessageBox.Show("Размер кисти должно быть целым положительным числом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                brushToolStripTextBox.Text = CurWidth.ToString().Trim();
                return;
            }
            CurWidth = curWidth;
            brushToolStripTextBox.Text = CurWidth.ToString().Trim();
        }
        public void ActivateSave()
        {
            saveAsToolStripMenuItem.Enabled = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Windows Bitmap (*.bmp)|*.bmp|Файлы JPEG (*.jpeg, *.jpg)|*.jpeg;*.jpg|Все файлы ()*.*|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Canvas frmChild = new Canvas(openFileDialog.FileName);
                frmChild.MdiParent = this;
                frmChild.Show();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((Canvas)ActiveMdiChild).SaveAs();
        }

        private void cascadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void leftToRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void upToDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void sortIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void failToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (ActiveMdiChild == null)
            {
                saveToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
            }
            else
            {
                saveToolStripMenuItem.Enabled = ((Canvas)ActiveMdiChild).SavedAs;
                saveAsToolStripMenuItem.Enabled = true;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((Canvas)ActiveMdiChild).Save();
        }

        private void toolStripButtonLine_CheckedChanged(object sender, EventArgs e)
        {
            if (toolStripButtonLine.Checked)
            {
                toolStripButtonEllipce.Checked = false;
                toolStripButtonStar.Checked = false;
            }
        }

        private void toolStripButtonEllipce_CheckedChanged(object sender, EventArgs e)
        {
            if (toolStripButtonEllipce.Checked)
            {
                toolStripButtonLine.Checked = false;
                toolStripButtonStar.Checked = false;
            }
        }

        private void toolStripButtonStar_CheckedChanged(object sender, EventArgs e)
        {
            if (toolStripButtonStar.Checked)
            {
                toolStripButtonLine.Checked = false;
                toolStripButtonEllipce.Checked = false;
            }
        }
    }
}
