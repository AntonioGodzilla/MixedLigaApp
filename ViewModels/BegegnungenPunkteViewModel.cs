using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using WpfApplicationLiga.Helpers.Common;
using WpfApplicationLiga.Helpers.MVVM;
using WpfApplicationLiga.Services;
using WpfApplicationLiga.Views.Controls;

namespace WpfApplicationLiga.ViewModels
{
    class BegegnungenPunkteViewModel:BaseModel
    {
        private readonly DBConnector _dbConnector;
        private CollectionViewSource _source;
        private ICollectionView _punkteSaetzeView;

        public SpielePunkteSaetzeView SpielePunkteSaetzeViewControl { get; set; }

        #region Properties
        public int CurrentIdSaison
        {
            get;
            set;
        }

        public ICollectionView PunkteSaetzeView
        {
            get { return _punkteSaetzeView; }
            set
            {
                _punkteSaetzeView = value;
                OnPropertyChanged("PunktesSaetzeView");
                
            }
        }
        #endregion
        
        public void Initialize()
        {
            //if (SpielePunkteSaetzeViewControl == null)
            //{
                //Control initialisieren
                SpielePunkteSaetzeViewControl = null;
                SpielePunkteSaetzeViewControl = new SpielePunkteSaetzeView();
            //}
            this.SpielePunkteSaetzeViewControl.DataContext = this;

            LoadData();

            Functions.SortDataGrid(this.SpielePunkteSaetzeViewControl.dtgSpielePunkteSaetze, 1, ListSortDirection.Ascending);
            this.SpielePunkteSaetzeViewControl.dtgSpielePunkteSaetze.SelectedIndex = 0;

           
        }

        public void LoadData()
        {
            _source = new CollectionViewSource();

            var punkteSaetze = _dbConnector.GetBegegnungenPunkteSaetze(CurrentIdSaison);

            //LinQ-Statement
            _source.Source = punkteSaetze;
            //_source.Source=(from p in punkteSaetze orderby p.spielnummer descending select p);
            this.PunkteSaetzeView = _source.View;
        }

        public UserControl GetView()
        {
            return this.SpielePunkteSaetzeViewControl;
        }
        public BegegnungenPunkteViewModel()
        {
            _dbConnector = new DBConnector();
        }
    }
}
