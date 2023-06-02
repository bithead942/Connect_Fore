using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using ConnectFore;
using System.IO;

namespace Connect
{
    public class GameLogic
    {
        public static int _boardCols = 7;
        public static int _boardRows = 6;
        public int startcol = 0;
        public int numcols = 0;
        public int totalNodes = 0;
        int maxDepth = 0;
        int bestScoreR = 0;
        int bestScoreB = 0;
        int numNodes = 0;
        public int[] scores = new int[_boardCols];
        int[] nodes = new int[_boardCols];
        Random r = new Random();
        public Dictionary<StateAction, double> Q_100K = new Dictionary<StateAction, double>();
        public Dictionary<StateAction, double> Q_200K = new Dictionary<StateAction, double>();
        public Dictionary<StateAction, double> Q_300K = new Dictionary<StateAction, double>();
        public Dictionary<StateAction, double> Q = new Dictionary<StateAction, double>();
        public List<StateAction> MoveHistory = new List<StateAction>();


        public GameLogic()  //Constructor
        {

        }

        #region COMMON
        public char[,] updateBoard(string jsonString)
        {
            char[,] board = new char[_boardRows, _boardCols];

            try
            {
                dynamic a = JsonConvert.DeserializeObject<dynamic>(jsonString);
                for (int i = 0; i < _boardRows; i++)
                {
                    for (int j = 0; j < _boardCols; j++)
                    {
                        board[i, j] = a.GameBoard.rows[i][j];
                        // 0,0 is upper left
                    }
                }
                return board;
            }
            catch
            {
                return board;
            }

        }

        public bool areFourConnected(char[,] board, char player)
        {
            // horizontalCheck 
            for (int i = 0; i < _boardRows; i++)
            {
                for (int j = 0; j < _boardCols - 3; j++)
                {
                    if (board[i, j] == player && board[i, j + 1] == player && board[i, j + 2] == player && board[i, j + 3] == player)
                    {
                        startcol = j + 1;
                        numcols = 4;
                        return true;
                    }
                }
            }
            // verticalCheck
            for (int i = 3; i < _boardRows; i++)
            {
                for (int j = 0; j < _boardCols; j++)
                {
                    if (board[i, j] == player && board[i - 1, j] == player && board[i - 2, j] == player && board[i - 3, j] == player)
                    {
                        startcol = j + 1;
                        numcols = 1;
                        return true;
                    }
                }
            }
            // desendingDiagonalCheck   "\"
            for (int i = 0; i < _boardRows - 3; i++)
            {
                for (int j = 0; j < _boardCols - 3; j++)
                {
                    if (board[i, j] == player && board[i + 1, j + 1] == player && board[i + 2, j + 2] == player && board[i + 3, j + 3] == player)
                    {
                        startcol = j + 1;
                        numcols = 4;
                        return true;
                    }
                }
            }
            // ascendingDiagonalCheck  "/"
            for (int i = 3; i < _boardRows; i++)
            {
                for (int j = 0; j < _boardCols - 3; j++)
                {
                    if (board[i, j] == player && board[i - 1, j + 1] == player && board[i - 2, j + 2] == player && board[i - 3, j + 3] == player)
                    {
                        startcol = j + 1;
                        numcols = 4;
                        return true;
                    }
                }
            }
            return false;
        }

        public char isCheating(char[,] board, char player, int iFirstPlayer)
        {
            int countRed = 0;
            int countBlue = 0;

            for (int i = 0; i < _boardRows; i++)
            {
                for (int j = 0; j < _boardCols; j++)
                {
                    if (board[i, j] == 'R')
                    {
                        countRed++;
                    }
                    if (board[i, j] == 'B')
                    {
                        countBlue++;
                    }
                }
            }
            if (iFirstPlayer == 2)  //Red went first
            {
                if (player == 'B')
                {
                    if (countBlue - countRed >= 1)
                        return 'B';
                }
                else
                {
                    if (countRed - countBlue >= 2)
                        return 'R';
                }
            }
            else  //Blue went first
            {
                if (player == 'B')
                {
                    if (countBlue - countRed >= 2)
                        return 'B';
                }
                else
                {
                    if (countRed - countBlue >= 1)
                        return 'R';
                }
            }

            return '-';
        }

