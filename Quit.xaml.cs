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
using System.Windows.Shapes;

namespace ConnectFore
{
    /// <summary>
    /// Interaction logic for Quit.xaml
    /// </summary>
    public partial class Quit : Window
    {
        public static int iSelection = 0;
        System.Windows.Threading.DispatcherTimer navigationTimer = new System.Windows.Threading.DispatcherTimer();

        public Quit()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            navigationTimer.Tick += navigationTimer_Tick;
            navigationTimer.Interval = new TimeSpan(0, 1, 0);
            navigationTimer.Start();
        }

        private void navigationTimer_Tick(object sender, EventArgs e)
        {
            navigationTimer.Stop();
            this.Close();

        }

        #region BUTTONS
        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            //Eject
            iSelection = 1;
            navigationTimer.Stop();
            pgGameBoard.HurryTimer.Stop();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            iSelection = 2;
            navigationTimer.Stop();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            iSelection = 0;
            navigationTimer.Stop();
            this.Close();
        }

        #endregion
    }
}
