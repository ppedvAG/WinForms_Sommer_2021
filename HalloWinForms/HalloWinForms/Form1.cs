using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalloWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           

            label1.Text = Properties.Resources.Titel;
            BackColor = Properties.Settings.Default.BackColor;
            Left = Properties.Settings.Default.WinPos.X;
            Top = Properties.Settings.Default.WinPos.Y;

#if DEBUG
            Text = "DEBUG ist nun aktiv";
#else
            Text = "RELEASE ist nun aktiv";
#endif

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Test");
            //new Form1().Show();
            for (int i = 0; i < 12; i++)
            {
                listBox1.Items.Add($"Ding {i:00}");
                comboBox1.Items.Add($"Ding {i:00}");

                var tn = new TreeNode($"Node {i:00}");
                treeView1.Nodes.Add(tn);
                for (int j = 0; j < 3; j++)
                {
                    tn.Nodes.Add(new TreeNode($"AAaa {j}"));

         
                }
            }
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("Mouse Klick");
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.Left += 1;
            button1.Top+= 1;
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            progressBar1.Value = trackBar1.Value; 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Left++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                BackColor = colorDialog1.Color;             
                Properties.Settings.Default.BackColor = colorDialog1.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.WinPos = new Point(Left, Top);
            Properties.Settings.Default.Save();
        }
    }
}
