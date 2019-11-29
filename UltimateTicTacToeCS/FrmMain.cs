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
            uttt.Size = Size;
            uttt.SendToBack();

            FrmMain_Resize(this, EventArgs.Empty);

            timer.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            uttt.Invalidate();
            pbCanvas.Image = uttt.Draw();
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            int size = Math.Min(Width, Height);
            foreach (Control control in Controls)
            {
                control.Size = new Size(size, size);
                control.Location = new Point((Width - size) / 2, 0);
            }
        }
    }
}
