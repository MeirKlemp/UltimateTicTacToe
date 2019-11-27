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
    public partial class FrmMain : Form
    {
        private UltimateTicTacToeGui uttt;
        public FrmMain()
        {
            InitializeComponent();

            uttt = new UltimateTicTacToeGui();
            uttt.Parent = this;
            uttt.Dock = DockStyle.Fill;
            uttt.SendToBack();

            timer.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            uttt.Invalidate();
            pictureBox1.Image = uttt.Draw();
        }
    }
}
