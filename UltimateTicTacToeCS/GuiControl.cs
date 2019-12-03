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

        public GuiControl()
        {
            InitializeComponent();
            Menu = new List<ImageDrawControl>();
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

            return bm;
        }

        private RectangleF? GetHeaderBounds()
        {
            if (Menu.Count > 0)
            {
                if (Width > Height)
                {
                    return new RectangleF(0, 0, Width * .2f, Height);
                }
                return new RectangleF(0, 0, Width, Height * .2f);
            }

            return null;
        }
    }
}
