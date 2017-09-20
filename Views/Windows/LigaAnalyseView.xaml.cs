using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Configuration;

namespace WpfApplicationLiga.Views.Windows
{
    /// <summary>
    /// Interaction logic for LigaAnalyseView.xaml
    /// </summary>
    public partial class LigaAnalyseView : Window
    {
        public LigaAnalyseView()
        {
            //Properties.Resources.Culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"]);
            InitializeComponent();
        }

        private void TabControlTabelle_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            MessageBox.Show("Event");
        }

    }
}
