using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Four_In_A_Row___OOP_version.Presentation
{
    public class ColorScheme
    {
        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ForegroundColor { get; set; }

        public ColorScheme() 
        {            
            // Save the current background and foreground colors.
            BackgroundColor = Console.BackgroundColor;
            ForegroundColor = Console.ForegroundColor;
        }

        public ColorScheme(string backgroundColor, string foregroundColor)
        {
            BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), backgroundColor);
            ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), foregroundColor);
        }

        public void Implement()
        {
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = ForegroundColor;
        }
    }
}
