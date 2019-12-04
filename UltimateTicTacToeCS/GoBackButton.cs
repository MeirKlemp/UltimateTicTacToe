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

            var backColor = Color.FromArgb((int)(125 * mouseEnter.Value), (int)(125 * mouseEnter.Value), (int)(125 * mouseEnter.Value));
            gfx.FillRectangle(new SolidBrush(backColor), 0, 0, Width, Height);

            return bm;
        }
    }
}
