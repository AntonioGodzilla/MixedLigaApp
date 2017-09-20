using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using EFLigaDB;
using WpfApplicationLiga.Helpers.Common;
using WpfApplicationLiga.Helpers.MVVM;
using WpfApplicationLiga.Services;
using WpfApplicationLiga.Views.Controls;
using UserControl = System.Windows.Controls.UserControl;

namespace WpfApplicationLiga.ViewModels
{
    class PunktestandViewModel:BaseModel
    {
        private readonly DBConnector _dbConnector;
        private CollectionViewSource _source;
        private ICollectionView _punktestandView;
        //private int _currentidSaison;
       
        public PunktestandView PunktestandViewControl { get; set; }

        #region Properties

        public ICollectionView PunktestandView
        {
            get { return _punktestandView; }
            set
            {
                _punktestandView = value;
                OnPropertyChanged("PunktestandView");
            }
        }

        public int CurrentidSaison { get; set; }

        #endregion

        public void Initialize()
        {
            //if (PunktestandViewControl == null)
            //{
                //Control initialisieren
                PunktestandViewControl = null;
                PunktestandViewControl = new PunktestandView();
            //}
            this.PunktestandViewControl.DataContext = this;

            LoadData();
            
        }
        
        public void LoadData()
        {
            _dbConnector.RefreshData();
            _source = new CollectionViewSource();


            var punktestand = _dbConnector.GetPunktestand(CurrentidSaison);
            _source.Source = punktestand;
            this.PunktestandView = _source.View;


        }
        public UserControl GetView()
        {
            return this.PunktestandViewControl;
        }
        public PunktestandViewModel()
        {
            _dbConnector = new DBConnector();
        }
    }
}
