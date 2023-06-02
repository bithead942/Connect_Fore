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
using System.Threading;
using Connect;

namespace ConnectFore
{
    /// <summary>
    /// Interaction logic for pgGameBoard.xaml
    /// </summary>
    public partial class pgGameBoard : Page
    {
        public static BoardState TheBoard;
        public Thread currentGameThread;
        double[] scores = new double[7];
        int moveCounter;
        int totalMoves = 0;
        Speak _speak = new Speak();
        char[,] board = new char[7, 6];
        int iFirstPlayer = 0;
        int iNextMove = 0;
        Player currentPlayer = Player.Player1;
        MainWindow.AIOption _selectedAI = MainWindow.AIOption.TwoPlayer;
        MainWindow.DifficultyOption _selectedDifficulty = MainWindow.DifficultyOption.NA;
        public static System.Windows.Threading.DispatcherTimer HurryTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer ExtraBallTimer = new System.Windows.Threading.DispatcherTimer();

        public pgGameBoard(MainWindow.AIOption selectedAI, MainWindow.DifficultyOption selectedDifficulty)
        {
            InitializeComponent();
            _selectedAI = selectedAI;
            _selectedDifficulty = selectedDifficulty;
        }  //Constructor

        #region STARTUP
        public void Page_Loaded(object sender, EventArgs e)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            if (_selectedAI == MainWindow.AIOption.Q_Learning)
            {
                MainWindow._gameLogic.SwitchQs(_selectedDifficulty);
            }

            Thread _startupThread1 = new Thread(new ThreadStart(startupThread_Board));
            _startupThread1.Name = "tBoard";
            _startupThread1.IsBackground = true;
            _startupThread1.Start();

            Thread _startupThread2 = new Thread(new ThreadStart(startupThread_Pins));
            _startupThread2.Name = "tPins";
            _startupThread2.IsBackground = true;
            _startupThread2.Start();

            HurryTimer.Tick += HurryTimer_Tick;
            HurryTimer.Interval = new TimeSpan(0, 2, 0);
            //Dont's tart HurryTimer yet - wait for game play to begin.

            ExtraBallTimer.Tick += ExtraBallTimer_Tick;
            ExtraBallTimer.Interval = new TimeSpan(0, 0, 5);

