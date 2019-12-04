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
        public List<ImageDrawControl> Menu { get; set; }

        public BoardControl(bool goBackButton = true)
        {
            InitializeComponent();
            Menu = new List<ImageDrawControl>();

            if (goBackButton)
            {
                Menu.Add(new GoBackButton { Parent = this });
                Menu.Add(new GoBackButton { Parent = this });
                Menu.Add(new GoBackButton { Parent = this });
                Menu.Add(new GoBackButton { Parent = this });
                Menu.Add(new GoBackButton { Parent = this });
            }

            Resize += (sender, e) => {
                BoardSize = Math.Min(Width, Height);
                BoardLocation = new PointF((Width - BoardSize) / 2, (Height - BoardSize) / 2);

                if (BoardSize == 0)
                {
                    BoardSize = 1;
                }

                var menuBounds = GetMenuBounds();

                if (menuBounds != null)
                {
                    if (menuBounds.Value.Height == Height)
                    {
                        if (BoardLocation.X < menuBounds.Value.Width)
                        {
                            BoardSize = Math.Min(Width - menuBounds.Value.Width, Height);
                            BoardLocation = new PointF(menuBounds.Value.Width, (Height - BoardSize) / 2);
                        }
                    }
                    else
                    {
                        if (BoardLocation.Y < menuBounds.Value.Height)
                        {
                            BoardSize = Math.Min(Width, Height - menuBounds.Value.Height);
                            BoardLocation = new PointF((Width - BoardSize) / 2, menuBounds.Value.Height);
                        }
                    }

                    bool onSide = menuBounds.Value.Height == Height;
                    for (int i = 0; i < Menu.Count; i++)
                    {
                        if (onSide)
                        {
                            int size = (int)menuBounds.Value.Width;
                            Menu[i].Location = new Point(0, size * i);
                            Menu[i].Size = new Size(size, size);
                        }
                        else
                        {
                            int size = (int)menuBounds.Value.Height;
                            Menu[i].Location = new Point(size * i, 0);
                            Menu[i].Size = new Size(size, size);
                        }
                    }
                }
            };
        }

        protected abstract void DrawSquare(int row, int col, RectangleF rect);

        public override Image Draw()
        {
            var bm = base.Draw();

            DrawBoard();

            for (int i = 0; i < Menu.Count; i++)
            {
                gfx.DrawImage(Menu[i].Draw(), Menu[i].Bounds);
            }

            return bm;
        }

        private RectangleF? GetMenuBounds()
        {
            if (Menu.Count > 0)
            {
                if (Width > Height)
                {
                    float maxHeight = Height / Menu.Count;
                    float sizeW = Math.Min(Width * .2f, maxHeight);
                    return new RectangleF(0, 0, sizeW, Height);
                }
                float maxWidth = Width / Menu.Count;
                float sizeH = Math.Min(Height * .2f, maxWidth);
                return new RectangleF(0, 0, Width, sizeH);
            }

            return null;
        }

        private void DrawBoard()
        {
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
        }
    }
}
