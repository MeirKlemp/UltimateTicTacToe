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
    public partial class TicTacToeGui : Control
    {
        public static void DrawCross(Graphics gfx, RectangleF rect, Color color, int width, float animValue = 1)
        {
            var location = rect.Location;
            var size = rect.Size;

            if (animValue < .5f)
            {
                gfx.DrawLine(new Pen(new SolidBrush(color), width), location,
                    new PointF(location.X + size.Width * animValue * 2, location.Y + size.Height * animValue * 2));
            }
            else
            {
                gfx.DrawLine(new Pen(new SolidBrush(color), width), location,
                    new PointF(location.X + size.Width, location.Y + size.Height));

                animValue -= .5f;
                gfx.DrawLine(new Pen(new SolidBrush(color), width), new PointF(location.X + size.Width, location.Y),
                    new PointF(location.X + size.Width * (1 - animValue * 2), location.Y + size.Height * animValue * 2));
            }
        }

        public static void DrawNought(Graphics gfx, RectangleF rect, Color color, int width, float animValue = 1)
        {
            gfx.DrawArc(new Pen(new SolidBrush(color), width), rect, -90, 360 * animValue);
        }

        public TicTacToe TicTacToe { get; private set; }
        public bool MouseClickEnabled { get; set; }

        private int LineWidth => 3 * Math.Min(Width, Height) / 300;
        private int WinLineWidth => 50 * Math.Min(Width, Height) / 300;
        private Animation[,] animations;
        private Animation winAnimation;
        private Animation enabled;
        private Animation mouseOn;
        private Point sqrMouse;

        public TicTacToeGui()
        {
            InitializeComponent();

            MouseClickEnabled = true;
            enabled = new Animation(250);
            NewGame();
        }
        public TicTacToeGui(TicTacToe board)
        {
            InitializeComponent();

            MouseClickEnabled = true;
            enabled = new Animation(250);
            NewGame();

            TicTacToe = board;
        }

        public void NewGame()
        {
            animations = new Animation[TicTacToe.ROWS, TicTacToe.COLS];
            TicTacToe = new TicTacToe();
            sqrMouse = new Point(-1, -1);

            for (int row = 0; row < TicTacToe.ROWS; ++row)
            {
                for (int col = 0; col < TicTacToe.COLS; ++col)
                {
                    animations[row, col] = new Animation(500);
                }
            }

            winAnimation = new Animation(500);
            mouseOn = new Animation(250);
            enabled.Start();
        }

        public void DrawEmpty(Graphics gfx, RectangleF rect, Color color, float animValue = 1, float mouseAnim = 0)
        {
            mouseAnim *= .25f;
            rect = new RectangleF(rect.X + rect.Width / 2 * (1 - animValue - mouseAnim), rect.Y + rect.Height / 2 * (1 - animValue - mouseAnim),
                rect.Width * (animValue + mouseAnim), rect.Height * (animValue + mouseAnim));
            gfx.FillEllipse(new SolidBrush(color), rect);
        }

        public void DrawSquare(int row, int col, Graphics gfx, RectangleF rect)
        {
            if (TicTacToe.Board[row, col] == TicTacToe.SqrState.Cross)
            {
                DrawCross(gfx, rect, Color.Blue, LineWidth, animations[row, col].Value);
            }
            else if (TicTacToe.Board[row, col] == TicTacToe.SqrState.Nought)
            {
                DrawNought(gfx, rect, Color.Red, LineWidth, animations[row, col].Value);
            } 
            else if (Enabled && !TicTacToe.GameOver)
            {
                rect = new RectangleF(rect.X + rect.Width / 4, rect.Y + rect.Height / 4, rect.Width / 2, rect.Height / 2);

                float mouseAnim = 0;
                if (sqrMouse.X == row && sqrMouse.Y == col)
                {
                    mouseAnim += mouseOn.Value;
                }

                DrawEmpty(gfx, rect, Color.Orange, enabled.Value, mouseAnim);
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
            SizeF sqrSize = new SizeF(colWidth * (1 - 2 * space), rowHeight * (1 - 2 * space));

            for (int row = 0; row < TicTacToe.ROWS; row++)
            {
                for (int col = 0; col < TicTacToe.COLS; col++)
                {
                    PointF sqrLoc = new PointF(colWidth * (col + space), rowHeight * (row + space));
                    DrawSquare(row, col, pe.Graphics, new RectangleF(sqrLoc, sqrSize));
                }
            }

            // Draw win animation.
            if (TicTacToe.Winner == TicTacToe.WinState.Cross)
            {
                DrawCross(pe.Graphics, new RectangleF(0, 0, Width, Height), Color.Blue, WinLineWidth, winAnimation.Value);
            }
            else if (TicTacToe.Winner == TicTacToe.WinState.Nought)
            {
                DrawNought(pe.Graphics, new RectangleF(WinLineWidth / 2, WinLineWidth / 2, Width - WinLineWidth, Height - WinLineWidth), Color.Red,  WinLineWidth, winAnimation.Value);
            }
        }

        public void Played(int row, int col)
        {
            animations[row, col].Start();

            if (TicTacToe.GameOver)
            {
                winAnimation.Start();
            }

            sqrMouse.X = -1;
        }

        private void MouseClicked(object sender, MouseEventArgs e)
        {
            if (MouseClickEnabled)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (TicTacToe.GameOver)
                    {
                        NewGame();
                    }
                    else
                    {
                        int row = e.Y / (Height / TicTacToe.ROWS);
                        int col = e.X / (Width / TicTacToe.COLS);

                        if (TicTacToe.Play(row, col))
                        {
                            Played(row, col);
                        }
                    }
                }
                else
                {
                    if (!Animation.Paused)
                    {
                        Animation.Pause();
                    }
                    else
                    {
                        Animation.Resume();
                    }
                }
            }
        }

        private void MouseMoved(object sender, MouseEventArgs e)
        {
            int row = e.Y / (Height / TicTacToe.ROWS);
            int col = e.X / (Width / TicTacToe.COLS);

            if (!TicTacToe.GameOver && TicTacToe.InBounds(row, col) && TicTacToe.Board[row, col] == TicTacToe.SqrState.Empty)
            {
                if (sqrMouse.X != row || sqrMouse.Y != col)
                {
                    sqrMouse = new Point(row, col);
                    mouseOn.Start();
                }
            }
            else
            {
                sqrMouse.X = -1;
            }
        }

        private void MouseLeaved(object sender, EventArgs e)
        {
            sqrMouse = new Point(-1, -1);
        }

        private void TicTacToeGuiEnabledChanged(object sender, EventArgs e)
        {
            if (Enabled)
            {
                enabled.Start();
            }
        }
    }
}
