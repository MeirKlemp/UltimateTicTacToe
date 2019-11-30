using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace UltimateTicTacToeCS
{
    public class Theme
    {
        public static Theme Default => new Theme { 
            Background = Color.Black,
            Cross = Color.Blue,
            Nought = Color.Red,
            AvailableSquare = Color.Orange,
            Lines = Color.White
        };

        public Color Background { get; set; }
        public Color Cross { get; set; }
        public Color Nought { get; set; }
        public Color AvailableSquare { get; set; }
        public Color Lines { get; set; }
    }
}
