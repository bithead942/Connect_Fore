using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for pgLetsPlay.xaml
    /// </summary>
    public partial class pgLetsPlay : Page
    {
        System.Windows.Threading.DispatcherTimer navigationTimer = new System.Windows.Threading.DispatcherTimer();
        private MediaPlayer mediaPlayer = new MediaPlayer();

        public pgLetsPlay()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            navigationTimer.Tick += navigationTimer_Tick;
            navigationTimer.Interval = new TimeSpan(0, 1, 0);
            navigationTimer.Start();

            lblTitle.Text = "Waiting for Reset...";

            Thread _startupThread1 = new Thread(new ThreadStart(startupThread_Board));
            _startupThread1.Name = "tBoard";
            _startupThread1.IsBackground = true;
            _startupThread1.Start();

        }

        private void navigationTimer_Tick(object sender, EventArgs e)
        {
            navigationTimer.Stop();
            pgMain frmMain = new pgMain();
            this.NavigationService.Navigate(frmMain);
        }

        void startupThread_Board()
        {
            while (MainWindow._ejectServoControl.isEjecting()) { }

            MainWindow._gameBoardControl.LightOff_Retry();

            Application.Current.Dispatcher.Invoke(() =>
            {
                mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Navigation/Choose Opponent.m4a"));
                mediaPlayer.Play();

                lblTitle.Text = "Choose Your Opponent";

                btn1Player.Visibility = Visibility.Visible;
                btn2Player.Visibility = Visibility.Visible;
            });
        }

        private void btn1Player_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            pgChooseAI frmChooseAI = new pgChooseAI();
            this.NavigationService.Navigate(frmChooseAI);
            mediaPlayer.Stop();
        }

        private void btn2Player_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            pgGameBoard frmGameBoard = new pgGameBoard((int) MainWindow.AIOption.TwoPlayer, (int) MainWindow.DifficultyOption.NA);
            this.NavigationService.Navigate(frmGameBoard);
            mediaPlayer.Stop();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
               mediaPlayer.Stop();
                AdminPassword frmAdmin = new AdminPassword();
                frmAdmin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                frmAdmin.ShowDialog();  //Waits for a response
         }
    }
}
