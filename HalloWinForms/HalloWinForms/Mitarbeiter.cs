using System;

namespace HalloWinForms
{
    public class Mitarbeiter
    {
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }

        public string Name { get { return Vorname + "" + Nachname; } }
        public DateTime GebDatum { get; set; }

    }
}
