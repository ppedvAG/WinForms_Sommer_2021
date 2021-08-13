using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooksClient
{
    public partial class HalloBinding : UserControl
    {
        public HalloBinding()
        {
            InitializeComponent();

            textBox2.DataBindings.Add("Text", textBox1, "Text", true, DataSourceUpdateMode.OnPropertyChanged);
            textBox2.DataBindings.Add("BackColor", textBox1, "Text", true, DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
