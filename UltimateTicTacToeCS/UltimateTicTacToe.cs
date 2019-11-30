using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UltimateTicTacToeCS.TicTacToe;

namespace UltimateTicTacToeCS
{
    public class UltimateTicTacToe
    {
        public TicTacToe[,] Boards { get; set; }
        public bool[,] PlayableBoards { get; set; }
        public int Moves { get {
                int moves = 0;
                for (int row = 0; row < ROWS; ++row)
                {
                    for (int col = 0; col < COLS; ++col)
                    {
                        moves += Boards[row, col].Moves;
                    }
                }

                return moves;
            } 
        }
        public Turn GameTurn { get; set; }
        public WinState Winner { get; private set; }
        public bool GameOver => Winner != WinState.NoOne;
        public UltimateTicTacToe Clone => new UltimateTicTacToe(this);

        public UltimateTicTacToe(UltimateTicTacToe clone)
        {
            Boards = new TicTacToe[ROWS, COLS];
            PlayableBoards = new bool[ROWS, COLS];
            Winner = clone.Winner;
            GameTurn = clone.GameTurn;

            for (int row = 0; row < ROWS; ++row)
            {
                for (int col = 0; col < COLS; ++col)
                {
                    Boards[row, col] = clone.Boards[row, col].Clone;
                    PlayableBoards[row, col] = clone.PlayableBoards[row, col];
                }
            }
        }
        public UltimateTicTacToe()
        {
            Boards = new TicTacToe[ROWS, COLS];
            PlayableBoards = new bool[ROWS, COLS];
            Winner = WinState.NoOne;
            GameTurn = Turn.Cross;

            for (int row = 0; row < ROWS; ++row)
            {
                for (int col = 0; col < COLS; ++col)
                {
                    Boards[row, col] = new TicTacToe();
                    PlayableBoards[row, col] = true;
                }
            }
        }

        public void NewBoard()
        {
            PlayableBoards = new bool[ROWS, COLS];
            Winner = WinState.NoOne;
            GameTurn = Turn.Cross;

            for (int row = 0; row < ROWS; ++row)
            {
                for (int col = 0; col < COLS; ++col)
                {
                    Boards[row, col].NewBoard();
                    PlayableBoards[row, col] = true;
                }
            }
        }

        public bool Play(int boardRow, int boardCol, int sqrRow, int sqrCol)
        {
            if (InBounds(boardRow, boardCol) && PlayableBoards[boardRow, boardCol])
            {
                Boards[boardRow, boardCol].GameTurn = GameTurn;
                if (Boards[boardRow, boardCol].Play(sqrRow, sqrCol))
                {
                    CheckWin();
                    for (int row = 0; row < ROWS; ++row)
                    {
                        for (int col = 0; col < COLS; ++col)
                        {
                            PlayableBoards[row, col] = !Boards[row, col].GameOver && !GameOver;
                            if (row != sqrRow || col != sqrCol)
                            {
                                PlayableBoards[row, col] &= Boards[sqrRow, sqrCol].GameOver;
                            }
                        }
                    }
                    NextTurn();
                    return true;
                }
            }

            return false;
        }

        public void NextTurn()
        {
            GameTurn = GameTurn == Turn.Cross ? Turn.Nought : Turn.Cross;
        }

        public void CheckWin()
        {
            var winState = (WinState)TurnToSqrState(GameTurn);
            bool haveMoves = false;

            for (int row = 0; row < ROWS; ++row)
            {
                for (int col = 0; col < COLS; ++col)
                {
                    if (Boards[row, col].Winner == winState)
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
                                    if (!InBounds(row + dRow * w, col + dCol * w) || Boards[row + dRow * w, col + dCol * w].Winner != winState)
                                    {
                                        break;
                                    }
                                }

                                if (w == WIN_SET)
                                {
                                    Winner = winState;
                                    return;
                                }
                            }
                        }
                    }
                    else if (Boards[row, col].Winner == WinState.NoOne)
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
            return Boards[0, 0].InBounds(row, col);
        }
    }
}
