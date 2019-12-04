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

            Options.ThemeChanged += (sender, e) => BackColor = Options.Theme.Background;
            Options.FullSCreenChanged += (sender, e) => SetFullScreen(Options.FullScreen);

            uttt = new UltimateTicTacToeGui();
            uttt.Parent = this;
            uttt.Dock = DockStyle.Fill;
            uttt.SendToBack();

            BackColor = Options.Theme.Background;
            SetFullScreen(Options.FullScreen);

            timer.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            pbCanvas.Image = uttt.Draw();
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
