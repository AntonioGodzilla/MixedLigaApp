using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using EFLigaDB;
using WpfApplicationLiga.Helpers.Common;
using WpfApplicationLiga.Helpers.MVVM;
using WpfApplicationLiga.Services;
using WpfApplicationLiga.Views.Controls;
using UserControl = System.Windows.Controls.UserControl;
using System.Windows;

namespace WpfApplicationLiga.ViewModels
{
    class BegegnungenEditierenViewModel: BaseModel
    {
        private DBConnector _dbConnector;
        
        private CollectionViewSource _source;
        
        private ICollectionView _begegungenEditView;
        private String _begegnungenSaetze;
        
        private readonly DelegateCommand _commandSaveChanges;
        private readonly DelegateCommand _commandRefreshSaetzeEndstand;

        //private int maxSpielNr;

        #region Properties
        public BegegnungenEditierenView View { get; set; }
        public ObservableCollection<tblMannschaften> MannschaftenViewSource { get; set; }
        public ICollectionView BegegnungenEditView
        {
            get { return _begegungenEditView; }
            set
            {
                _begegungenEditView = value; OnPropertyChanged("BegegnungenEditView");
            }
        }
        public String BegegnungenSaetze
        {
            get { return _begegnungenSaetze; }
            set
            {
                _begegnungenSaetze = value;
                OnPropertyChanged("BegegnungenSaetze");
            }
        }
        public DelegateCommand CommandSaveChanges
        {
            get { return _commandSaveChanges; }
        }
        public DelegateCommand CommandRefreshSaetzeEndstand
        {
            get { return _commandRefreshSaetzeEndstand; }
        }
        //public int MaxSpielNr
        //{
        //    get
        //    {
        //        //var begegnungenEdit = _dbConnector.GetBegegnungenEdit();
        //        //maxSpielNr=(int) begegnungenEdit.Max(p => p.Spielnummer);
        //        return maxSpielNr;
        //    }
 
        //}
        public int CurrentIdSaison
        {
            get;
            set;
        }
        #endregion

        public void SaveChanges()
        {
            _dbConnector.SaveChanges();

            RefreshSaetzeEndstand();
        }

        public void RefreshSaetzeEndstand()
        {
            var begegnungenSaetze = _dbConnector.GetBegegnungenSaetze(CurrentIdSaison);
            var inhalt = begegnungenSaetze.ToLookup(p => p.idBegegnung);
            var idCurrent = BegegnungenEditView.CurrentItem as EFLigaDB.tblBegegnungen;

            foreach (var item in inhalt[idCurrent.id])
            {
                BegegnungenSaetze = item.SatzpunkteHeimEndstand.ToString() + ":" + item.SatzpunkteGastEndstand.ToString();
            }
            
            //if (View.txbSpielnummer.Text=="") View.txbSpielnummer.Text = MaxSpielNr.ToString();
        }


        public void Initialize()
        {
            if (View == null)
            {
                View = new BegegnungenEditierenView();
                
            }
            View.DataContext = this;

            
            _source = new CollectionViewSource();
            var begegnungenEdit = _dbConnector.GetBegegnungenEdit(CurrentIdSaison);
            //Durch LinQ ist es dann leider nicht editierbar:(
            //_source.Source = (from p in begegnungenEdit where p.idSaison==3 select p);
            _source.Source = begegnungenEdit;
            
            this.BegegnungenEditView = _source.View;
            
            MannschaftenViewSource =  _dbConnector.GetMannschaften();

            BegegnungenSaetze = "0:0";

            Functions.SortDataGrid(this.View.DtgBegegnungenEdit, 2, ListSortDirection.Descending);
            this.View.DtgBegegnungenEdit.SelectedIndex = 0;


        }



        public UserControl GetView()
        {
            return this.View;
        }

        public BegegnungenEditierenViewModel()
        {
            _dbConnector = new DBConnector();
            _commandSaveChanges = new DelegateCommand(SaveChanges);
            _commandRefreshSaetzeEndstand = new DelegateCommand(RefreshSaetzeEndstand);

            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE");
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
        }

    }
}
