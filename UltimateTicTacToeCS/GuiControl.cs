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
    public abstract partial class GuiControl : BoardControl
    {
        public List<ImageDrawControl> Menu { get; set; }

        public GuiControl(bool goBackButton = true)
        {
            InitializeComponent();
            Menu = new List<ImageDrawControl>();

            if (goBackButton)
            {
                Menu.Add(new GoBackButton());
            }
        }

        public override Image Draw()
        {
            var headerBounds = GetHeaderBounds();

            if (headerBounds != null)
            {
                if (headerBounds.Value.Height == Height)
                {
                    if (BoardLocation.X < headerBounds.Value.Width)
                    {
                        BoardSize = Math.Min(Width - headerBounds.Value.Width, Height);
                        BoardLocation = new PointF(headerBounds.Value.Width, (Height - BoardSize) / 2);
                    }
                }
                else
                {
                    if (BoardLocation.Y < headerBounds.Value.Height)
                    {
                        BoardSize = Math.Min(Width, Height - headerBounds.Value.Height);
                        BoardLocation = new PointF((Width - BoardSize) / 2, headerBounds.Value.Height);
                    }
                }
            }

            var bm = base.Draw();

            if (headerBounds != null)
            {
                gfx.FillRectangle(Brushes.Blue, headerBounds.Value);
            }

            return bm;
        }

        private RectangleF? GetHeaderBounds()
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
    }
}
