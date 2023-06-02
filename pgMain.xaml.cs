using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
    /// Interaction logic for pgMain.xaml
    /// </summary>
    public partial class pgMain : Page
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();

        public pgMain()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Open(new Uri("C:/Users/gregs/source/repos/ConnectFore/multimedia/Recordings/Navigation/Welcome2.m4a"));
            mediaPlayer.Play();

            Thread _startupThread1 = new Thread(new ThreadStart(Startup_Check));
            _startupThread1.Name = "tBoard";
            _startupThread1.IsBackground = true;
            _startupThread1.Start();
        }

        void Startup_Check()
        {
            bool GBConnect = false;
            bool BDConnect = false;
            bool BEConnect = false;
            bool RConnect = false;

            while (!GBConnect || !BDConnect || !BEConnect || !RConnect)
            {
                //Check for GAME BOARD
                if (!MainWindow._gameBoardControl.LightOff())
                {
                    //MessageBox.Show("Could not connect to Game Board.");
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        GameBoardStatus.Fill = Brushes.Black;
                    });
                    GBConnect = false;
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        GameBoardStatus.Fill = Brushes.Green;
                    });
                    GBConnect = true;
                }

                // Check for BALL DISPENSER
                if (!MainWindow._ballDispenserControl.LightOff())
                {
                    //MessageBox.Show("Could not connect to Ball Dispenser.");
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        BallDispenserStatus.Fill = Brushes.Black;
                    });
                    BDConnect = false;
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        BallDispenserStatus.Fill = Brushes.Green;
                    });
                    BDConnect = true;
                }

                // Check for BALL EJECTOR
                if (!MainWindow._ejectServoControl.PinsUp())
                {
                    //MessageBox.Show("Could not connect to Ball Ejector.");
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        BallEjectorStatus.Fill = Brushes.Black;
                    });
                    BEConnect = false;
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        BallEjectorStatus.Fill = Brushes.Green;
                    });
                    BEConnect = true;
                }

                // Check for ROBOT
                if (!MainWindow._robotControl.LightOff())
                {
                    //MessageBox.Show("Could not connect to Robot.");
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        RobotStatus.Fill = Brushes.Black;
                    });
                    RConnect = false;
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        RobotStatus.Fill = Brushes.Green;
                    });
                    RConnect = true;
                }

                if (GBConnect && BDConnect && BEConnect && RConnect)
                {
                    while (MainWindow._ejectServoControl.isEjecting()) { }

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        btnLetsPlay.Visibility = Visibility.Visible;
                        btnRules.Visibility = Visibility.Visible;
                        grdStatus.Visibility = Visibility.Hidden;
                    });
                }
                else
                {
                    Thread.Sleep(2000);
                }
            }
        }


        private void btnRules_Click(object sender, RoutedEventArgs e)
        {
            pgRules frmRules = new pgRules();
            this.NavigationService.Navigate(frmRules);
            mediaPlayer.Stop();
        }

        private void btnLetsPlay_Click(object sender, RoutedEventArgs e)
        {
            pgLetsPlay frmLetsPlay = new pgLetsPlay();
            this.NavigationService.Navigate(frmLetsPlay);
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