            // UI Setup
            switch (_selectedAI)
            {
                case MainWindow.AIOption.TwoPlayer:
                    lblMode.Text = "2 Player";
                    spAIMode.Visibility = Visibility.Hidden;
                    spNextMove.Visibility = Visibility.Hidden;
                    spStats.Visibility = Visibility.Hidden;
                    btnInfo.Visibility = Visibility.Visible;
                    btnExtraBall.Visibility = Visibility.Visible;
                    btnQuit.Visibility = Visibility.Visible;
                    Score1.Text = "";
                    Score2.Text = "";
                    Score3.Text = "";
                    Score4.Text = "";
                    Score5.Text = "";
                    Score6.Text = "";
                    Score7.Text = "";
                    break;
                case MainWindow.AIOption.Random:
                    lblMode.Text = "1 Player";
                    lblAIMode.Text = "Random";
                    lblNextMove.Text = "";
                    spAIMode.Visibility = Visibility.Visible;
                    spNextMove.Visibility = Visibility.Visible;
                    spStats.Visibility = Visibility.Hidden;
                    btnInfo.Visibility = Visibility.Visible;
                    btnExtraBall.Visibility = Visibility.Visible;
                    btnQuit.Visibility = Visibility.Visible;
                    break;
                case MainWindow.AIOption.Defensive:
                    lblMode.Text = "1 Player";
                    lblAIMode.Text = "Defensive";
                    lblNextMove.Text = "";
                    spAIMode.Visibility = Visibility.Visible;
                    spNextMove.Visibility = Visibility.Visible;
                    spStats.Visibility = Visibility.Hidden;
                    btnExtraBall.Visibility = Visibility.Visible;
                    btnInfo.Visibility = Visibility.Visible;
                    btnExtraBall.Visibility = Visibility.Visible;
                    btnQuit.Visibility = Visibility.Visible;
                    break;
                case MainWindow.AIOption.MINMAX:
                    lblMode.Text = "1 Player";
                    lblAIMode.Text = "MINMAX";
                    lblNextMove.Text = "";
                    lblStats.Text = "Nodes:";
                    lblStatsVal.Text = "0";
                    spAIMode.Visibility = Visibility.Visible;
                    spNextMove.Visibility = Visibility.Visible;
                    spStats.Visibility = Visibility.Visible;
                    btnInfo.Visibility = Visibility.Visible;
                    btnExtraBall.Visibility = Visibility.Visible;
                    btnQuit.Visibility = Visibility.Visible;
                    break;
                case MainWindow.AIOption.Hybrid:
                    lblMode.Text = "1 Player";
                    lblAIMode.Text = "Hybrid";
                    lblNextMove.Text = "";
                    lblStats.Text = "Nodes:";
                    lblStatsVal.Text = "0";
                    spAIMode.Visibility = Visibility.Visible;
                    spNextMove.Visibility = Visibility.Visible;
                    spStats.Visibility = Visibility.Visible;
                    btnInfo.Visibility = Visibility.Visible;
                    btnExtraBall.Visibility = Visibility.Visible;
                    btnQuit.Visibility = Visibility.Visible;
                    break;
                case MainWindow.AIOption.Monte_Carlo:
                    lblMode.Text = "1 Player";
                    lblAIMode.Text = "Monte Carlo";
                    lblNextMove.Text = "";
                    lblStats.Text = "Sim Games:";
                    lblStatsVal.Text = "0";
                    spAIMode.Visibility = Visibility.Visible;
                    spNextMove.Visibility = Visibility.Visible;
                    spStats.Visibility = Visibility.Visible;
                    btnInfo.Visibility = Visibility.Visible;
                    btnExtraBall.Visibility = Visibility.Visible;
                    btnQuit.Visibility = Visibility.Visible;
                    break;
                case MainWindow.AIOption.Q_Learning:
                    lblMode.Text = "1 Player";
                    lblAIMode.Text = "Q Learning";
                    lblNextMove.Text = "";
                    lblStats.Text = "Learning Ops:";
                    lblStatsVal.Text = MainWindow._gameLogic.Q.Count.ToString("N0");
                    spAIMode.Visibility = Visibility.Visible;
                    spNextMove.Visibility = Visibility.Visible;
                    spStats.Visibility = Visibility.Visible;
                    btnInfo.Visibility = Visibility.Visible;
                    btnExtraBall.Visibility = Visibility.Visible;
                    btnQuit.Visibility = Visibility.Visible;
                    break;
            }

            // SPEAK
            if (_selectedAI == MainWindow.AIOption.TwoPlayer)
                _speak.speakStart2Player();
            else
                _speak.speakStart1Player(_selectedAI, _selectedDifficulty);

            // WHO GOES FIRST?
            // If AI = Random or Defensive, or if 2 Player Flip;  Else, Blue (Computer) always goes first
            if (_selectedAI == MainWindow.AIOption.TwoPlayer || _selectedAI == MainWindow.AIOption.Random || _selectedAI == MainWindow.AIOption.Defensive)
            {
                CoinFlip frmCoinFlip = new CoinFlip(_selectedAI);
                frmCoinFlip.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                frmCoinFlip.ShowDialog();  //Waits for a response
                iFirstPlayer = CoinFlip.iFirstPlayer;

                if (iFirstPlayer == 2)
                {
                    if (_selectedAI == MainWindow.AIOption.TwoPlayer)
                        lblTitle.Text = "Red's Turn";
                    else
                        lblTitle.Text = "Human's Turn";
                    MainWindow._ballDispenserControl.RedDispense_Retry();
                    mySolidColorBrush.Color = Color.FromRgb(255, 0, 0);
                }
                else
                {
                    if (_selectedAI == MainWindow.AIOption.TwoPlayer)
                    {
                        lblTitle.Text = "Blue's Turn";
                        MainWindow._ballDispenserControl.BlueDispense_Retry();
                    }
                    else
                    {
                        lblTitle.Text = "Robot's Turn";
                    }
                    mySolidColorBrush.Color = Color.FromRgb(0, 0, 255);
                }
            }
            else  //Computer (Blue) goes first
            {
                lblTitle.Text = "Robot's Turn";
                iFirstPlayer = 1;
                mySolidColorBrush.Color = Color.FromRgb(0, 0, 255);
                Thread.Sleep(4000);
                _speak.speakPlayFirst(_selectedAI, Player.Player1);
            }
            recBackground.Fill = mySolidColorBrush;

