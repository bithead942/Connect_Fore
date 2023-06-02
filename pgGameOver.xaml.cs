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
using Connect;
using GameBoard;
using BallEject;
using System.Threading;

namespace ConnectFore
{
    /// <summary>
    /// Interaction logic for pgGameOver.xaml
    /// </summary>
    public partial class pgGameOver : Page
    {
        System.Windows.Threading.DispatcherTimer navigationTimer = new System.Windows.Threading.DispatcherTimer();
        private MediaPlayer mediaPlayer = new MediaPlayer();
        Speak _speak = new Speak();
        Result _result = Result.Invalid;
        Player _player = Player.Player1;
        int _startCol = 0;
        int _numCols = 0;
        MainWindow.AIOption _aiOption;

        public pgGameOver(Result result, Player player, int startCol, int numCols, MainWindow.AIOption aiOption)
        {
            InitializeComponent();
            _result = result;
            _player = player;
            _startCol = startCol;
            _numCols = numCols;
            _aiOption = aiOption;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            //Thread.Sleep(1000); // Wait for GetBoard to finish?
            navigationTimer.Tick += navigationTimer_Tick;
            navigationTimer.Interval = new TimeSpan(0, 2, 0);
            navigationTimer.Start();

            switch (_result)
            {
                case Result.Intermediate:   //Quit
                    if (_player == Player.Player1)
                    {
                        lblTitle.Text = "GAME OVER:  Blue Quits";
                        mySolidColorBrush.Color = Color.FromRgb(255, 0, 0);  //Show Red
                    }
                    else
                    {
                        lblTitle.Text = "GAME OVER:  Red Quits";
                        mySolidColorBrush.Color = Color.FromRgb(0, 0, 255);  //Show Blue
                    }
                    recBackground.Fill = mySolidColorBrush;
                    if (_aiOption == MainWindow.AIOption.TwoPlayer)
                        _speak.speakQuit(2);
                    else
                        _speak.speakQuit(1);

                    //Lights Off
                    MainWindow._gameBoardControl.LightOff_Retry();
                    break;

                case Result.Invalid:   //Somebody cheated
                    if (_player == Player.Player1)
                    {
                        lblTitle.Text = "GAME OVER:  Blue Cheated!";
                        mySolidColorBrush.Color = Color.FromRgb(255, 0, 0);  //Show Red
                    }
                    else
                    {
                        lblTitle.Text = "GAME OVER:  Red Cheated!";
                        mySolidColorBrush.Color = Color.FromRgb(0, 0, 255);  //Show Blue
                    }
                    recBackground.Fill = mySolidColorBrush;
                    _speak.speakCheating();

                    //Lights Off
                    MainWindow._gameBoardControl.LightOff_Retry();
                    break;

                case Result.Win:   //Somebody won


                    if (_aiOption == MainWindow.AIOption.TwoPlayer)
                    {
                        _speak.speak2PlayerGameOver(Result.Win, _player);
                        if (_player == Player.Player1)
                        {
                            lblTitle.Text = "Blue Wins!!";
                            mySolidColorBrush.Color = Color.FromRgb(0, 0, 255);  //Show Blue
                            MainWindow._gameBoardControl.BlueWins_Retry(_startCol, _numCols);
                        }
                        else
                        {
                            lblTitle.Text = "Red Wins!!";
                            mySolidColorBrush.Color = Color.FromRgb(255, 0, 0);  //Show Red
                            MainWindow._gameBoardControl.RedWins_Retry(_startCol, _numCols);
                        }
                    }
                    else  // 1 Player game
                    {
                        if (_player == Player.Player1)  // Com wins
                            _speak.speakComWin();
                        else
                            _speak.speakComLoss();
                        if (_player == Player.Player1)
                        {
                            lblTitle.Text = "Robot Wins!!";
                            mySolidColorBrush.Color = Color.FromRgb(0, 0, 255);  //Show Blue
                            MainWindow._gameBoardControl.BlueWins_Retry(_startCol, _numCols);
                        }
                        else
                        {
                            lblTitle.Text = "Human Wins";
                            mySolidColorBrush.Color = Color.FromRgb(255, 0, 0);  //Show Red
                            MainWindow._gameBoardControl.RedWins_Retry(_startCol, _numCols);
                        }
                    }
                    recBackground.Fill = mySolidColorBrush;

                    break;

                case Result.Tie:   //It's a tie
                    lblTitle.Text = "It's A Tie!!";
                    mySolidColorBrush.Color = Color.FromRgb(255, 0, 255);  //Show Purple
                    recBackground.Fill = mySolidColorBrush;
                    _speak.speakTie();
                    break;

                default:
                    //Should never happen
                    break;
            }

        }

        private void navigationTimer_Tick(object sender, EventArgs e)
        {
            navigationTimer.Stop();
            Thread _resetThread_Board = new Thread(new ThreadStart(resetThread_Board));
            _resetThread_Board.Name = "tBoard";
            _resetThread_Board.IsBackground = true;
            _resetThread_Board.Start();

            Thread _resetThread_Pins = new Thread(new ThreadStart(resetThread_Pins));
            _resetThread_Pins.Name = "tPins";
            _resetThread_Pins.IsBackground = true;
            _resetThread_Pins.Start();

            pgMain frmMain = new pgMain();
            this.NavigationService.Navigate(frmMain);
        }

        #region BUTTONS
        private void btnPlayAgain_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Stop();

            Thread _resetThread_Board = new Thread(new ThreadStart(resetThread_Board));
            _resetThread_Board.Name = "tBoard";
            _resetThread_Board.IsBackground = true;
            _resetThread_Board.Start();

            Thread _resetThread_Pins = new Thread(new ThreadStart(resetThread_Pins));
            _resetThread_Pins.Name = "tPins";
            _resetThread_Pins.IsBackground = true;
            _resetThread_Pins.Start();

            pgLetsPlay frmLetsPlay = new pgLetsPlay();
            this.NavigationService.Navigate(frmLetsPlay);
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            mediaPlayer.Stop();

            Thread _resetThread_Board = new Thread(new ThreadStart(resetThread_Board));
            _resetThread_Board.Name = "tBoard";
            _resetThread_Board.IsBackground = true;
            _resetThread_Board.Start();

            Thread _resetThread_Pins = new Thread(new ThreadStart(resetThread_Pins));
            _resetThread_Pins.Name = "tPins";
            _resetThread_Pins.IsBackground = true;
            _resetThread_Pins.Start();

            pgMain frmMain = new pgMain();
            this.NavigationService.Navigate(frmMain);
        }
        #endregion

        void resetThread_Board()
        {
            MainWindow._gameBoardControl.LightOff_Retry();
            //MainWindow._gameBoardControl.Disconnect();
        }

        void resetThread_Pins()
        {
            MainWindow._ejectServoControl.Eject();
            //MainWindow._ejectServoControl.Disconnect();
        }
    }
}
