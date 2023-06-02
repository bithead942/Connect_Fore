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
using GameBoard;
using BallEject;
using Connect;
using System.Threading;
using BallDispenser;
using Robot;

namespace ConnectFore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum AIOption { TwoPlayer, Random, Defensive, MINMAX, Hybrid, Monte_Carlo, Q_Learning };
        public enum DifficultyOption { NA, Low, Medium, High };
        public static GameBoardControl _gameBoardControl = new GameBoardControl();
        public static EjectServoControl _ejectServoControl = new EjectServoControl();
        public static BallDispenserControl _ballDispenserControl = new BallDispenserControl();
        public static GameLogic _gameLogic = new GameLogic();
        public static RobotControl _robotControl = new RobotControl();

        public MainWindow()
        {
            InitializeComponent();

            Thread _LoadQThread = new Thread(new ThreadStart(LoadQThread));
            _LoadQThread.Name = "tLoadQ";
            _LoadQThread.IsBackground = true;
            _LoadQThread.Start();

            pgMain frmMain = new pgMain();
            MainFrame.NavigationService.Navigate(frmMain);
        }

        void LoadQThread()
        {
            _gameLogic.LoadQs(DifficultyOption.High);
            _gameLogic.LoadQs(DifficultyOption.Medium);
            _gameLogic.LoadQs(DifficultyOption.Low);
        }

            void window_closing(object sender, EventArgs e)
        {

            _gameBoardControl.LightOff_Retry();
            _gameBoardControl.Disconnect();
            _ballDispenserControl.LightOff_Retry();
            _ballDispenserControl.Disconnect();
            _robotControl.LightOff_Retry();
            _robotControl.LauncherOff();
            _robotControl.LaserOff();
            _robotControl.Disconnect();
        }

    }
}
