using System.Configuration;
using System.Globalization;
using System.Windows;
using WpfApplicationLiga.ViewModels;

namespace WpfApplicationLiga
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Properties.Resources.Culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"]);
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE");
            //System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");

            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //for (int i = 0; i < 10000000; i++)
            //{
            //    txtVersion.Text = i.ToString();

            //}
            

            LigaAnalyseViewModel myStartWindow = new LigaAnalyseViewModel();
            //WindowControlTesterViewModel myStartTest = new WindowControlTesterViewModel();
            //myStartTest.Initialize();

            myStartWindow.Initialize();
            this.Visibility = System.Windows.Visibility.Hidden;
            this.Close();
        }
    }
}