        public bool isBoardError(char[,] board)
        {
            //check for vertical space between balls

            for (int i = 0; i < _boardRows - 1; i++)
            {
                for (int j = 0; j < _boardCols; j++)
                {
                    if ((board[i, j] == 'R' || board[i, j] == 'B') && board[i + 1, j] == '-')
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool isTie(char[,] board)
        {
            if (!areFourConnected(board, 'B') && !areFourConnected(board, 'R'))
            {
                for (int i = 0; i < _boardCols; i++)
                {
                    if (board[0, i] == '-')  // if any of the top row is still avaialble, still moves avaialble
                    {
                        return false;
                    }
                }
                return true;  //it's a tie
            }
            return false;  //someone has won
        }

        bool canPlay(char[,] board, int col)
        {
            if (board[0, col] == '-')
                return true;
            else
                return false;
        }

        //makePlay is not used in this version
        char[,] makePlay(char[,] board, int col, char player)
        {
            char[,] newBoard = (char[,])board.Clone();

            for (int i = _boardRows - 1; i >= 0; i--)   //for the selected column, start at the bottom and find the first avaialble spot to play
            {
                if (newBoard[i, col] == '-')
                {
                    newBoard[i, col] = player;
                    break;
                }
            }

            return newBoard;
        }

        void clearScores()
        {
            for (int i = 0; i < _boardCols; i++)
            {
                scores[i] = 0;
                nodes[i] = 0;
            }
        }

        public bool isMoveMade(char[,] new_board, char[,] old_board, int totalMoves)
        {
            int moveCounter = 0;
            bool boardDifferent = false;

            for (int i = 0; i < _boardRows; i++)
            {
                for (int j = 0; j < _boardCols; j++)
                {
                    if (new_board[i, j] != '-')  //Add the total number 'R' and 'B'
                    {
                        moveCounter++;
                    }    
                    if (new_board[i, j] != old_board[i, j])
                    {
                        boardDifferent = true;  //a move was made
                    }
                }
            }
            if (boardDifferent && (moveCounter > totalMoves))
                return true;  //move made - board is different and a new ball is detected
            else
                return false;  //No move made, all cells are the same
        }

        //lastPlayedColour - logic covered by isCheating, so not called
        public char lastPlayedColour(char[,] new_board, char[,] old_board)
        {
            for (int i = 0; i < _boardRows; i++)
            {
                for (int j = 0; j < _boardCols; j++)
                {
                    if (new_board[i, j] != old_board[i, j])
                    {
                        return new_board[i, j];
                    }
                }
            }
            return '-';  //No move made, all cells are the same
        }

        #endregion

        #region MINMAX
        /*******************************************
         * Solving Connect 4 can been seen as finding the best path in a decision tree where each node 
         * is a Position. At each node player has to choose one move leading to one of the possible next 
         * positions. When it is your turn, you want to choose the best possible move that will maximize 
         * your score. But next turn your opponent will try himself to maximize his score, thus minimizing 
         * yours.
         *
         * This leads to a reccursive algorithm to score a position. At each step:
         *    - When it’s your turn, the score is the maximum score of any of the next possible positions 
         *    (you will play the move that maximizes your score),
         *    - While when it’s your opponent’s turn, the score is the minimum score of next possible 
         *    positions (your opponent will play the move that minimizes your score, and maximizes his).
         *    - Final positions (draw game after 42 moves or position with a winning alignment) get a score 
         *    according to our score function.
         *    
         * In practice exploring the full tree is most of the time untractable due to exponential growth 
         * of tree size with search depth. Most AI implementation explore the tree up to a given depth and 
         * use heuristic score functions that evaluate these non final positions. 
         ******************************************/

        public int getNextMove_MINMAX(char[,] board, char player, MainWindow.DifficultyOption difficultyOption)
        {
            int bestScore = 0;
            int[] bestCols = new int[7];
            int bestColsCounter = 0;

            clearScores();

            switch (difficultyOption)
            {
                case MainWindow.DifficultyOption.High:
                    maxDepth = 7;
                    break;
                case MainWindow.DifficultyOption.Medium:
                    maxDepth = 6;
                    break;
                case MainWindow.DifficultyOption.Low:
                    maxDepth = 5;
                    break;
                default:
                    maxDepth = 5;
                    break;
            }

            MINMAX(board, 0, 0, player);

            bestScore = Math.Abs(scores[0]);
            for (int i = 0; i < _boardCols; i++)
            {
                if (Math.Abs(scores[i]) >= bestScore)
                {
                    if (Math.Abs(scores[i]) == bestScore)
                    {
                        //add it to the list of columns tied for best move
                        bestCols[bestColsCounter] = i;
                        bestColsCounter++;
                    }
                    else
                    {
                        //clear the list and start over
                        for (int j = 0; j < _boardCols; j++)
                        {
                            bestCols[j] = 0;
                        }
                        bestCols[0] = i;
                        bestColsCounter = 1;
                    }
                    bestScore = Math.Abs(scores[i]);
                }
            }

            totalNodes = nodes.Sum();

            if (bestColsCounter > 1)
            {
                //there is a tie, randomly pick
                Random r = new Random();
                int rInt = r.Next(0, bestColsCounter - 1);
                return bestCols[rInt] + 1; //Humans start counting at 1
            }
            else
            {
                //only one column to pick;
                return bestCols[0] + 1; //Humans start counting at 1
            }
        }

        int MINMAX(char[,] board, int blueMoves, int redMoves, char player)
        {
            char newPlayer;
            char[,] newBoard = new char[_boardRows, _boardCols];
            int score = 0;

            //Check if Blue wins
            if (areFourConnected(board, 'B'))
                return 22 - blueMoves;
            //Check if Red wins
            if (areFourConnected(board, 'R'))
                return redMoves - 22;
            //Check if Tie
            if (isTie(board))
                return 0;
            //Check Max Depth
            if ((blueMoves + redMoves) >= maxDepth)
                return 0;

            if (player == 'B')
            {
                newPlayer = 'R';
                redMoves++;
            }
            else
            {
                newPlayer = 'B';
                blueMoves++;
            }

            for (int col = 0; col < _boardCols; col++)
            {
                if (canPlay(board, col))
                {
                    newBoard = makePlay(board, col, newPlayer);
                    score = MINMAX(newBoard, blueMoves, redMoves, newPlayer);
                    numNodes++;
                    if (newPlayer == 'B')
                    {
                        bestScoreB = Math.Max(bestScoreB, score);  // Optimize for Blue win
                    }
                    else
                    {
                        bestScoreR = Math.Min(bestScoreR, score);  // Optimize for Red loss
                    }
                }
                if (blueMoves + redMoves == 1)  //top of stack
                {
                    if (canPlay(board, col))
                    {
                        if (bestScoreB >= Math.Abs(bestScoreR))
                            scores[col] = bestScoreB;
                        else
                            scores[col] = bestScoreR;
                    }
                    else
                    {
                        scores[col] = 0;   //column is full, so it gets the lowest score possible
                    }
                    nodes[col] = numNodes;
                    numNodes = 0;
                    bestScoreB = 0;
                    bestScoreR = 0;
                }
            }

            if ((blueMoves + redMoves) % 2 == 0)  //modulus to check remainder
            {
                return bestScoreB;
            }
            else
            {
                return bestScoreR;
            }

        }


        #endregion

        #region RANDOM
        /*******************************************
        * Randomly selects a column for the next move.   
        ******************************************/
        public int getNextMove_RANDOM(char[,] board, char player)
        {
            int nextMove = r.Next(0, _boardCols);   // returns a value >= 0 and < _boardCols
            while (!canPlay(board, nextMove))  //keep trying until you get a valid move
            {
                nextMove = r.Next(0, _boardCols);
            }

            return nextMove + 1;  //Humans start counting at 1
        }

        #endregion

        #region DEFENSIVE
        /*******************************************
        * The idea is to anticipate and avoid exploring very bad moves allowing the opponent to win directly at the next turn. 
        * Follow 3 rules:
        *   1.  If I can win in the next move, make the move
        *   2.  If my oppnent can win in the next move, make the block
        *   3.  If my move will allow my opponent to win, don't make that move
        ******************************************/
        public int getNextMove_DEFENSIVE(char[,] board, char player)
        {
            int[] winningCols = new int[7];
            int winningColsCounter = 0;
            int bestScore = 0;
            int[] bestCols = new int[7];
            int bestColsCounter = 0;

            clearScores();

            // Check if I can win
            winningColsCounter = canPlayerWin(board, player, winningCols);
            totalNodes = 7;
            if (winningColsCounter > 0)
            {
                //Make the move to win.  If there is more than one winning move, pick the first one - it doesn't matter
                return winningCols[0] + 1; //Humans start counting at 1
            }

            //Check if opponent can win in next move
            char newPlayer = ' ';
            if (player == 'B')
                newPlayer = 'R';
            else
                newPlayer = 'B';

            winningColsCounter = canPlayerWin(board, newPlayer, winningCols);
            totalNodes = totalNodes + 7;
            if (winningColsCounter > 0)
            {
                //Make the move to block.  If there is more than one place to block, pick the first one - it doesn't matter
                return winningCols[0] + 1; //Humans start counting at 1
            }


            //Check if my move will allow opponent to immediately win
            for (int i = 0; i < _boardCols; i++)
            {
                //Try each column.  Test to see if opponent can win after my move.
                char[,] newBoard = (char[,])board.Clone();
                newBoard = makePlay(board, i, player);
                winningColsCounter = canPlayerWin(newBoard, newPlayer, winningCols);
                if (winningColsCounter > 0)
                {
                    scores[i] = -1;
                }
                else
                {
                    scores[i] = 1;
                }
                totalNodes = totalNodes + 7;
            }

            //Get the list of the best scores
            bestScore = scores[0];
            for (int i = 0; i < _boardCols; i++)
            {
                if (scores[i] >= bestScore)
                {
                    if (scores[i] == bestScore)
                    {
                        //add it to the list of columns tied for best move
                        bestCols[bestColsCounter] = i;
                        bestColsCounter++;
                    }
                    else
                    {
                        //clear the list and start over
                        for (int j = 0; j < _boardCols; j++)
                        {
                            bestCols[j] = 0;
                        }
                        bestCols[0] = i;
                        bestColsCounter = 1;
                    }
                    bestScore = scores[i];
                }
            }

            if (bestColsCounter > 1)
            {
                //there is a tie, randomly pick
                Random r = new Random();
                int rInt = r.Next(0, bestColsCounter - 1);
                return bestCols[rInt] + 1; //Humans start counting at 1
            }
            else
            {
                //only one column to pick;
                return bestCols[0] + 1; //Humans start counting at 1
            }
        }

        int canPlayerWin(char[,] board, char player, int[] winningCols)
        {
            int winningColsCounter = 0;

            for (int i = 0; i < _boardCols; i++)
            {
                //Try each column and determine if any result in a win
                char[,] newBoard = (char[,])board.Clone();
                newBoard = makePlay(board, i, player);
                if (areFourConnected(newBoard, player))
                {
                    winningCols[winningColsCounter] = i;
                    winningColsCounter++;
                }

            }
            return winningColsCounter;
        }

        #endregion

        #region HYBRID
        /*******************************************
         * Hybrid combines the strenths of MINMAX, ANTICIPATE and RANDOM
         * Also detects horizontal double attack
         * Also takes center position on first move;
         ******************************************/

        public int getNextMove_HYBRID(char[,] board, char player, MainWindow.DifficultyOption difficultyOption)
        {
            int[] winningCols = new int[7];
            int winningColsCounter = 0;
            int bestScore = 0;
            int[] bestCols = new int[7];
            int bestColsCounter = 0;

            clearScores();

            switch (difficultyOption)
            {
                case MainWindow.DifficultyOption.High:
                    maxDepth = 7;
                    break;
                case MainWindow.DifficultyOption.Medium:
                    maxDepth = 6;
                    break;
                case MainWindow.DifficultyOption.Low:
                    maxDepth = 5;
                    break;
                default:
                    maxDepth = 5;
                    break;
            }

            // Check if I can win
            winningColsCounter = canPlayerWin(board, player, winningCols);
            totalNodes = 7;
            if (winningColsCounter > 0)
            {
                //Make the move to win.  If there is more than one winning move, pick the first one - it doesn't matter
                return winningCols[0] + 1; //Humans start counting at 1
            }

            //Check if opponent can win in next move
            char newPlayer = ' ';
            if (player == 'B')
                newPlayer = 'R';
            else
                newPlayer = 'B';

            winningColsCounter = canPlayerWin(board, newPlayer, winningCols);
            totalNodes = totalNodes + 7;
            if (winningColsCounter > 0)
            {
                //Make the move to block.  If there is more than one place to block, pick the first one - it doesn't matter
                return winningCols[0] + 1; //Humans start counting at 1
            }

            //Detect double attack
            int nextMove = 0;
            if (doubleAttack(board, player, ref nextMove))
            {
                return nextMove + 1; //Humans start counting at 1;
            }

            //Check first move
            if (isFirstMove(board) || board[5, 3] == '-')
            {
                return 4; //Always take middle position on first move
            }

            //Check if my move will allow opponent to immediately win
            for (int i = 0; i < _boardCols; i++)
            {
                //Try each column.  Test to see if opponent can win after my move.
                char[,] newBoard = (char[,])board.Clone();
                newBoard = makePlay(board, i, player);
                winningColsCounter = canPlayerWin(newBoard, newPlayer, winningCols);
                if (winningColsCounter > 0)
                {
                    scores[i] = -50;  //very bad move
                }
                totalNodes = totalNodes + 7;
            }

            //Now apply MINMAX
            MINMAX(board, 0, 0, player);

            bestScore = scores[0];
            for (int i = 0; i < _boardCols; i++)
            {
                if (scores[i] >= bestScore)
                {
                    if (scores[i] == bestScore)
                    {
                        //add it to the list of columns tied for best move
                        bestCols[bestColsCounter] = i;
                        bestColsCounter++;
                    }
                    else
                    {
                        //clear the list and start over
                        for (int j = 0; j < _boardCols; j++)
                        {
                            bestCols[j] = 0;
                        }
                        bestCols[0] = i;
                        bestColsCounter = 1;
                    }
                    bestScore = scores[i];
                }
            }

            totalNodes = totalNodes + nodes.Sum();

            if (bestColsCounter > 1)
            {
                //there is a tie, randomly pick
                Random r = new Random();
                int rInt = r.Next(0, bestColsCounter - 1);
                return bestCols[rInt] + 1; //Humans start counting at 1
            }
            else
            {
                //only one column to pick;
                return bestCols[0] + 1; //Humans start counting at 1
            }
        }

        bool doubleAttack(char[,] board, char player, ref int nextMove)
        {
            char newPlayer = ' ';
            if (player == 'B')
                newPlayer = 'R';
            else
                newPlayer = 'B';

            for (int i = 0; i < _boardCols - 3; i++)
            {
                totalNodes = totalNodes + 1;
                if (board[5, i] == '-' && board[5, i + 1] == newPlayer && board[5, i + 2] == newPlayer && board[5, i + 3] == '-')
                {
                    nextMove = i;
                    return true;
                }

                /*for (int j = 4; j >= 0; j--)
                {
                    if ((board[j, i] == '-' && board[j + 1, i] != '-') && (board[j, i + 1] == newPlayer && board[j + 1, i +1] != '-') && (board[j, i + 2] == newPlayer && board[j + 1, i+2] != '-') && (board[j, i + 3] == '-' && board[j + 1, i+3] != '-'))
                    {
                        nextMove = i;
                        totalNodes = totalNodes + 1;
                        return true;
                    }
                }*/
            }
            return false;
        }

        bool isFirstMove(char[,] board)
        {
            totalNodes = totalNodes + 1;

            for (int i = 0; i < _boardCols; i++)
            {
                if (board[5, i] != '-')
                    return false;
            }
            return true;
        }

        #endregion

        #region MONTE CARLO
        /*******************************************
        * Monte Carlo plays many random games from a given starting position to determine a probable outcome
        * based on the "Law of Large Numbers (LLN)"
        ******************************************/

        public int getNextMove_MC(char[,] board, char player, MainWindow.DifficultyOption difficultyOption)
        {
            char[,] newBoard = new char[_boardRows, _boardCols];
            int score = 0;
            int bestScore = 0;
            int[] bestCols = new int[7];
            int bestColsCounter = 0;

            clearScores();

            switch (difficultyOption)
            {
                case MainWindow.DifficultyOption.High:
                    maxDepth = 2000;
                    break;
                case MainWindow.DifficultyOption.Medium:
                    maxDepth = 4000;
                    break;
                case MainWindow.DifficultyOption.Low:
                    maxDepth = 6300;
                    break;
                default:
                    maxDepth = 2000;
                    break;
            }

            for (int col = 0; col < _boardCols; col++) //try a move in each column
            {
                if (canPlay(board, col))
                {
                    for (int j = 0; j < maxDepth; j++)  //play lots of games starting in a given column until the end
                    {
                        newBoard = makePlay(board, col, player);
                        score = MONTE_CARLO(newBoard, player);
                        scores[col] = scores[col] + score;
                        nodes[col]++;
                    }
                }
                else
                {
                    scores[col] = -6000;  //Column is full
                }
            }

            bestScore = scores[0];
            for (int i = 0; i < _boardCols; i++)
            {
                if (scores[i] >= bestScore)
                {
                    if (scores[i] == bestScore)
                    {
                        //add it to the list of columns tied for best move
                        bestCols[bestColsCounter] = i;
                        bestColsCounter++;
                    }
                    else
                    {
                        //clear the list and start over
                        for (int j = 0; j < _boardCols; j++)
                        {
                            bestCols[j] = 0;
                        }
                        bestCols[0] = i;
                        bestColsCounter = 1;
                    }
                    bestScore = scores[i];
                }
            }

            totalNodes = nodes.Sum();

            if (bestColsCounter > 1)
            {
                //there is a tie, randomly pick
                Random r = new Random();
                int rInt = r.Next(0, bestColsCounter - 1);
                return bestCols[rInt] + 1; //Humans start counting at 1
            }
            else
            {
                //only one column to pick;
                return bestCols[0] + 1; //Humans start counting at 1
            }
        }


        int MONTE_CARLO(char[,] board, char player)  //Play 1 game using random moves, until the end
        {
            char newPlayer;
            char[,] newBoard = new char[_boardRows, _boardCols];
            int score = 0;
            int nextMove = 0;

            //Check if Blue wins
            if (areFourConnected(board, 'B'))
                return 1;
            //Check if Red wins
            if (areFourConnected(board, 'R'))
                return -1;
            //Check if Tie
            if (isTie(board))
                return 0;

            if (player == 'B')
            {
                newPlayer = 'R';
            }
            else
            {
                newPlayer = 'B';
            }

            nextMove = r.Next(0, _boardCols);   // returns a value >= 0 and < _boardCols
            while (!canPlay(board, nextMove))  //keep trying until you get a valid move
            {
                nextMove = r.Next(0, _boardCols);
            }
            newBoard = makePlay(board, nextMove, newPlayer);  //make the move
            score = MONTE_CARLO(newBoard, newPlayer);  //call self
            newBoard = null;
            return score;
        }

        #endregion

        #region Q_LEARNING
        public int getNextMove_QLearning(BoardState b)
        {
            int bestCol = -1;
            double bestWeight = -1;
            double value = 0;
            double weight = 0;

            for (int i = 0; i < b.cols; ++i)
            {
                if (canPlay(b.board, i))
                {
                    StateAction sa = new StateAction(b, i);
                    if (Q.TryGetValue(sa, out value))
                    {
                        weight = value;
                    }
                    else
                    {
                        weight = random_value();
                    }
                    scores[i] = (int)(weight * 1000);
                }
                else
                {
                    scores[i] = 0;  //can't play
                }
                if (weight > bestWeight)
                {
                    bestCol = i;
                    bestWeight = weight;
                }
            }

            return bestCol + 1;  //Humans start counting at 1
        }

        public int random_action(BoardState b)
        {
            return r.Next(0, b.cols);

        }

        public double random_value()
        {
            return r.NextDouble() * (0.1);
        }

        public void updateQ(Result r, BoardState TheBoard)
        {
            double value = 0;
            int moveHistoryCounter = 0;
            bool QResult = true;  //set to initiate loop

            moveHistoryCounter = MoveHistory.Count;
            if (r == Result.Loss)  // on a loss, trace back the steps that caused the loss and punish earlier steps if the last step was already a learned step
            {
                while (QResult && (moveHistoryCounter > 0) && (value <= 0))  //Find the most recent COM move without learning
                {
                    moveHistoryCounter--;
                    QResult = Q.TryGetValue(MoveHistory[moveHistoryCounter], out value);  //true = learning already recorded
                }
                if (!QResult)  //new learning
                {
                    Q.Add(MoveHistory[moveHistoryCounter], -1);
                }
                if (value > 0)  //undo old learning
                {
                    Q.Remove(MoveHistory[moveHistoryCounter]);
                    Q.Add(MoveHistory[moveHistoryCounter], -1);
                }

            }
            else  //on a win or tie, give reward
            {
                if (r == Result.Win)  //big reward to last move, small reward for other 3 moves in the connected 4
                {
                    //Last move on the list created the win, so no need to search
                    moveHistoryCounter--;
                    QResult = Q.TryGetValue(MoveHistory[moveHistoryCounter], out value);  //true = learning already recorded
                    if (!QResult)
                    {
                        Q.Add(MoveHistory[moveHistoryCounter], 1); //Big reward since it was the winning move
                    }
                    else
                    {
                        Q.Remove(MoveHistory[moveHistoryCounter]);  //update
                        Q.Add(MoveHistory[moveHistoryCounter], value);
                    }
                    //search list of moves to finds the one that contributed to the win, then reward them
                    for (int i = moveHistoryCounter - 1; i >= 0; i--)   //work backward through the MoveHistory list
                    {
                        Tuple<int, int> moveCoord = getCoordinates(MoveHistory[i]);
                        foreach (Tuple<int, int> coord in TheBoard.winners)
                        {
                            if ((coord.Item1 == moveCoord.Item1) && (coord.Item2 == moveCoord.Item2))
                            {
                                QResult = Q.TryGetValue(MoveHistory[i], out value);

                                if (!QResult)
                                {
                                    Q.Add(MoveHistory[i], 0.1);  //add new
                                }
                                else
                                {
                                    Q.Remove(MoveHistory[i]);  //update
                                    value = value + 0.1;
                                    if (value > 1.0)
                                        value = 1.0;
                                    Q.Add(MoveHistory[i], value);
                                }
                                break;
                            }
                        }
                    }
                }

                if (r == Result.Tie)
                {
                    moveHistoryCounter--;
                    QResult = Q.TryGetValue(MoveHistory[moveHistoryCounter], out value);
                    if (!QResult)
                    {
                        Q.Add(MoveHistory[moveHistoryCounter], 0);  //add new
                    }
                    else
                    {
                        Q.Remove(MoveHistory[moveHistoryCounter]);  //update
                        Q.Add(MoveHistory[moveHistoryCounter], 0);
                    }
                }
            }
            MoveHistory.Clear();
        }

        public Tuple<int, int> getCoordinates(StateAction sa)
        {
            int i = 0;

            for (i = sa.state.rows - 1; i >= 0; i--)  //work from the bottom-up to find the first available space in the selected column
            {
                if (sa.state.board[i, sa.action] == '-')
                {
                    break;
                }
            }

            return Tuple.Create(i + 1, sa.action);  //sa already has the move recorded, so select the row below for the actual coord of the move
        }

        public void SwitchQs(MainWindow.DifficultyOption difficultyOption)
        {
            switch (difficultyOption)
            {
                case MainWindow.DifficultyOption.High:
                    Q = Q_300K;
                    break;
                case MainWindow.DifficultyOption.Medium:
                    Q = Q_200K;
                    break;
                case MainWindow.DifficultyOption.Low:
                    Q = Q_100K;
                    break;
                default:
                    Q = Q_100K;
                    break;
            }
        }

        #endregion

        #region FileIO

        public async void LoadQs(MainWindow.DifficultyOption difficultyOption)
        {
            string strFileName = "";

            switch (difficultyOption)
            {
                case MainWindow.DifficultyOption.High:
                    strFileName = "data/Q_300K.dat";
                    if (File.Exists(strFileName))
                    {
                        using (StreamReader sr = new StreamReader(strFileName))
                        {
                            while (!sr.EndOfStream)
                            {
                                string line = await sr.ReadLineAsync();
                                string[] split = line.Split(',');
                                int rows = _boardRows;
                                int cols = _boardCols;
                                BoardState bs = new BoardState(rows, cols);
                                int splitCounter = 1;
                                for (int a = 0; a < rows; a++)
                                {
                                    for (int b = 0; b < cols; b++)
                                    {
                                        bs.board[a, b] = char.Parse(split[splitCounter]);
                                        splitCounter++;
                                    }
                                }
                                StateAction sa = new StateAction(bs, int.Parse(split[0]));
                                Q_300K.Add(sa, double.Parse(split[splitCounter]));
                            }
                        }
                    }
                    break;
                case MainWindow.DifficultyOption.Medium:
                    strFileName = "data/Q_200K.dat";
                    if (File.Exists(strFileName))
                    {
                        using (StreamReader sr = new StreamReader(strFileName))
                        {
                            while (!sr.EndOfStream)
                            {
                                string line = await sr.ReadLineAsync();
                                string[] split = line.Split(',');
                                int rows = _boardRows;
                                int cols = _boardCols;
                                BoardState bs = new BoardState(rows, cols);
                                int splitCounter = 1;
                                for (int a = 0; a < rows; a++)
                                {
                                    for (int b = 0; b < cols; b++)
                                    {
                                        bs.board[a, b] = char.Parse(split[splitCounter]);
                                        splitCounter++;
                                    }
                                }
                                StateAction sa = new StateAction(bs, int.Parse(split[0]));
                                Q_200K.Add(sa, double.Parse(split[splitCounter]));
                            }
                        }
                    }
                    break;
                case MainWindow.DifficultyOption.Low:
                    strFileName = "data/Q_100K.dat";
                    if (File.Exists(strFileName))
                    {
                        using (StreamReader sr = new StreamReader(strFileName))
                        {
                            while (!sr.EndOfStream)
                            {
                                string line = await sr.ReadLineAsync();
                                string[] split = line.Split(',');
                                int rows = _boardRows;
                                int cols = _boardCols;
                                BoardState bs = new BoardState(rows, cols);
                                int splitCounter = 1;
                                for (int a = 0; a < rows; a++)
                                {
                                    for (int b = 0; b < cols; b++)
                                    {
                                        bs.board[a, b] = char.Parse(split[splitCounter]);
                                        splitCounter++;
                                    }
                                }
                                StateAction sa = new StateAction(bs, int.Parse(split[0]));
                                Q_100K.Add(sa, double.Parse(split[splitCounter]));
                            }
                        }
                    }
                    break;
                default:
                    strFileName = "data/Q_100K.dat";
                    if (File.Exists(strFileName))
                    {
                        using (StreamReader sr = new StreamReader(strFileName))
                        {
                            while (!sr.EndOfStream)
                            {
                                string line = await sr.ReadLineAsync();
                                string[] split = line.Split(',');
                                int rows = _boardRows;
                                int cols = _boardCols;
                                BoardState bs = new BoardState(rows, cols);
                                int splitCounter = 1;
                                for (int a = 0; a < rows; a++)
                                {
                                    for (int b = 0; b < cols; b++)
                                    {
                                        bs.board[a, b] = char.Parse(split[splitCounter]);
                                        splitCounter++;
                                    }
                                }
                                StateAction sa = new StateAction(bs, int.Parse(split[0]));
                                Q_100K.Add(sa, double.Parse(split[splitCounter]));
                            }
                        }
                    }
                    break;
            }


        }

        public async void SaveQs(MainWindow.DifficultyOption difficultyOption)
        {
            string line = "";

            if (difficultyOption == MainWindow.DifficultyOption.High)  // Only save learning for the High Difficulty Option
            {
                using (StreamWriter sw = new StreamWriter("data/Q_300K.dat"))
                {
                    foreach (var entry in Q)
                    {
                        line = entry.Key.action.ToString() + ",";
                        for (int a = 0; a < 6; a++)
                        {
                            for (int b = 0; b < 7; b++)
                            {
                                line = line + entry.Key.state.board[a, b].ToString() + ",";
                            }
                        }
                        line = line + entry.Value.ToString();
                        await sw.WriteLineAsync(line);
                    }
                    sw.Close();
                }
            }
        }

        #endregion

    }
}