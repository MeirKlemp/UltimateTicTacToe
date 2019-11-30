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
        public Size FixedSize => new Size(Width - (FormBorderStyle == FormBorderStyle.Sizable ? 16 : 0), Height - (FormBorderStyle == FormBorderStyle.Sizable ? 39 : 0));

        private UltimateTicTacToeGui uttt;

        public FrmMain()
        {
            InitializeComponent();

            Options.ThemeChanged += (sender, e) => BackColor = Options.Theme.Background;
            Options.FullSCreenChanged += (sender, e) => SetFullScreen(Options.FullScreen);

            uttt = new UltimateTicTacToeGui();
            uttt.Parent = this;
            uttt.Size = Size;
            uttt.SendToBack();

            BackColor = Options.Theme.Background;
            SetFullScreen(Options.FullScreen);
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
            int size = Math.Min(FixedSize.Width, FixedSize.Height);
            foreach (Control control in Controls)
            {
                control.Size = new Size(size, size);
                control.Location = new Point((Width - 16 - size) / 2, 0);
            }
        }

        private void SetFullScreen(bool full)
        {
            if (full)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
            }
        }
    }
}
