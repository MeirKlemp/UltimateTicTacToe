using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateTicTacToeCS
{
    public abstract partial class BoardControl : ImageDrawControl
    {
        protected int LineWidth => 3 * (int)BoardSize / 300;
        protected float RowHeight => BoardSize / TicTacToe.ROWS;
        protected float ColWidth => BoardSize / TicTacToe.COLS;
        public float BoardSize { get; set; }
        public PointF BoardLocation { get; set; }

        public BoardControl()
        {
            InitializeComponent();

            Resize += (sender, e) => {
                BoardSize = Math.Min(Width, Height);
                BoardLocation = new PointF((Width - BoardSize) / 2, (Height - BoardSize) / 2);

                if (BoardSize == 0)
                {
                    BoardSize = 1;
                }
            };
        }

        protected abstract void DrawSquare(int row, int col, RectangleF rect);

        public override Image Draw()
        {
            var bm = base.Draw();

            // Draw speration lines.
            // Rows.
            for (int row = 1; row < TicTacToe.ROWS; row++)
            {
                var pointA = new PointF(BoardLocation.X, BoardLocation.Y + RowHeight * row);
                var pointB = new PointF(BoardLocation.X + BoardSize, BoardLocation.Y + RowHeight * row);
                gfx.DrawLine(new Pen(new SolidBrush(Options.Theme.Lines), LineWidth), pointA, pointB);
            }

            // Cols.
            for (int col = 1; col < TicTacToe.COLS; col++)
            {
                var pointA = new PointF(BoardLocation.X + ColWidth * col, BoardLocation.Y);
                var pointB = new PointF(BoardLocation.X + ColWidth * col, BoardLocation.Y + BoardSize);
                gfx.DrawLine(new Pen(new SolidBrush(Options.Theme.Lines), LineWidth), pointA, pointB);
            }

            // Draw Squares.
            float space = 0.1f;
            SizeF sqrSize = new SizeF(ColWidth * (1 - 2 * space), RowHeight * (1 - 2 * space));

            for (int row = 0; row < TicTacToe.ROWS; row++)
            {
                for (int col = 0; col < TicTacToe.COLS; col++)
                {
                    PointF sqrLoc = new PointF(BoardLocation.X + ColWidth * (col + space), BoardLocation.Y + RowHeight * (row + space));
                    DrawSquare(row, col, new RectangleF(sqrLoc, sqrSize));
                }
            }

            return bm;
        }
    }
}
