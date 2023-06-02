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
    /// Interaction logic for Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        System.Windows.Threading.DispatcherTimer navigationTimer = new System.Windows.Threading.DispatcherTimer();
        MainWindow.AIOption aiOption;

        public Info(MainWindow.AIOption _aiOption)
        {
            InitializeComponent();
            aiOption = _aiOption;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            navigationTimer.Tick += navigationTimer_Tick;
            navigationTimer.Interval = new TimeSpan(0, 2, 0);
            navigationTimer.Start();

            switch (aiOption)
            {
                case MainWindow.AIOption.MINMAX:
                    lblAIDescription.Text = "MINMAX is a recursive algorythm that explores every possible move for the curernt board.  The number of moves ahead (Depth) is limited by the DIFFICULTY level you slected when you started the game (5 - 7 moves ahead).";
                    lblScoreDescription.Text = "The numbers below each column represent scores that resulted from the MINMAX algorythm.  The number is based on the maximum number of moves that a player can make (21). The highest number is selected for the next move.  In the event of a tie, a random choice of the best options is selected. \n A POSITIVE VALUE:  21 - # moves befor a possible win.  \n A NEGATIVE VALUE: # moves befor a possible loss - 21, or -100 for full row. \n A ZERO VALUE:  Tie, or Could not determine outcome within depth limit. \n EXAMPLE:  A score of -21 means the opponent could win on the next move if the robot chooses this column.";
                    lblStatDescription.Text = "The 'Nodes' value on the right shows the total number of moves explored to decide next move.";
                    break;
                case MainWindow.AIOption.Hybrid:
                    lblAIDescription.Text = "HYBRID is combination of MINMAX, DEFENSIVE and RANDOM.  MINMAX is a recursive algorythm that explores every possible move for the curernt board.  The number of moves ahead (Depth) is limited by the DIFFICULTY level you slected when you started the game (5 - 7 moves ahead).";
                    lblScoreDescription.Text = "The numbers below each column represent scores that resulted from the MINMAX algorythm.  The number is based on the maximum number of moves that a player can make (21). The highest number is selected for the next move.  In the event of a tie, a random choice of the best options is selected.\n A POSITIVE VALUE:  21 - # moves befor a possible win.  \n A NEGATIVE VALUE: # moves befor a possible loss - 21, or -100 for full row. \n A ZERO VALUE:  Tie, or Could not determine outcome within depth limit. \n EXAMPLE:  A score of -21 means the opponent could win on the next move if the robot chooses this column.";
                    lblStatDescription.Text = "The 'Nodes' value on the right shows the total number of moves explored to decide next move.";
                    break;
                case MainWindow.AIOption.Monte_Carlo:
                    lblAIDescription.Text = "MONTE CARLO is a recursive algorythm that runs thousands of random games to completion based on the current board.  The number of games played for each column is limited by the DIFFICULTY level you selected when you started the game (2000 - 6000+ games).";
                    lblScoreDescription.Text = "The numbers below each column represent scores that resulted from the conclusion of each random game starting in that column.  The column with the highest value is selected as the next move. \n A WIN RESULT:  +1 to the column total. \n A LOSS RESULT:  -1 to the column total. \n A TIE RESULT:  No change to the column total. \n A VALUE OF -6000:  Column is full.  \n EXAMPLE:  A column value of 3564 with a HIGH DIFFICULTY selected (6000 simulated games) means a high probability of a winning outcome for a move in this column.";
                    lblStatDescription.Text = "The 'Sim Games' value on the right shows the total number of simulated games explored to decide next move.";
                    break;
                case MainWindow.AIOption.Q_Learning:
                    lblAIDescription.Text = "Q LEARNING uses positive and negative reinformcement techniques to learn effective strategies.  The algorythm improves with experience and can never be beaten the same way twice.  The amount of experience is based on the DIFFICULTY level you selected when you started the game (100K - 300+K games).";
                    lblScoreDescription.Text = "The numbers below each column are the 'Q' value.  The results of every game are recorded.  Q point values are recorded in a large in-memory dictionary.  If there is no experience for a move option, a random Q number is generated.  The column with the highest value is selected as the next move. \n A WIN RESULT:  A large, positive point award is added to the winning move.  A small, positive point reward is also added to the records for the other 3 moves that allowed the win.  \n A LOSS RESULT:  A large negative point award is added to the losing move record.  \n A TIE RESULT:  No point adjustment.";
                    lblStatDescription.Text = "The 'Learning Ops' value on the right shows the total number of records in the in-memory dictionary.";
                    break;
            }

        }

        private void navigationTimer_Tick(object sender, EventArgs e)
        {
            navigationTimer.Stop();
            this.Close();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            navigationTimer.Stop();
            this.Close();
        }
    }
}
