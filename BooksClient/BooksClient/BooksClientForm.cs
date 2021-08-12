using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace BooksClient
{
    public partial class BooksClientForm : Form
    {
        public BooksClientForm()
        {
            InitializeComponent();
        }

        private void showForm1Button_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.Manual;
            form1.Left = Left + ((ClientSize.Width - form1.Width) / 2);
            form1.Top = Top + ((ClientSize.Height - form1.Height) / 2);
            form1.Show();

            //if (form1.ShowDialog() == DialogResult.Ignore)
            //{ //...}
            //}
        }

        private async void searchBookButton_Click(object sender, EventArgs e)
        {
            searchBookButton.Enabled = false;
            try
            {
                //var url = "https://www.googqwdwqleapis.com/books/v1/volumes?q=" + suchTextBox.Text;
                var url = "https://www.googleapis.com/books/v1/volumes?q=" + suchTextBox.Text;

                var http = new HttpClient();
                http.Timeout = new TimeSpan(0, 0, 3);
                string json = await http.GetStringAsync(url);

                jsonTextBox.Text = json;

                BooksResult br = JsonConvert.DeserializeObject<BooksResult>(json);

                dataGridView1.DataSource = br.items.Select(x => x.volumeInfo).OrderBy(x => x.pageCount).ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Das hat leider nicht geklappt 😥😢\n" + ex.Message);
            }
            searchBookButton.Enabled = !false;
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //using (var sw = new StreamWriter(saveFileDialog1.FileName + "kwefnhfewknf\\ewihewfkjn\\"))
                    using (var sw = new StreamWriter(saveFileDialog1.FileName))
                    {
                        var json = JsonConvert.SerializeObject(dataGridView1.DataSource, Formatting.Indented);
                        sw.WriteLine(json);
                    }// sw.Dispose(); //-> sw.Close()
                }
                catch (DirectoryNotFoundException ex)
                {
                    MessageBox.Show("Das Verzeichnis tut gar da sein!!\n" + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Json Book Files|*.json;*.bson|Alle Dateien|*.*",
                    Title = "Json Book File",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var sr = new StreamReader(openFileDialog.FileName))
                    {
                        var volInfos = JsonConvert.DeserializeObject<IEnumerable<Volumeinfo>>(sr.ReadToEnd());

                        dataGridView1.DataSource = volInfos;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Schade: " + ex.Message);
            }
        }


        private void suchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                searchBookButton_Click(this, null);
            }
        }


    }
}
