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
    public partial class GoBackButton : ImageDrawControl
    {
        private int LineWidth => 5 * Math.Min(Width, Height) / 300;
        private int LineWidthHighlight => 5 * Math.Min(Width, Height) / 300;

        private Animation mouseEnter;

        public GoBackButton()
        {
            InitializeComponent();

            mouseEnter = new Animation(250);

            MouseEnter += (sender, e) => mouseEnter.Start();
            MouseLeave += (sender, e) => mouseEnter = new Animation(250);
        }

        public override Image Draw()
        {
            var bm = base.Draw();

            float space = .05f;
            int red = BackColor.R + (int)((Options.Theme.ButtonHighlight.R - BackColor.R) * mouseEnter.Value);
            int green = BackColor.G + (int)((Options.Theme.ButtonHighlight.G - BackColor.G) * mouseEnter.Value);
            int blue = BackColor.B + (int)((Options.Theme.ButtonHighlight.B - BackColor.B) * mouseEnter.Value);

            gfx.FillRectangle(new SolidBrush(Color.FromArgb(red, green, blue)), 0, 0, Width, Height);

            var pointLeft = new PointF(Width * space, Height / 2);
            var pointRight = new PointF(Width * (1 - space), Height / 2);
            var pointTop= new PointF(Width / 3, Height * space);
            var pointBottom = new PointF(Width / 3, Height * (1 - space));
            var width = LineWidth + LineWidthHighlight * mouseEnter.Value;

            gfx.DrawLine(new Pen(new SolidBrush(Options.Theme.ButtonMain), width), pointLeft, pointRight);
            gfx.DrawLine(new Pen(new SolidBrush(Options.Theme.ButtonMain), width), pointLeft, pointTop);
            gfx.DrawLine(new Pen(new SolidBrush(Options.Theme.ButtonMain), width), pointLeft, pointBottom);

            return bm;
        }
    }
}
