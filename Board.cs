using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect
{
    public enum Player
    {
        Player1 = 'B',
        Player2 = 'R'
    }

    public enum Result
    {
        Invalid,
        Intermediate,
        Win,
        Loss,
        Tie
    }

    public enum Score
    {
        Loss = -10,
        Tie = 0,
        Win = 10
    }

    public class BoardState
    {
        public int rows, cols;
        public char[,] board;
        public List<Tuple<int, int>> winners = new List<Tuple<int, int>>();
        static int _boardCols = 7;
        static int _boardRows = 6;
        public int startcol = 0;
        public int numcols = 0;


        public BoardState(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;

            this.board = new char[rows, cols];
            for (int i = 0; i < this.rows; ++i)
            {
                for (int j = 0; j < this.cols; ++j)
                {
                    this.board[i, j] = '-';
                }
            }
        }

        public BoardState(BoardState other)
        {
            this.rows = other.rows;
            this.cols = other.cols;
            this.board = new char[rows, cols];
            for (int i = 0; i < this.rows; ++i)
            {
                for (int j = 0; j < this.cols; ++j)
                {
                    this.board[i, j] = other.board[i, j];
                }
            }
        }

        public bool areFourConnected(BoardState bs, char player)
        {
            char[,] board = bs.board;

            // horizontalCheck 
            for (int i = 0; i < _boardRows; i++)
            {
                for (int j = 0; j < _boardCols - 3; j++)
                {
                    if (board[i, j] == player && board[i, j + 1] == player && board[i, j + 2] == player && board[i, j + 3] == player)
                    {
                        startcol = j + 1;  //adjust for 0 based
                        numcols = 4;
                        bs.winners.Clear();
                        bs.winners.Add(Tuple.Create(i, j));
                        bs.winners.Add(Tuple.Create(i, j + 1));
                        bs.winners.Add(Tuple.Create(i, j + 2));
                        bs.winners.Add(Tuple.Create(i, j + 3));

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
                        startcol = j + 1;  //adjust for 0 based
                        numcols = 1;
                        bs.winners.Clear();
                        bs.winners.Add(Tuple.Create(i, j));
                        bs.winners.Add(Tuple.Create(i - 1, j));
                        bs.winners.Add(Tuple.Create(i - 2, j));
                        bs.winners.Add(Tuple.Create(i - 3, j));

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
                        bs.winners.Clear();
                        bs.winners.Add(Tuple.Create(i, j));
                        bs.winners.Add(Tuple.Create(i + 1, j + 1));
                        bs.winners.Add(Tuple.Create(i + 2, j + 2));
                        bs.winners.Add(Tuple.Create(i + 3, j + 3));

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
                        winners.Clear();
                        winners.Add(Tuple.Create(i, j));
                        winners.Add(Tuple.Create(i - 1, j + 1));
                        winners.Add(Tuple.Create(i - 2, j + 2));
                        winners.Add(Tuple.Create(i - 3, j + 3));

                        return true;
                    }
                }
            }
            return false;
        }

        public bool isTie(BoardState bs)
        {
            if (!areFourConnected(bs, 'B') && !areFourConnected(bs, 'R'))
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

        #region UNUSED
        /*public static bool operator ==(BoardState lhs, BoardState rhs)
        {
            return Equals(lhs, rhs);
        }

        public static bool operator !=(BoardState lhs, BoardState rhs)
        {
            return !Equals(lhs, rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj is BoardState)
            {
                BoardState other = obj as BoardState;
                if (this.rows != other.rows)
                    return false;
                if (this.cols != other.cols)
                    return false;

                for (int i = 0; i < other.rows; i++)
                {
                    for (int j = 0; j < other.rows; j++)
                    {
                        if (this.board[i, j] != other.board[i, j])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        } 

        public static BoardState initFromArray(char[] arr, int size, int cols)
        {
            int rows = size / cols;
            BoardState b = new BoardState(rows, cols);
            for (int i = 0; i < size; i++)
            {
                int r = (i / rows);
                int c = i % cols;
                if (arr[i] == 1 || arr[i] == 2)
                {
                    b.board[r, c] = arr[i];
                }
            }
            return b;
        } 

        public BoardState move(int col, Player p, ref Result r)
        {
            int changedRow = -1;
            BoardState newState = new BoardState(this);
            if (this.board[0, col] != '-')  //check if col is full
            {
                if (isTie(newState))
                {
                    r = Result.Tie;
                    return newState;
                }
                else
                {
                    r = Result.Invalid;
                    return null;
                }
            }

            for (int i = newState.rows - 1; i >= 0; --i)
            {
                if (newState.board[i, col] == '-')
                {
                    newState.board[i, col] = (char)p;
                    changedRow = i;
                    break;
                }
            }
            r = Result.Intermediate;
            if (areFourConnected(newState, (char)p))
            {
                r = Result.Win;
            }
            else if (isTie(newState))
            {
                r = Result.Tie;
            }
            return newState;
        } 

        public static bool validRowCol(int rows, int cols, int r, int c)
        {
            return (r < rows && r >= 0) && (c < cols && c >= 0);
        } 

        public static bool match(char[,] board, int row, int col, char p)
        {
            return board[row, col] == p;
        } */
        #endregion

    }
}
