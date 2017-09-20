using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplicationLiga.Models
{
    class qry_Begegnungen_Punkte
    {
        public int idBegegnung { get; set; }
        public int idMannschaftHeim { get; set; }
        public int idMannschaftGast { get; set; }
        public short Satz { get; set; }
    }
}