            while (_startupThread1.IsAlive == true) { }   //Wait for thread to finish
            if (MainWindow._gameBoardControl.IsOpen())
            {
                MainWindow._gameBoardControl.LightOn_Retry();
                DrawBoard();
            }
            totalMoves = 0;

            //START GAME
            if (_selectedAI == MainWindow.AIOption.TwoPlayer)
            {
                Thread t = new Thread(TwoPlayerGame);
                currentGameThread = t;
                t.Start();
            }
            else
            {
                Thread t = new Thread(OnePlayerGame);
                currentGameThread = t;
                t.Start();
            }

        }

        void startupThread_Board()
        {
            string strGameBoard = "";

            strGameBoard = MainWindow._gameBoardControl.GetBoard_Retry();
            if (MainWindow._gameBoardControl.IsOpen())
            {
                if (strGameBoard[0] != '\0')
                {
                    board = MainWindow._gameLogic.updateBoard(strGameBoard);

                    TheBoard = new BoardState(6, 7);
                    TheBoard.board = board;
                }
            }
        }

        void startupThread_Pins()
        {
            MainWindow._ejectServoControl.PinsUp();
        }

        #endregion

        public void DrawBoard()
        {
            BoardGrid.Children.Clear();
            for (int i = 0; i < TheBoard.rows; i++)
            {
                for (int j = 0; j < TheBoard.cols; j++)
                {
                    Ellipse r = new Ellipse();
                    r.Height = 144;
                    r.Width = 144;
                    if (TheBoard.board[i, j] == '-')
                    {
                        r.Fill = new SolidColorBrush(Colors.White);
                    }
                    else if (TheBoard.board[i, j] == 'B')
                    {
                        r.Fill = new SolidColorBrush(Colors.Blue);
                    }
                    else
                    {
                        r.Fill = new SolidColorBrush(Colors.Red);
                    }
                    Canvas.SetLeft(r, (j * 210) + 39);
                    Canvas.SetTop(r, (i * 144) + (i * 5) + 5);
                    BoardGrid.Children.Add(r);
                }
            }
        }

        void HurryTimer_Tick(object sender, EventArgs e)
        {
            HurryTimer.Stop();
            _speak.speakHurry();
        }

        void ExtraBallTimer_Tick(object sender, EventArgs e)
        {
            ExtraBallTimer.Stop();
            btnExtraBall.IsEnabled = true;
        }

