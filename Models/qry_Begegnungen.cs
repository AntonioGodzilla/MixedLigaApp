using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplicationLiga.Models
{
    class qry_Begegnungen
    {
        public int  id_Begegnung { get; set; }
        public double Spielnummer { get; set; }
        public string Heim_Mannschaft { get; set; }
        public string Gast_Mannschaft { get; set; }
        public DateTime Begegnungsdatum { get; set; }
        public string Heim_Kurz { get; set; }
        public string Gast_Kurz { get; set; }
        public string Bemerkungen { get; set; }


    }
}
