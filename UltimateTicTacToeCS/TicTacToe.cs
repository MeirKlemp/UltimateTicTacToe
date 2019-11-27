using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToeCS
{
    public class TicTacToe
    {
        public const int ROWS = 3;
        public const int COLS = 3;
        public const int WIN_SET = 3;

        public static SqrState TurnToSqrState(Turn turn)
        {
            if (turn == Turn.Cross)
            {
                return SqrState.Cross;
            }

            return SqrState.Nought;
        }

        public enum SqrState { Nought = -1, Empty, Cross }
        public enum Turn { Cross, Nought }
        public enum WinState { Nought=-1, Draw, Cross, NoOne}

        public SqrState[,] Board { get; private set; }
        public int Moves { get; set; }
        public Turn GameTurn { get; set; }
        public WinState Winner { get; private set; }
        public bool GameOver => Winner != WinState.NoOne;
        public TicTacToe Clone => new TicTacToe(this);

        public TicTacToe(TicTacToe clone)
        {
            Board = new SqrState[ROWS, COLS];
            Moves = clone.Moves;
            GameTurn = clone.GameTurn;
            Winner = clone.Winner;

            for (int row = 0; row < ROWS; ++row)
            {
                for (int col = 0; col < COLS; ++col)
                {
                    Board[row, col] = clone.Board[row, col];
                }
            }
        }

        public TicTacToe()
        {
            NewBoard();
        }

        public void NewBoard()
        {
            Board = new SqrState[ROWS, COLS];
            Moves = 0;
            Winner = WinState.NoOne;
            GameTurn = Turn.Cross;
        }

        public bool Play(int row, int col)
        {
            if (InBounds(row, col) && Board[row, col] == SqrState.Empty && !GameOver)
            {
                Board[row, col] = TurnToSqrState(GameTurn);
                CheckWin();
                ++Moves;
                NextTurn();
                return true;
            }

            return false;
        }

        public void NextTurn()
        {
            GameTurn = GameTurn == Turn.Cross ? Turn.Nought : Turn.Cross;
        }

        public void CheckWin()
        {
            var state = TurnToSqrState(GameTurn);
            bool haveMoves = false;

            for (int row = 0; row < ROWS; ++row)
            {
                for (int col = 0; col < COLS; ++col)
                {
                    if (Board[row, col] == state)
                    {
                        for (int dRow = -1; dRow < 2; ++dRow)
                        {
                            for (int dCol = -1; dCol < 2; ++dCol)
                            {
                                if (dRow == 0 && dCol == 0)
                                {
                                    continue;
                                }

                                int w = 1;
                                for (; w < WIN_SET; w++)
                                {
                                    if (!InBounds(row + dRow * w, col + dCol * w) || Board[row + dRow * w, col + dCol * w] != state)
                                    {
                                        break;
                                    }
                                }

                                if (w == WIN_SET)
                                {
                                    Winner = (WinState)state;
                                    return;
                                }
                            }
                        }
                    }
                    else if (Board[row, col] == SqrState.Empty)
                    {
                        haveMoves = true;
                    }
                }
            }

            if (!haveMoves)
            {
                Winner = WinState.Draw;
            }
        }

        public bool InBounds(int row, int col)
        {
            return Math.Max(0, Math.Min(row, ROWS - 1)) == row &&
                Math.Max(0, Math.Min(col, COLS - 1)) == col;
        }
    }
}
