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
using System.Threading;

namespace ConnectFore
{
    /// <summary>
    /// Interaction logic for AdminGB.xaml
    /// </summary>
    public partial class AdminFunctions : Window
    {
        System.Windows.Threading.DispatcherTimer RefreshTimer = new System.Windows.Threading.DispatcherTimer();

        public AdminFunctions()
        {
            InitializeComponent();
        }

        public void Page_Loaded(object sender, EventArgs e)
        {
            UpdateStats();

            RefreshTimer.Tick += RefreshTimer_Tick;
            RefreshTimer.Interval = new TimeSpan(0, 0, 10);
            RefreshTimer.Start();
        }

        void RefreshTimer_Tick(object sender, EventArgs e)
        {
            UpdateStats();
        }

        void UpdateStats()
        {
            if (MainWindow._gameBoardControl.IsOpen() || MainWindow._gameBoardControl.Connect())
            {
                GameBoardStatus.Fill = Brushes.Green;
            }
            else
            {
                GameBoardStatus.Fill = Brushes.Black;
            }

            if (MainWindow._ejectServoControl.IsOpen())
            {
                BallEjectorStatus.Fill = Brushes.Green;
            }
            else
            {
                BallEjectorStatus.Fill = Brushes.Black;
            }

            if (MainWindow._ballDispenserControl.IsOpen() || MainWindow._ballDispenserControl.Connect())
            {
                BallDispenserStatus.Fill = Brushes.Green;
            }
            else
            {
                BallDispenserStatus.Fill = Brushes.Black;
            }

            if (MainWindow._robotControl.IsOpen() || MainWindow._robotControl.Connect())
            {
                RobotStatus.Fill = Brushes.Green;
            }
            else
            {
                RobotStatus.Fill = Brushes.Black;
            }

        }

        #region "Main"
        private void btnKillApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn1Player_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._ballDispenserControl.BlueOpen_Retry();
            Thread.Sleep(10000);
            MainWindow._ballDispenserControl.BlueClose_Retry();

        }

        private void btn2Player_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._robotControl.BlueOpen();
            Thread.Sleep(10000);
            MainWindow._robotControl.BlueClose();
        }

        private void btnDonePlaying_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._robotControl.BlueOpen();
            MainWindow._ballDispenserControl.BlueOpen_Retry();
            MainWindow._ballDispenserControl.RedOpen_Retry();
            MainWindow._ejectServoControl.Eject();
            //Thread.Sleep(10000);  //Not needed since Eject acts like delay
            MainWindow._robotControl.BlueClose();
            MainWindow._ballDispenserControl.RedClose_Retry();
            MainWindow._ballDispenserControl.BlueClose_Retry();
        }
        #endregion

        #region "Game Board"
        private void btnEject_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._ejectServoControl.Eject();
        }

        private void btnPinsUp_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._ejectServoControl.PinsUp();
        }

        private void btnPinsDown_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._ejectServoControl.PinsDown();
        }

        private void btnLightsOn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._gameBoardControl.LightOn_Retry();
        }

        private void btnGBLightsOff_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._gameBoardControl.LightOff_Retry();
        }
        #endregion

        #region "Ball Dispenser"
        private void btnBlueBall_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._ballDispenserControl.BlueDispense_Retry();
        }

        private void btnBlueOpen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._ballDispenserControl.BlueOpen_Retry();
        }

        private void btnBlueClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._ballDispenserControl.BlueClose_Retry();
        }

        private void btnRedBall_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._ballDispenserControl.RedDispense_Retry();
        }

        private void btnRedOpen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._ballDispenserControl.RedOpen_Retry();
        }

        private void btnRedClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._ballDispenserControl.RedClose_Retry();
        }

        private void btnBDLightsOff_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._ballDispenserControl.LightOff();
        }

        #endregion

        #region "Robot"
        private void btnRLightsOff_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._robotControl.LightOff();
        }

        private void btnRLaserOn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._robotControl.LaserOn();
        }

        private void btnRLaserOff_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._robotControl.LaserOff();
        }

        private void btnRDispense_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._robotControl.DispenseBlue();
        }

        private void btnRDispenserOpen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._robotControl.BlueOpen();
        }

        private void btnRDispenserClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._robotControl.BlueClose();
        }

        private void btnRLauncherOn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._robotControl.LauncherOn("200");
        }

        private void btnRLauncherOff_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._robotControl.LauncherOff();
        }
        #endregion


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            RefreshTimer.Stop();
            this.Close();
        }


    }
}
