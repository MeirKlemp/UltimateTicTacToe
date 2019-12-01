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
        public GuiControl()
        {
            InitializeComponent();
        }

        public override Image Draw()
        {
            var bm = base.Draw();

            return bm;
        }
    }
}
