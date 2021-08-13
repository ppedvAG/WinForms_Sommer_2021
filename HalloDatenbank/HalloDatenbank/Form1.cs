using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HalloDatenbank
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            conString = "Server=(localdb)\\mssqllocaldb;Database=Northwind;Trusted_Connection=true;";
        }

        string conString = "Server=.;Database=Northwind;Trusted_Connection=true;";

        private void button1_Click(object sender, EventArgs e)
        {

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Employees"))
                {
                    cmd.Connection = con;

                    object result = cmd.ExecuteScalar();
                    if (result is int count)
                        Text = $"{count} Employees in DB";
                }

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employees"))
                {
                    cmd.Connection = con;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) //zeileweise aus der DB die Daten holen
                    {
                        Employee emp = new Employee()
                        {
                            Id = reader.GetInt32(0),
                            Nachname = reader.GetString(1),
                            Vorname = reader.GetString(2),
                            GebDatum = reader.GetDateTime(5)
                        };

                        //listBox1.Items.Add($"{emp.Vorname} {emp.Nachname} {emp.GebDatum:d}"); // selected Item == string
                        listBox1.Items.Add(emp); // selected Item == Employee
                    }
                }

            }  //con.Dispose(); --> con.Close();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Employee emp)
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    //using (SqlCommand cmd = new SqlCommand($"UPDATE Employees SET BirthDate={emp.GebDatum} WHERE EmployeeID={emp.Id}")) //bäh!
                    //sql injection ';DELETE users;--
                    using (SqlCommand cmd = new SqlCommand("UPDATE Employees SET BirthDate=@newBDate WHERE EmployeeID=@id"))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@newBDate", emp.GebDatum.AddYears(1));
                        cmd.Parameters.AddWithValue("@id", emp.Id);

                        int affectedRows = cmd.ExecuteNonQuery();
                        if (affectedRows == 0)
                            MessageBox.Show("Nicht wurde veränder");
                        else if (affectedRows == 1)
                            MessageBox.Show($"{emp.Vorname} wurde verjüngt");
                        else
                            MessageBox.Show("PANIK!");
                    }
                }
            }
        }

        private void listBox1_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is Employee emp)
                e.Value = $"{emp.Vorname}  {emp.Nachname} {emp.GebDatum:d}";
        }
    }

    class Employee
    {
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public DateTime GebDatum { get; set; }
    }
}
