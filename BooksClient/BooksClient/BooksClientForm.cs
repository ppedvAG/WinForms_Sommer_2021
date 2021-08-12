using System;
using System.Collections.Generic;
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
            var url = "https://www.googleapis.com/books/v1/volumes?q=" + suchTextBox.Text;

            var http = new HttpClient();
            string json = await http.GetStringAsync(url);

            jsonTextBox.Text = json;

            BooksResult br = Newtonsoft.Json.JsonConvert.DeserializeObject<BooksResult>(json);

            dataGridView1.DataSource = br.items.Select(x => x.volumeInfo).OrderBy(x => x.pageCount).ToList();

        }
    }
}
