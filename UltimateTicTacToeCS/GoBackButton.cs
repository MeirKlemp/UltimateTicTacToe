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
        public GoBackButton()
        {
            InitializeComponent();
        }

        public override Image Draw()
        {
            return base.Draw();
        }
    }
}
