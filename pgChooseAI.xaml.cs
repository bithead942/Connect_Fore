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
    /// Interaction logic for pgChooseAI.xaml
    /// </summary>
    public partial class pgChooseAI : Page
    {
        System.Windows.Threading.DispatcherTimer navigationTimer = new System.Windows.Threading.DispatcherTimer();
        private MediaPlayer mediaPlayer = new MediaPlayer();

        public pgChooseAI()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            navigationTimer.Tick += navigationTimer_Tick;
            navigationTimer.Interval = new TimeSpan(0, 1, 0);
            navigationTimer.Start();

            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Navigation/Choose AI.m4a"));
            mediaPlayer.Play();

        }

        private void navigationTimer_Tick(object sender, EventArgs e)
        {
            navigationTimer.Stop();
            pgMain frmMain = new pgMain();
            this.NavigationService.Navigate(frmMain);

        }

        #region Buttons
        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            pgGameBoard frmGameBoard = new pgGameBoard(MainWindow.AIOption.Random, MainWindow.DifficultyOption.NA);
            this.NavigationService.Navigate(frmGameBoard);
            mediaPlayer.Stop();

        }

        private void btnDefensive_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            pgGameBoard frmGameBoard = new pgGameBoard(MainWindow.AIOption.Defensive, MainWindow.DifficultyOption.NA);
            this.NavigationService.Navigate(frmGameBoard);
            mediaPlayer.Stop();

        }

        private void btnMINMAX_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Stop();
            Difficulty1 frmDifficulty = new Difficulty1();
            frmDifficulty.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmDifficulty.ShowDialog();  //Waits for a response

            if (Difficulty1.iSelection > 0)
            {
                pgGameBoard frmGameBoard = new pgGameBoard(MainWindow.AIOption.MINMAX, (MainWindow.DifficultyOption) Difficulty1.iSelection);
                this.NavigationService.Navigate(frmGameBoard);
            }
            else
            {
                navigationTimer.Start();
            }

        }

        private void btnHybrid_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Stop();
            Difficulty1 frmDifficulty = new Difficulty1();
            frmDifficulty.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmDifficulty.ShowDialog();  //Waits for a response

            if (Difficulty1.iSelection > 0)
            {
                pgGameBoard frmGameBoard = new pgGameBoard(MainWindow.AIOption.Hybrid, (MainWindow.DifficultyOption) Difficulty1.iSelection);
                this.NavigationService.Navigate(frmGameBoard);
            }
            else
            {
                navigationTimer.Start();
            }
        }

        private void btnMonteCarlo_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Stop();
            Difficulty2 frmDifficulty = new Difficulty2();
            frmDifficulty.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmDifficulty.ShowDialog();  //Waits for a response

            if (Difficulty2.iSelection > 0)
            {
                pgGameBoard frmGameBoard = new pgGameBoard(MainWindow.AIOption.Monte_Carlo, (MainWindow.DifficultyOption) Difficulty2.iSelection);
                this.NavigationService.Navigate(frmGameBoard);
            }
            else
            {
                navigationTimer.Start();
            }
        }

        private void btnQLearning_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Stop();
            Difficulty3 frmDifficulty = new Difficulty3();
            frmDifficulty.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmDifficulty.ShowDialog();  //Waits for a response

            if (Difficulty3.iSelection > 0)
            {
                pgGameBoard frmGameBoard = new pgGameBoard(MainWindow.AIOption.Q_Learning, (MainWindow.DifficultyOption) Difficulty3.iSelection);
                this.NavigationService.Navigate(frmGameBoard);
            }
            else
            {
                navigationTimer.Start();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            pgMain frmMain = new pgMain();
            this.NavigationService.Navigate(frmMain);
            mediaPlayer.Stop();
        }
        #endregion
    }
}
