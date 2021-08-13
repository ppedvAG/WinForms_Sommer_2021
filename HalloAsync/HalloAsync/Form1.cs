using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalloAsync
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                progressBar1.Value = i;
                Thread.Sleep(100);
                Application.DoEvents(); //häßlicher pfusch!
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //hier bin ich im hintergrund Thread

            for (int i = 0; i <= 100; i++)
            {
                //progressBar1.Value = i; // nicht geht
                backgroundWorker1.ReportProgress(i, $"Bin bei {i}%");
                Thread.Sleep(100);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //hier bin ich im UI Thread
            progressBar1.Value = e.ProgressPercentage;
            label1.Text = e.UserState.ToString();

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //hier bin ich im UI Thread

            MessageBox.Show("Fertig");
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;

            try
            {
                var con = new SqlConnection("Server=kwejnwegnj;Database=wekgjnh;Trusted_Connection=true");
                await con.OpenAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            progressBar1.Style = ProgressBarStyle.Continuous;
            
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                progressBar1.Value = i;

                await Task.Delay(100);
              
            }
        }
    }
}
