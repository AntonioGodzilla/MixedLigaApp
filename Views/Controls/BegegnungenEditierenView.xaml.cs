using System.Globalization;
using System.Windows.Controls;
using System.Configuration;

namespace WpfApplicationLiga.Views.Controls
{
    /// <summary>
    /// Interaction logic for BegegnungenEditierenView.xaml
    /// </summary>
    public partial class BegegnungenEditierenView : UserControl
    {
        public BegegnungenEditierenView()
        {
            //Properties.Resources.Culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"]);
            InitializeComponent();
        }
    }
}
