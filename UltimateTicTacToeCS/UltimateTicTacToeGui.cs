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
    public partial class UltimateTicTacToeGui : Control
    {
        public UltimateTicTacToe UltimateTicTacToe { get; private set; }
        public TicTacToeGui[,] Boards { get; private set; }

        private int LineWidth => 3 * Math.Min(Width, Height) / 300;
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
                    Boards[row, col] = new TicTacToeGui(UltimateTicTacToe.Boards[row, col]);
                    Boards[row, col].Parent = this;
                    Boards[row, col].MouseClickEnabled = false;
                    Boards[row, col].MouseClick += MouseClicked;
                    Boards[row, col].Tag = new int[] { row, col };
                }
            }

            winAnimation = new Animation(500);
            Invalidate();
        }

        public void NewGame()
        {
            UltimateTicTacToe.NewBoard();

            for (int row = 0; row < TicTacToe.ROWS; ++row)
            {
                for (int col = 0; col < TicTacToe.COLS; ++col)
                {
                    Boards[row, col].NewGame();
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

                if (UltimateTicTacToe.Play(index[0], index[1], row, col))
                {
                    board.Played(row, col);
                }

                if (UltimateTicTacToe.GameOver)
                {
                    winAnimation.Start();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            // Draw speration lines.
            // Rows.
            float rowHeight = Height / TicTacToe.ROWS;
            for (int row = 1; row < TicTacToe.ROWS; row++)
            {
                pe.Graphics.DrawLine(new Pen(Brushes.Black, LineWidth), new PointF(0, rowHeight * row), new PointF(Width, rowHeight * row));
            }

            // Cols.
            float colWidth = Width / TicTacToe.COLS;
            for (int col = 1; col < TicTacToe.COLS; col++)
            {
                pe.Graphics.DrawLine(new Pen(Brushes.Black, LineWidth), new PointF(colWidth * col, 0), new PointF(colWidth * col, Height));
            }

            // Draw Squares.
            float space = 0.1f;
            Size boardSize = new Size((int)(colWidth * (1 - 2 * space)), (int)(rowHeight * (1 - 2 * space)));

            for (int row = 0; row < TicTacToe.ROWS; row++)
            {
                for (int col = 0; col < TicTacToe.COLS; col++)
                {
                    Boards[row, col].Location = new Point((int)(colWidth * (col + space)), (int)(rowHeight * (row + space)));
                    Boards[row, col].Size = boardSize;
                    Boards[row, col].Enabled = UltimateTicTacToe.PlayableBoards[row, col];
                    Boards[row, col].Invalidate();
                }
            }

            // Draw win animation.
            if (UltimateTicTacToe.Winner == TicTacToe.WinState.Cross)
            {
                DrawCross(pe.Graphics, new RectangleF(0, 0, Width, Height), Color.Blue, WinLineWidth, winAnimation.Value);
            }
            else if (UltimateTicTacToe.Winner == TicTacToe.WinState.Nought)
            {
                DrawNought(pe.Graphics, new RectangleF(WinLineWidth / 2, WinLineWidth / 2, Width - WinLineWidth, Height - WinLineWidth), Color.Red, WinLineWidth, winAnimation.Value);
            }
        }

        private void UltimateTicTacToeGuiMouseClick(object sender, MouseEventArgs e)
        {
            if (UltimateTicTacToe.GameOver)
            {
                NewGame();
            }
        }
    }
}
