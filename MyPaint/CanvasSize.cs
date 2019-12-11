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
    public partial class CanvasSize : Form
    {
        public int CanvasWidth;
        public int CanvasHeight;
        public CanvasSize()
        {
            InitializeComponent();
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            CanvasWidth = int.Parse(widthTextBox.Text);
            CanvasHeight = int.Parse(heightTextBox.Text);
        }
    }
}
