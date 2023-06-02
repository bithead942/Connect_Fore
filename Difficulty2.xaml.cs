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
    /// Interaction logic for Difficulty2.xaml
    /// </summary>
    public partial class Difficulty2 : Window
    {
        public static int iSelection = 0;
        System.Windows.Threading.DispatcherTimer navigationTimer = new System.Windows.Threading.DispatcherTimer();
        private MediaPlayer mediaPlayer = new MediaPlayer();

        public Difficulty2()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            navigationTimer.Tick += navigationTimer_Tick;
            navigationTimer.Interval = new TimeSpan(0, 1, 0);
            navigationTimer.Start();

            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Navigation/Difficulty Level 2.m4a"));
            mediaPlayer.Play();
        }

        private void navigationTimer_Tick(object sender, EventArgs e)
        {
            navigationTimer.Stop();
            this.Close();

        }

        #region Buttons
        private void btnDifficulty1_Click(object sender, RoutedEventArgs e)
        {
            iSelection = 1;
            navigationTimer.Stop();
            mediaPlayer.Stop();
            this.Close();
        }

        private void btnDifficulty2_Click(object sender, RoutedEventArgs e)
        {
            iSelection = 2;
            navigationTimer.Stop();
            mediaPlayer.Stop();
            this.Close();
        }

        private void btnDifficulty3_Click(object sender, RoutedEventArgs e)
        {
            iSelection = 3;
            navigationTimer.Stop();
            mediaPlayer.Stop();
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            iSelection = 0;
            navigationTimer.Stop();
            mediaPlayer.Stop();
            this.Close();
        }
        #endregion
    }
}