        #region GAME PLAY
        public void TwoPlayerGame()
        {
            Result r = Result.Invalid;
            Player winningPlayer = Player.Player1;


            while (!(r == Result.Win || r == Result.Tie || r == Result.Loss))  //Play 1 game until Win/Lose/Draw
            {
                currentPlayer = (iFirstPlayer == 1) ? Player.Player1 : Player.Player2;
                r = HumanPlay((iFirstPlayer == 1) ? Player.Player1 : Player.Player2);

                // Dispense Ball for next player
                if (r != Result.Win && r != Result.Tie)
                {
                    if (currentPlayer == Player.Player1)
                    {
                        MainWindow._ballDispenserControl.RedDispense_Retry();  //Prepare for 2nd player (Red)
                    }
                    else
                    {
                        MainWindow._ballDispenserControl.BlueDispense_Retry();  //Prepare for 2nd player (Blue)
                    }
                }

                BoardGrid.Dispatcher.Invoke(() =>
                {
                    SolidColorBrush mySolidColorBrush = new SolidColorBrush();

                    _speak.speakHumanTurn((iFirstPlayer == 1) ? Player.Player2 : Player.Player1, _selectedAI);
                    DrawBoard();
                    if (iFirstPlayer == 1)
                    {
                        lblTitle.Text = "Red's Turn";
                        mySolidColorBrush.Color = Color.FromRgb(255, 0, 0);
                    }
                    else
                    {
                        lblTitle.Text = "Blue's Turn";
                        mySolidColorBrush.Color = Color.FromRgb(0, 0, 255);
                    }
                    recBackground.Fill = mySolidColorBrush;
                });
                if (!(r == Result.Win || r == Result.Tie || r == Result.Loss))
                {
                    currentPlayer = (iFirstPlayer == 1) ? Player.Player2 : Player.Player1;
                    r = HumanPlay((iFirstPlayer == 1) ? Player.Player2 : Player.Player1);

                    // Dispense Ball for next player
                    if (r != Result.Win && r != Result.Tie)
                    {
                        if (currentPlayer == Player.Player1)
                        {
                            MainWindow._ballDispenserControl.RedDispense_Retry();  //Prepare for 1nd player (Red)
                        }
                        else
                        {
                            MainWindow._ballDispenserControl.BlueDispense_Retry();  //Prepare for 1nd player (Blue)
                        }
                    }

                    BoardGrid.Dispatcher.Invoke(() =>
                    {
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();

                        _speak.speakHumanTurn((iFirstPlayer == 1) ? Player.Player1 : Player.Player2, _selectedAI);
                        DrawBoard();
                        if (iFirstPlayer == 1)
                        {
                            lblTitle.Text = "Blue's Turn";
                            mySolidColorBrush.Color = Color.FromRgb(0, 0, 255);
                        }
                        else
                        {
                            lblTitle.Text = "Red's Turn";
                            mySolidColorBrush.Color = Color.FromRgb(255, 0, 0);
                        }
                        recBackground.Fill = mySolidColorBrush;
                    });
                    if ((r == Result.Win || r == Result.Tie || r == Result.Loss))
                    {
                        winningPlayer = (iFirstPlayer == 1) ? Player.Player2 : Player.Player1;
                    }
                }
                else
                {
                    winningPlayer = (iFirstPlayer == 1) ? Player.Player1 : Player.Player2;
                }
            }

            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                _speak.mediaPlayer.Stop();
                pgGameOver frmGameOver = new pgGameOver(r, winningPlayer, MainWindow._gameLogic.startcol, MainWindow._gameLogic.numcols, _selectedAI);
                this.NavigationService.Navigate(frmGameOver);
            });

            currentGameThread.Abort();
        }

        public void OnePlayerGame()
        {
            Player winningPlayer = Player.Player1;
            Result r = Result.Invalid;

            while (!(r == Result.Win || r == Result.Tie))  //Play 1 game until Win/Lose/Draw
            {
                //**********
                //FIRST PLAYER 
                //**********
                if (iFirstPlayer == 1)  //First player is robot
                {
                    currentPlayer = Player.Player1;
                    r = RobotPlay(_selectedAI, _selectedDifficulty);

                    if (r != Result.Win && r != Result.Tie)
                    {
                        MainWindow._ballDispenserControl.RedDispense_Retry();  //Prepare for 2nd player (Red)
                    }
                }
                else  //First player is human
                {
                    HurryTimer.Start();
                    HurryTimer.Interval = new TimeSpan(0, 2, 0);

                    currentPlayer = Player.Player2;
                    r = HumanPlay(Player.Player2);
                    HurryTimer.Stop();
                }
                totalMoves++;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                    if (currentPlayer == Player.Player1)
                    {
                        _speak.mediaPlayer.Stop();
                        _speak.speakHumanTurn(Player.Player2, _selectedAI);
                    }
                    else
                    {
                        _speak.mediaPlayer.Stop();
                        _speak.speakComTurn();
                    }

                    DrawBoard();
                    if (iFirstPlayer == 1)
                    {
                        lblTitle.Text = "Human's Turn";
                        mySolidColorBrush.Color = Color.FromRgb(255, 0, 0);
                        Score1.Text = "";
                        Score2.Text = "";
                        Score3.Text = "";
                        Score4.Text = "";
                        Score5.Text = "";
                        Score6.Text = "";
                        Score7.Text = "";
                        lblNextMove.Text = "";
                        if (_selectedAI != MainWindow.AIOption.Q_Learning)
                            lblStatsVal.Text = "";
                        else
                            lblStatsVal.Text = MainWindow._gameLogic.Q.Count.ToString("N0");

                    }
                    else
                    {
                        lblTitle.Text = "Robot's Turn";
                        mySolidColorBrush.Color = Color.FromRgb(0, 0, 255);
                    }
                    recBackground.Fill = mySolidColorBrush;
                });

                //**********
                //SECOND PLAYER 
                //**********
                if (!(r == Result.Win || r == Result.Tie))
                {
                    if (iFirstPlayer == 1)  //Second player is human
                    {
                        HurryTimer.Start();
                        HurryTimer.Interval = new TimeSpan(0, 2, 0);

                        currentPlayer = Player.Player2;
                        r = HumanPlay(Player.Player2);
                        HurryTimer.Stop();
                    }
                    else  //Second player is robot
                    {
                        currentPlayer = Player.Player1;
                        r = RobotPlay(_selectedAI, _selectedDifficulty);

                        if (r != Result.Win && r != Result.Tie)
                        {
                            MainWindow._ballDispenserControl.RedDispense_Retry();  //Prepare for 2nd player (Red)
                        }
                    }
                    totalMoves++;

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();

                        if (currentPlayer == Player.Player1)
                        {
                            _speak.mediaPlayer.Stop();
                            _speak.speakHumanTurn(Player.Player2, _selectedAI);
                        }
                        else
                        {
                            _speak.mediaPlayer.Stop();
                            _speak.speakComTurn();
                        }

                        DrawBoard();
                        if (iFirstPlayer == 1)
                        {
                            lblTitle.Text = "Robot's Turn";
                            mySolidColorBrush.Color = Color.FromRgb(0, 0, 255);
                        }
                        else
                        {
                            lblTitle.Text = "Human's Turn";
                            mySolidColorBrush.Color = Color.FromRgb(255, 0, 0);
                            Score1.Text = "";
                            Score2.Text = "";
                            Score3.Text = "";
                            Score4.Text = "";
                            Score5.Text = "";
                            Score6.Text = "";
                            Score7.Text = "";
                            lblNextMove.Text = "";
                            if (_selectedAI != MainWindow.AIOption.Q_Learning)
                                lblStatsVal.Text = "";
                            else
                                lblStatsVal.Text = MainWindow._gameLogic.Q.Count.ToString("N0");
                        }
                        recBackground.Fill = mySolidColorBrush;
                    });
                    if ((r == Result.Win || r == Result.Tie))
                    {
                        winningPlayer = (iFirstPlayer == 1) ? Player.Player2 : Player.Player1;
                    }
                }
                else
                {
                    winningPlayer = (iFirstPlayer == 1) ? Player.Player1 : Player.Player2;
                }
            }
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                _speak.mediaPlayer.Stop();
                pgGameOver frmGameOver = new pgGameOver(r, winningPlayer, MainWindow._gameLogic.startcol, MainWindow._gameLogic.numcols, _selectedAI);
                this.NavigationService.Navigate(frmGameOver);
            });

            currentGameThread.Abort();
        }

        public Result RobotPlay(MainWindow.AIOption aiOption, MainWindow.DifficultyOption difficultyOption)
        {
            Result r = Result.Invalid;
            char[,] bn_old = TheBoard.board;
            char[,] bn_new = TheBoard.board;
            string strGameBoard = "";
            char isCheater = '-';

            //Decide next move
            switch (_selectedAI)
            {
                case MainWindow.AIOption.Random:
                    iNextMove = MainWindow._gameLogic.getNextMove_RANDOM(TheBoard.board, (char)Player.Player1);
                    break;
                case MainWindow.AIOption.Defensive:
                    iNextMove = MainWindow._gameLogic.getNextMove_DEFENSIVE(TheBoard.board, (char)Player.Player1);
                    break;
                case MainWindow.AIOption.MINMAX:
                    iNextMove = MainWindow._gameLogic.getNextMove_MINMAX(TheBoard.board, (char)Player.Player1, difficultyOption);

                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        lblStatsVal.Text = MainWindow._gameLogic.totalNodes.ToString("N0");
                        Score1.Text = MainWindow._gameLogic.scores[0].ToString();
                        Score2.Text = MainWindow._gameLogic.scores[1].ToString();
                        Score3.Text = MainWindow._gameLogic.scores[2].ToString();
                        Score4.Text = MainWindow._gameLogic.scores[3].ToString();
                        Score5.Text = MainWindow._gameLogic.scores[4].ToString();
                        Score6.Text = MainWindow._gameLogic.scores[5].ToString();
                        Score7.Text = MainWindow._gameLogic.scores[6].ToString();
                    });
                    break;
                case MainWindow.AIOption.Hybrid:
                    iNextMove = MainWindow._gameLogic.getNextMove_HYBRID(TheBoard.board, (char)Player.Player1, difficultyOption);

                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        lblStatsVal.Text = MainWindow._gameLogic.totalNodes.ToString("N0");
                        Score1.Text = MainWindow._gameLogic.scores[0].ToString();
                        Score2.Text = MainWindow._gameLogic.scores[1].ToString();
                        Score3.Text = MainWindow._gameLogic.scores[2].ToString();
                        Score4.Text = MainWindow._gameLogic.scores[3].ToString();
                        Score5.Text = MainWindow._gameLogic.scores[4].ToString();
                        Score6.Text = MainWindow._gameLogic.scores[5].ToString();
                        Score7.Text = MainWindow._gameLogic.scores[6].ToString();
                    });
                    break;
                case MainWindow.AIOption.Monte_Carlo:
                    iNextMove = MainWindow._gameLogic.getNextMove_MC(TheBoard.board, (char)Player.Player1, difficultyOption);

                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        lblStatsVal.Text = MainWindow._gameLogic.totalNodes.ToString("N0");
                        Score1.Text = MainWindow._gameLogic.scores[0].ToString();
                        Score2.Text = MainWindow._gameLogic.scores[1].ToString();
                        Score3.Text = MainWindow._gameLogic.scores[2].ToString();
                        Score4.Text = MainWindow._gameLogic.scores[3].ToString();
                        Score5.Text = MainWindow._gameLogic.scores[4].ToString();
                        Score6.Text = MainWindow._gameLogic.scores[5].ToString();
                        Score7.Text = MainWindow._gameLogic.scores[6].ToString();
                    });
                    break;

                case MainWindow.AIOption.Q_Learning:
                    iNextMove = MainWindow._gameLogic.getNextMove_QLearning(TheBoard);

                    moveCounter++;
                    StateAction sa = new StateAction(TheBoard, iNextMove);
                    MainWindow._gameLogic.MoveHistory.Add(sa);

                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Score1.Text = MainWindow._gameLogic.scores[0].ToString();
                        Score2.Text = MainWindow._gameLogic.scores[1].ToString();
                        Score3.Text = MainWindow._gameLogic.scores[2].ToString();
                        Score4.Text = MainWindow._gameLogic.scores[3].ToString();
                        Score5.Text = MainWindow._gameLogic.scores[4].ToString();
                        Score6.Text = MainWindow._gameLogic.scores[5].ToString();
                        Score7.Text = MainWindow._gameLogic.scores[6].ToString();
                    });
                    break;
            }
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                lblNextMove.Text = iNextMove.ToString();
            });

            Thread.Sleep(100);  //let the ball settle

            // Robot Aim and Fire!
            MainWindow._robotControl.AimFire_Retry(iNextMove.ToString());

            Thread.Sleep(1000);

            //Wait for the move
            while (!MainWindow._gameLogic.isMoveMade(bn_new, bn_old, totalMoves))   //Wait until the board changes
            {
                    strGameBoard = MainWindow._gameBoardControl.GetBoard_Retry();

                    if (strGameBoard[0] != '\0')
                    {
                        bn_new = MainWindow._gameLogic.updateBoard(strGameBoard);
                    }
                    if (MainWindow._gameLogic.isBoardError(bn_new))
                        bn_new = bn_old;  //refresh
            }

            isCheater = MainWindow._gameLogic.isCheating(bn_new, (char)Player.Player1, iFirstPlayer);
            if (isCheater != '-')  //Check if too many of one colour or last played is wrong colour
            {
                //Game over - cheating
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    pgGameOver frmGameOver = new pgGameOver(Result.Invalid, (isCheater == 'B') ? Player.Player1 : Player.Player2, 0, 0, _selectedAI);
                    this.NavigationService.Navigate(frmGameOver);
                });

                currentGameThread.Abort();
            }

            TheBoard.board = bn_new;

            if (MainWindow._gameLogic.areFourConnected(TheBoard.board, (char)Player.Player1))
            {
                r = Result.Win;
                if (_selectedAI == MainWindow.AIOption.Q_Learning)
                {
                    MainWindow._gameLogic.updateQ(r, TheBoard);
                    MainWindow._gameLogic.SaveQs(_selectedDifficulty);   //Why is this executing?
                }
            }

            if (MainWindow._gameLogic.isTie(TheBoard.board))
            {
                r = Result.Tie;
                if (_selectedAI == MainWindow.AIOption.Q_Learning)
                {
                    MainWindow._gameLogic.updateQ(r, TheBoard);
                    MainWindow._gameLogic.SaveQs(_selectedDifficulty);
                }
            }

            if (r != Result.Win && r != Result.Tie)
                MainWindow._gameBoardControl.LightOn_Retry();  //Refresh incase COM port reset

            return r;
        }

        public Result HumanPlay(Player player)
        {
            Result r = Result.Invalid;
            char[,] bn_old = TheBoard.board;
            char[,] bn_new = TheBoard.board;
            string strGameBoard = "";


            while (!MainWindow._gameLogic.isMoveMade(bn_new, bn_old, totalMoves))   //Wait until the board changes
            {
                    strGameBoard = MainWindow._gameBoardControl.GetBoard_Retry();
                    if (strGameBoard[0] != '\0')
                    {
                        bn_new = MainWindow._gameLogic.updateBoard(strGameBoard);
                    }
                    if (MainWindow._gameLogic.isBoardError(bn_new))
                        bn_new = bn_old;  //refresh
            }
            //Thread.Sleep(1000);  //let the ball settle

            char isCheater = MainWindow._gameLogic.isCheating(bn_new, (char)player, iFirstPlayer);
            if (isCheater != '-')  //Check if too many of one colour or last played is wrong colour
            {
                //Game over - cheating
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    pgGameOver frmGameOver = new pgGameOver(Result.Invalid, (isCheater == 'B') ? Player.Player1 : Player.Player2, 0, 0, _selectedAI);
                    this.NavigationService.Navigate(frmGameOver);

                });

                currentGameThread.Abort();
            }

            TheBoard.board = bn_new;

            if (MainWindow._gameLogic.areFourConnected(TheBoard.board, (char)player))
            {
                r = Result.Win;
                if (_selectedAI == MainWindow.AIOption.Q_Learning)
                {
                    MainWindow._gameLogic.updateQ(r, TheBoard);
                    MainWindow._gameLogic.SaveQs(_selectedDifficulty);
                }
            }

            if (MainWindow._gameLogic.isTie(TheBoard.board))
            {
                r = Result.Tie;
                if (_selectedAI == MainWindow.AIOption.Q_Learning)
                {
                    MainWindow._gameLogic.updateQ(r, TheBoard);
                    MainWindow._gameLogic.SaveQs(_selectedDifficulty);
                }
            }

            if (r != Result.Win && r != Result.Tie)
                MainWindow._gameBoardControl.LightOn_Retry();  //Refresh incase COM port reset

            return r;
        }

        #endregion

        #region BUTTONS
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            HurryTimer.Stop();
            Question frmQuestion = new Question(_selectedAI);
            frmQuestion.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmQuestion.ShowDialog();  //Waits for a response

            HurryTimer.Start();
            HurryTimer.Interval = new TimeSpan(0, 2, 0);
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                pgGameOver frmGameOver = new pgGameOver(Result.Intermediate, currentPlayer, 0, 0, _selectedAI);
                this.NavigationService.Navigate(frmGameOver);
            });

            currentGameThread.Abort();
        }

        private void btnExtraBall_Click(object sender, RoutedEventArgs e)
        {

            if (_selectedAI == MainWindow.AIOption.TwoPlayer)
            {
                if (currentPlayer == Player.Player1)
                {
                    MainWindow._ballDispenserControl.BlueDispense_Retry();
                }
                else
                {
                    MainWindow._ballDispenserControl.RedDispense_Retry();
                }
                _speak.speakAnotherBall(currentPlayer);
            }
            else
            {
                if (currentPlayer == Player.Player1)
                {
                    //Robot aim and fire again
                    MainWindow._robotControl.AimFire_Retry(iNextMove.ToString());
                }
                else
                {
                    MainWindow._ballDispenserControl.RedDispense_Retry();
                    _speak.speakAnotherBall(currentPlayer);
                }
            }
            btnExtraBall.IsEnabled = false;
            ExtraBallTimer.Start();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            HurryTimer.Stop();

            AdminPassword frmAdmin = new AdminPassword();
            frmAdmin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frmAdmin.ShowDialog();  //Waits for a response

            HurryTimer.Start();
            HurryTimer.Interval = new TimeSpan(0, 2, 0);
        }

        #endregion


    }
}
