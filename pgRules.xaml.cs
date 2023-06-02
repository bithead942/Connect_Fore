using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConnectFore
{
    /// <summary>
    /// Interaction logic for pgRules.xaml
    /// </summary>
    public partial class pgRules : Page
    {
        System.Windows.Threading.DispatcherTimer navigationTimer = new System.Windows.Threading.DispatcherTimer();
        private MediaPlayer mediaPlayer = new MediaPlayer();
        Speak _speak = new Speak();

        public pgRules()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            navigationTimer.Tick += navigationTimer_Tick;
            navigationTimer.Interval = new TimeSpan(0, 1, 0);
            navigationTimer.Start();

            _speak.speakRules();
        }

        private void navigationTimer_Tick(object sender, EventArgs e)
        {
            navigationTimer.Stop();
            pgMain frmMain = new pgMain();
            this.NavigationService.Navigate(frmMain);

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            pgMain frmMain = new pgMain();
            this.NavigationService.Navigate(frmMain);
            mediaPlayer.Stop();
        }
    }
}
