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
        public List<ImageDrawControl> Header { get; set; }

        public GuiControl()
        {
            InitializeComponent();
            Header = new List<ImageDrawControl>();
            Header.Add(new TicTacToeGui());
        }

        public override Image Draw()
        {
            var headerBounds = GetHeaderBounds();

            if (headerBounds != null)
            {
                if (BoardLocation.X < headerBounds.Value.Width)
                {
                    BoardSize = Math.Min(Width - headerBounds.Value.Width, Height);
                    BoardLocation = new PointF(headerBounds.Value.Width, (Height - BoardSize) / 2);
                }
            }

            var bm = base.Draw();

            gfx.FillRectangle(Brushes.Blue, headerBounds.Value);

            return bm;
        }

        private RectangleF? GetHeaderBounds()
        {
            if (Header.Count > 0)
            {
                return new RectangleF(0, 0, Width * .2f, Height);
            }

            return null;
        }
    }
}
