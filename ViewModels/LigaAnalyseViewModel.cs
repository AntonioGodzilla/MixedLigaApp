using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using WpfApplicationLiga.Helpers.Common;
using WpfApplicationLiga.Helpers.MVVM;
using WpfApplicationLiga.Services;
using WpfApplicationLiga.Views.Controls;
using WpfApplicationLiga.Views.Windows;
using UserControl = System.Windows.Controls.UserControl;

namespace WpfApplicationLiga.ViewModels
{
    class LigaAnalyseViewModel : BaseModel
    {
        private DBConnector _dbConnector;
        private PunktestandViewModel _punktestandViewModel;
        private BegegnungenPunkteViewModel _spielePunkteSaetzeViewModel;
        private BegegnungenEditierenViewModel _begegnungenEditierenViewModel;
        private CollectionViewSource _source;
        private ICollectionView _saisonsView;
        private readonly DelegateCommand _commandLoadTable;
        private readonly DelegateCommand _commandSaveAsHtml;
        private readonly DelegateCommand _commandSaveData;
        private EFLigaDB.qry_LigaSaison _idCurrent;

        public LigaAnalyseView WindowsLigaAnalyseView { get; set; }

        public UserControl viewControlPs = new System.Windows.Controls.UserControl();
        public UserControl viewControlSps = new System.Windows.Controls.UserControl();
        public UserControl viewControlEdit = new System.Windows.Controls.UserControl();
        

        public ICollectionView SaisonsView
        {
            get { return _saisonsView; }
            set
            {
                _saisonsView = value;
                OnPropertyChanged("SaisonsView");
            }
        }
        
        #region Commands
        public DelegateCommand CommandSaveHtml { get { return _commandSaveAsHtml; } }

        public DelegateCommand CommandLoadTable { get { return _commandLoadTable; } }

        public DelegateCommand CommandSaveData { get { return _commandSaveData; } }
        #endregion

        public void LoadTable()
        {
            _dbConnector = new DBConnector();
            _dbConnector.RefreshData();

            var saisons = _dbConnector.GetSaisons();
            _source.Source = saisons;
            this.SaisonsView = _source.View;
            _idCurrent = SaisonsView.CurrentItem as EFLigaDB.qry_LigaSaison;
            
            

            _punktestandViewModel = new PunktestandViewModel { CurrentidSaison = _idCurrent.idSaison };
            _punktestandViewModel.Initialize();

            _spielePunkteSaetzeViewModel = new BegegnungenPunkteViewModel();
            _spielePunkteSaetzeViewModel.CurrentIdSaison = _idCurrent.idSaison;
            _spielePunkteSaetzeViewModel.Initialize();

            _begegnungenEditierenViewModel = new BegegnungenEditierenViewModel();
            _begegnungenEditierenViewModel.CurrentIdSaison = _idCurrent.idSaison;
            _begegnungenEditierenViewModel.Initialize();


            viewControlPs = _punktestandViewModel.GetView();
            viewControlSps = _spielePunkteSaetzeViewModel.GetView();
            viewControlEdit = _begegnungenEditierenViewModel.GetView();

            // Binden des DataContext an die Window-Property dieser Klasse
            if (WindowsLigaAnalyseView == null)
            {
                WindowsLigaAnalyseView = new LigaAnalyseView();

                WindowsLigaAnalyseView.GridShow.Children.Add(viewControlPs);
                Grid.SetColumn(viewControlPs, 0);
                Grid.SetColumnSpan(viewControlPs, 3);
                Grid.SetRow(viewControlPs, 1);

                WindowsLigaAnalyseView.GridShow.Children.Add(viewControlSps);
                Grid.SetColumn(viewControlSps, 0);
                Grid.SetColumnSpan(viewControlSps, 3);
                Grid.SetRow(viewControlSps, 3);

                WindowsLigaAnalyseView.GridEdit.Children.Add((viewControlEdit));
                Grid.SetColumn(viewControlEdit, 0);
                Grid.SetColumnSpan(viewControlEdit, 3);
                Grid.SetRow(viewControlEdit, 2);
            }

            this.WindowsLigaAnalyseView.DataContext = this;
            this.WindowsLigaAnalyseView.Show();
        }

        public void Initialize()
        {
            _source = new CollectionViewSource();
            LoadTable();

           


        }

        //Version direkt aus dem datagrid
        public void SaveAsHtml()
        {
            var viewControlPs = new PunktestandView();
            viewControlPs = (PunktestandView)_punktestandViewModel.GetView();
            var dt = new DataTable("Tabelle");
            dt = Functions.DataGridtoDataTable(viewControlPs.dtgPunktestand);
            dt.TableName = "Tabelle";
            dt.WriteXml("D:\\Dev\\XML2HTML\\Tabelle.xml");

            var viewControlPsd = new SpielePunkteSaetzeView();
            viewControlPsd = (SpielePunkteSaetzeView) _spielePunkteSaetzeViewModel.GetView();
            var dt2 = new DataTable("TabelleDetails");
            dt2 = Functions.DataGridtoDataTable(viewControlPsd.dtgSpielePunkteSaetze);
            dt2.TableName = "TabelleDetails";
            dt2.WriteXml("D:\\Dev\\XML2HTML\\TabelleDetails.xml");



        }

        public void SaveData()
        {
            _begegnungenEditierenViewModel.SaveChanges();
        }

        public LigaAnalyseViewModel()
        {
         
            _commandSaveAsHtml = new DelegateCommand(SaveAsHtml);
            _commandLoadTable = new DelegateCommand(LoadTable);
            _commandSaveData = new DelegateCommand(SaveData);
            
            //SaveAsHtml();

        }
    }
}
