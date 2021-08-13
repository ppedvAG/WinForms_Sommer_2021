using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace BooksClient
{
    public partial class BooksClientForm : Form
    {
        BindingSource source = new BindingSource();

        public BooksClientForm()
        {
            InitializeComponent();

            //ratingValueLabel.DataBindings.Add("Text", ratingTrackBar, "Value");
            //ratingValueLabel.DataBindings.Add(nameof(Label.Text), ratingTrackBar, nameof(TrackBar.Value));
            //ratingValueLabel.DataBindings.Add(nameof(ratingValueLabel.Text), ratingTrackBar, nameof(ratingTrackBar.Value));


            //ratingValueLabel.DataBindings.Add( //ziel Control
            //    nameof(ratingValueLabel.Text), //ziel Property
            //    ratingTrackBar,                //quell Control
            //    nameof(ratingTrackBar.Value),  //quell Property
            //    true,                          //Formatierung aktiv
            //    DataSourceUpdateMode.Never,    //nur lesen
            //    "?",                           //falls null zeige ? an
            //    "0⭐");                        //format string für simple typen (int, decimal, bool...etc)


            //format bei complexen typen (klassen)
            var binding = new Binding(nameof(ratingValueLabel.Text), ratingTrackBar, nameof(ratingTrackBar.Value));
            binding.Format += Binding_Format;
            ratingValueLabel.DataBindings.Add(binding);

            source.DataSource = new List<Volumeinfo>(); // muss initialisiert werden mit dem entsprechen Datentyp

            listBox1.DataSource = source;

            titleTextBox.DataBindings.Add(nameof(titleTextBox.Text), source, nameof(Volumeinfo.title), true, DataSourceUpdateMode.OnPropertyChanged);
            descTextBox.DataBindings.Add(nameof(descTextBox.Text), source, nameof(Volumeinfo.description));
            var rateBinding = new Binding(nameof(ratingTrackBar.Value), source, nameof(Volumeinfo.averageRating), true, DataSourceUpdateMode.OnPropertyChanged);
            rateBinding.Parse += RateBinding_FormatToString; //von der trackbar (value[int]) zur source (averageRating[string])
            rateBinding.Format += RateBinding_FormatToInt;//von der source (averageRating[string]) zur  trackbar (value[int]) 
            ratingTrackBar.DataBindings.Add(rateBinding);
            pageCountNumericUpDown.DataBindings.Add(nameof(pageCountNumericUpDown.Value), source, nameof(Volumeinfo.pageCount));

            //langComboBox.DataSource = new List<string>(new string[] { "", "de", "en", "fr", "sp" });
            langComboBox.DataSource = CultureInfo.GetCultures(CultureTypes.AllCultures).Select(x => x.TwoLetterISOLanguageName).Distinct().ToList();
            langComboBox.DataBindings.Add(nameof(langComboBox.SelectedItem), source, nameof(Volumeinfo.language));

            source.CurrentItemChanged += Source_CurrentItemChanged; 
        }

        private void Source_CurrentItemChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < authorsCheckedListBox.Items.Count; i++)
            {
                bool isAuthorOfSelectedBook = source.Current != null && source.Current is Volumeinfo vi && vi.authors.Contains(authorsCheckedListBox.Items[i]);
                authorsCheckedListBox.SetItemChecked(i, isAuthorOfSelectedBook);
            }
        }

        private void RateBinding_FormatToString(object sender, ConvertEventArgs e)
        {
            if (e.Value is int ratingAlsInt)
            {
                e.Value = ratingAlsInt.ToString();
            }
        }


        private void RateBinding_FormatToInt(object sender, ConvertEventArgs e)
        {
            if (e.Value is string ratingAsString)
            {
                if (string.IsNullOrWhiteSpace(ratingAsString))
                    e.Value = 0;

                if (double.TryParse(ratingAsString, NumberStyles.Any, new CultureInfo("en-US"), out double ratingAlsInt))
                {
                    e.Value = (int)Math.Round(ratingAlsInt, 0, MidpointRounding.AwayFromZero);
                }
            }
        }

        private void Binding_Format(object sender, ConvertEventArgs e)
        {
            if (e.Value is int i)
            {
                e.Value = $"{i} 🌟";
            }
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
                source.DataSource = dataGridView1.DataSource;

                authorsCheckedListBox.DataSource = br.items.Select(x => x.volumeInfo)
                                                           .Where(x => x.authors != null)
                                                           .SelectMany(x => x.authors)
                                                           .Distinct().ToList();

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
                        listBox1.DataSource = dataGridView1.DataSource;
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

        private void listBox1_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is Volumeinfo volinfo)
            {
                e.Value = $"{volinfo.title} [{volinfo.publishedDate}] ({volinfo.pageCount} pages)";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new Form();
            //form.Controls.Add(new Label() { Text = "Hallo", Left = 12, Top = 10 });
            //form.Controls.Add(new HalloBinding() { Text = "Hallo", Left = 12, Top = 40 });

            var hb = new HalloBinding() { Text = "Hallo", Dock = DockStyle.Fill };
            hb.textBox1.Text = "Hallo";
            form.Width = hb.Width;
            form.Height = hb.Height;
            form.Controls.Add(hb);
            form.ShowDialog();
        }
    }
}
