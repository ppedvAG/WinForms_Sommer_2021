using System;
using System.Linq;
using System.Windows.Forms;

namespace HalloDataSets
{
    public partial class EmployeesDataSetControl : UserControl
    {
        BindingSource source = new BindingSource();
        DataSet1 dataSet1 = new DataSet1();
        DataSet1TableAdapters.EmployeesTableAdapter adapter = new DataSet1TableAdapters.EmployeesTableAdapter();

        public EmployeesDataSetControl()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;

            source.DataSource = dataSet1.Employees;
            dataGridView1.DataSource = source;

            LoadAllEmployees();

        }

        private void loadToolStripButton1_Click(object sender, EventArgs e)
        {
            LoadAllEmployees();
        }

        private void LoadAllEmployees()
        {
            adapter.Fill(dataSet1.Employees);
        }

        private void saveToolStripButton1_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            adapter.Update(dataSet1.Employees);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //linq expression
            var query = from emp in dataSet1.Employees
                        where emp.BirthDate.Month > 6
                        orderby emp.FirstName
                        select emp;

            source.DataSource = query;

            //linq lambda
            source.DataSource = dataSet1.Employees.Where(emp => emp.BirthDate.Month > 6)
                                                  .OrderBy(x => x.FirstName); ;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var year = dataSet1.Employees.Min(x => x.BirthDate.Year);
            MessageBox.Show($"Min Year {year} ");
        }
    }
}
