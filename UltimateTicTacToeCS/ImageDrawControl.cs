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
    public abstract partial class ImageDrawControl : Control
    {
        protected Graphics gfx;

        public ImageDrawControl()
        {
            InitializeComponent();

            Options.ThemeChanged += (sender, e) => BackColor = Options.Theme.Background;
        }

        public virtual Image Draw()
        {
            int width = Width == 0 ? 1 : Width;
            int height = Height== 0 ? 1 : Height;

            var bm = new Bitmap(width, height);

            if (gfx != null)
            {
                gfx.Dispose();
            }

            gfx = Graphics.FromImage(bm);
            gfx.FillRectangle(new SolidBrush(BackColor), 0, 0, width, height);

            return bm;
        }
    }
}
