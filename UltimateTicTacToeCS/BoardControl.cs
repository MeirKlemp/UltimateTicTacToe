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
        protected int LineWidth => 3 * Math.Min(Width, Height) / 300;
        protected float RowHeight => Height / TicTacToe.ROWS;
        protected float ColWidth => Width / TicTacToe.COLS;

        public BoardControl()
        {
            InitializeComponent();
        }

        protected abstract void DrawSquare(int row, int col, RectangleF rect);

        public override Image Draw()
        {
            var bm = base.Draw();

            // Draw speration lines.
            // Rows.
            for (int row = 1; row < TicTacToe.ROWS; row++)
            {
                gfx.DrawLine(new Pen(new SolidBrush(Options.Theme.Lines), LineWidth), new PointF(0, RowHeight * row), new PointF(Width, RowHeight * row));
            }

            // Cols.
            for (int col = 1; col < TicTacToe.COLS; col++)
            {
                gfx.DrawLine(new Pen(new SolidBrush(Options.Theme.Lines), LineWidth), new PointF(ColWidth * col, 0), new PointF(ColWidth * col, Height));
            }

            // Draw Squares.
            float space = 0.1f;
            SizeF sqrSize = new SizeF(ColWidth * (1 - 2 * space), RowHeight * (1 - 2 * space));

            for (int row = 0; row < TicTacToe.ROWS; row++)
            {
                for (int col = 0; col < TicTacToe.COLS; col++)
                {
                    PointF sqrLoc = new PointF(ColWidth * (col + space), RowHeight * (row + space));
                    DrawSquare(row, col, new RectangleF(sqrLoc, sqrSize));
                }
            }

            return bm;
        }
    }
}
