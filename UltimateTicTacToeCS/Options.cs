using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToeCS
{
    public static class Options
    {
        public static event EventHandler ThemeChanged;
        public static event EventHandler UseAnimationsChanged;
        public static event EventHandler FullSCreenChanged;

        public static Theme Theme { get => theme; set { theme = value; ThemeChanged?.Invoke(Theme, EventArgs.Empty); } }
        public static bool UseAnimations { get => useAnimations; set { useAnimations = value; UseAnimationsChanged?.Invoke(useAnimations, EventArgs.Empty); } }
        public static bool FullScreen { get => fullScreen; set { fullScreen = value; FullSCreenChanged?.Invoke(fullScreen, EventArgs.Empty); } }

        private static Theme theme = Theme.Default;
        private static bool useAnimations = true;
        private static bool fullScreen = false;
    }
}
