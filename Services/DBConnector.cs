using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using EFLigaDB;
using System.Windows;
using System.ComponentModel;

namespace WpfApplicationLiga.Services
{  
    class DBConnector
    {
        private LigaDB_ORM _dbLiga;

        public ObservableCollection<uspGetSaisonTabelle_Result> GetPunktestand(int idSaison)
        {
            var punktestand = new ObservableCollection<uspGetSaisonTabelle_Result>(_dbLiga.uspGetSaisonTabelle(idSaison));
            return punktestand;
        }

        public ObservableCollection<qry_LigaSaison> GetSaisons()
        {
            try
            {

                var saisons = new ObservableCollection<qry_LigaSaison>(_dbLiga.qry_LigaSaison);
                return saisons;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.ToString());
                return null;
            }
        }

        public ObservableCollection<qry_Begegnungen_Zusammenfassung_PunkteSaetze> GetBegegnungenPunkteSaetze(int idSaison)
        {
            var begegnungenPunkteSaetze = new ObservableCollection<qry_Begegnungen_Zusammenfassung_PunkteSaetze>(from p in _dbLiga.qry_Begegnungen_Zusammenfassung_PunkteSaetze where p.idSaison==idSaison select p);
            return begegnungenPunkteSaetze;
        }

        public ObservableCollection<tblBegegnungen> GetBegegnungenEdit(int idSaison)
        {
            var begegnungenEdit = new ObservableCollection<tblBegegnungen>(from p in _dbLiga.tblBegegnungen where p.idSaison==idSaison select p);
            return begegnungenEdit;
        }


        public ObservableCollection<tblMannschaften> GetMannschaften()
        {
            var mannschaften = new ObservableCollection<tblMannschaften>(_dbLiga.tblMannschaften);
            return mannschaften;
        }

        public ObservableCollection<tblSpielbaelle> GetSpielbaelle()
        {
            var spielbaelle = new ObservableCollection<tblSpielbaelle>(_dbLiga.tblSpielbaelle);
            return spielbaelle;
        }

        public ObservableCollection<qry_Begegnungen_Punkteberechnung> GetBegegnungenSaetze(int idSaison)
        {
            var BegegnungenSaetze =new ObservableCollection<qry_Begegnungen_Punkteberechnung>(from p in _dbLiga.qry_Begegnungen_Punkteberechnung where p.idSaison==idSaison select p);
            return BegegnungenSaetze;
        }

        //public void SaveAsHtml()
        //{
        //    var tabelleDetail =  new ObservableCollection<qry_Begegnungen_Zusammenfassung_PunkteSaetze>
        //        (from p in _dbLiga.qry_Begegnungen_Zusammenfassung_PunkteSaetze orderby p.Begegnungsdatum descending select p);
        //    //var tabelleDetail2 = (from p in tabelleDetail orderby p.Spielnummer select p) as ObservableCollection<qry_Begegnungen_Zusammenfassung_PunkteSaetze>;
            
        //    XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<qry_Begegnungen_Zusammenfassung_PunkteSaetze>));
        //    using (StreamWriter wr = new StreamWriter(@"D:\Dev\XML2HTML\OC_TabelleDetails.xml"))
        //    {
        //        xs.Serialize(wr, tabelleDetail);
        //    }

        //    var punktestand = new ObservableCollection<uspGetSaisonTabelle_Result>(_dbLiga.uspGetSaisonTabelle(1));
        //    xs = new XmlSerializer(typeof(ObservableCollection<uspGetSaisonTabelle_Result>));
        //    using (var wr = new StreamWriter(@"D:\Dev\XML2HTML\OC_Tabelle.xml"))
        //    {
        //        xs.Serialize(wr, punktestand);
        //    }
        //}

        public void RefreshData()
        {
            //_dbLiga.Refresh(RefreshMode.ClientWins,_dbLiga.uspGetSaisonTabelle(2));
            //_dbLiga.Refresh(RefreshMode.ClientWins, _dbLiga.qry_Begegnungen_Zusammenfassung_PunkteSaetze);
            _dbLiga = new LigaDB_ORM();

        }


        public int SaveChanges()
        {

            try
            {
                _dbLiga.SaveChanges();
                return 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.InnerException.ToString());
                return 0;
            }

        }

        public DBConnector()
        {
            _dbLiga = new LigaDB_ORM();
            
        }

    }
    public class Spiele: Helpers.MVVM.NotificationObject
    {
        private string _spielnummer;
        public Spiele(string spielnummer)
        {
            this._spielnummer = spielnummer;
        }
        public string Spielnummer { get { return _spielnummer; }
            set { _spielnummer = value; OnPropertyChanged("Spielnummer"); }
        }
    }
}
