using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomControls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int paintCount = 0;
        private void button1_Paint(object sender, PaintEventArgs e)
        {
            this.Text = $"{paintCount++}";
            e.Graphics.FillEllipse(new SolidBrush(Color.Red), new RectangleF(0, 0, 40, 40));
        }
    }
}
