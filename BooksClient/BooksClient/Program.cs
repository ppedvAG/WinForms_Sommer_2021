using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

namespace BooksClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Debug.WriteLine("Hallo");

            int zahl = 5;
            decimal geld = 932847.90832474325987m;
            DateTime heute = DateTime.Now;

            Debug.WriteLine(zahl.ToString());
            Debug.WriteLine(zahl.ToString("0000"));
            Debug.WriteLine(zahl.ToString("00.00"));
            Debug.WriteLine("");
            Debug.WriteLine(geld.ToString());
            Debug.WriteLine(geld.ToString("00.00"));
            Debug.WriteLine(geld.ToString("c"));
            Debug.WriteLine(geld.ToString("c", new CultureInfo("en-GB")));
            Debug.WriteLine("");
            Debug.WriteLine(heute.ToString());
            Debug.WriteLine("Heute ist der " + heute.ToString("d") + " und es sind " + zahl.ToString("00") + "°C");
            Debug.WriteLine(string.Format("Heute ist der {0:d} und es sind {1:00}°C", heute, zahl));
            Debug.WriteLine($"Heute ist der {heute:d} und es sind {zahl:00}°C"); //string interpolation
            Debug.WriteLine("");

            Volumeinfo volumeinfo = new Volumeinfo() { title = "Ein tolles Buch" };
            Debug.WriteLine(volumeinfo.ToString());



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BooksClientForm());
        }
    }
}
