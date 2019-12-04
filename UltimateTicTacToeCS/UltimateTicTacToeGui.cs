using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static UltimateTicTacToeCS.TicTacToeGui;

namespace UltimateTicTacToeCS
{
    public partial class UltimateTicTacToeGui : BoardControl
    {
        public UltimateTicTacToe UltimateTicTacToe { get; private set; }
        public TicTacToeGui[,] Boards { get; private set; }

        private int WinLineWidth => 50 * Math.Min(Width, Height) / 300;
        private Animation winAnimation;

        public UltimateTicTacToeGui()
        {
            InitializeComponent();
            UltimateTicTacToe = new UltimateTicTacToe();

            Boards = new TicTacToeGui[TicTacToe.ROWS, TicTacToe.COLS];

            for (int row = 0; row < TicTacToe.ROWS; ++row)
            {
                for (int col = 0; col < TicTacToe.COLS; ++col)
                {
                    Boards[row, col] = new TicTacToeGui(UltimateTicTacToe.Boards[row, col], false);
                    Boards[row, col].Parent = this;
                    Boards[row, col].MouseClickEnabled = false;
                    Boards[row, col].ShowLastMove = false;
                    Boards[row, col].MouseClick += MouseClicked;
                    Boards[row, col].Tag = new int[] { row, col };
                }
            }

            winAnimation = new Animation(500);
            Menu[0].Click += (sender, e) => NewGame();
        }

        public void NewGame()
        {
            UltimateTicTacToe.NewBoard();

            for (int row = 0; row < TicTacToe.ROWS; ++row)
            {
                for (int col = 0; col < TicTacToe.COLS; ++col)
                {
                    Boards[row, col].NewGame(UltimateTicTacToe.Boards[row, col]);
                }
            }

            winAnimation = new Animation(500);
        }

        private void MouseClicked(object sender, MouseEventArgs e)
        {
            var board = (TicTacToeGui)sender;
            var index = (int[])board.Tag;

            if (e.Button == MouseButtons.Left)
            {
                int row = e.Y / (board.Height / TicTacToe.ROWS);
                int col = e.X / (board.Width / TicTacToe.COLS);

                if (UltimateTicTacToe.LastMove[0] >= 0)
                {
                    Boards[UltimateTicTacToe.LastMove[0], UltimateTicTacToe.LastMove[1]].ShowLastMove = false;
                }

                if (UltimateTicTacToe.Play(index[0], index[1], row, col))
                {
                    board.Played(row, col);

                    board.ShowLastMove = true;

                    Parent.Text = string.Format("Ultimate Tic Tac Toe (Turn: {0}, Moves: {1})", UltimateTicTacToe.GameTurn, UltimateTicTacToe.Moves);
                }
                else
                {
                    Boards[UltimateTicTacToe.LastMove[0], UltimateTicTacToe.LastMove[1]].ShowLastMove = true;
                }

                if (UltimateTicTacToe.GameOver)
                {
                    winAnimation.Start();
                    Parent.Text = string.Format("Ultimate Tic Tac Toe (Won: {0}, Moves: {1})", UltimateTicTacToe.Winner, UltimateTicTacToe.Moves);
                }
            }
        }

        protected override void DrawSquare(int row, int col, RectangleF rect)
        {
            Boards[row, col].Location = new Point((int)rect.X, (int)rect.Y);
            Boards[row, col].Size = new Size((int)rect.Width, (int)rect.Height);
            Boards[row, col].Enabled = UltimateTicTacToe.PlayableBoards[row, col];
            gfx.DrawImage(Boards[row, col].Draw(), new Rectangle(Boards[row, col].Location, Boards[row, col].Size));
        }

        public override Image Draw()
        {
            var bm = base.Draw();

            // Draw win animation.
            if (UltimateTicTacToe.Winner == TicTacToe.WinState.Cross)
            {
                DrawCross(gfx, new RectangleF(BoardLocation, new SizeF(BoardSize, BoardSize)), Options.Theme.Cross, WinLineWidth, winAnimation.Value);
            }
            else if (UltimateTicTacToe.Winner == TicTacToe.WinState.Nought)
            {
                DrawNought(gfx, new RectangleF(BoardLocation.X + WinLineWidth / 2, BoardLocation.Y + WinLineWidth / 2, BoardSize - WinLineWidth, BoardSize - WinLineWidth), Options.Theme.Nought, WinLineWidth, winAnimation.Value);
            }

            return bm;
        }

        private void UltimateTicTacToeGuiMouseClick(object sender, MouseEventArgs e)
        {
            if (UltimateTicTacToe.GameOver)
            {
                //NewGame();
            }
        }
    }
}
