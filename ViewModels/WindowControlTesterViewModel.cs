using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using WpfApplicationLiga.Helpers.MVVM;
using WpfApplicationLiga.Services;
using WpfApplicationLiga.Views.Windows;

namespace WpfApplicationLiga.ViewModels
{
    class WindowControlTesterViewModel: BaseModel
    {
        private readonly DBConnector _dbConnector;
        //private PunktestandViewModel _punktestandViewModel;
        //private BegegnungenPunkteViewModel _spielePunkteSaetzeViewModel;
        private BegegnungenEditierenViewModel _begegnungenEditierenViewModel;
        private CollectionViewSource _source;
        //private ICollectionView _saisonsView;

        public WindowControlTesterView  WindowControlTesterView { get; set; }



        public void Initialize()
        {
            _source = new CollectionViewSource();

            _begegnungenEditierenViewModel = new BegegnungenEditierenViewModel();
            _begegnungenEditierenViewModel.Initialize();
            var viewControlEdit = _begegnungenEditierenViewModel.GetView();


            // Binden des DataContext an die Window-Property dieser Klasse
            if (WindowControlTesterView == null)
            {
                WindowControlTesterView = new WindowControlTesterView();

                WindowControlTesterView.GridWindows.Children.Add(viewControlEdit);
                Grid.SetColumn(viewControlEdit, 0);
                Grid.SetRow(viewControlEdit, 1);


            }

            this.WindowControlTesterView.DataContext = this;
            this.WindowControlTesterView.Show();
        }
        public WindowControlTesterViewModel()
        {
            _dbConnector = new DBConnector();
            Initialize();
        }
    }
}
