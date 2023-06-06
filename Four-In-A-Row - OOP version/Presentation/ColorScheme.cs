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

        // Save the current background and foreground colors
        public ColorScheme() 
        {
            BackgroundColor = Console.BackgroundColor;
            ForegroundColor = Console.ForegroundColor;
        }

        // Save a specific background and foreground color
        public ColorScheme(string backgroundColor, string foregroundColor)
        {
            BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), backgroundColor);
            ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), foregroundColor);
        }

        // Implement the saved background and/or foreground colors
        // boolean parameters decide whether to implement or not
        public void Implement(bool background = true, bool foreground = true)
        {
            if (background)
            {
                Console.BackgroundColor = BackgroundColor;
            }

            if (foreground)
            {
                Console.ForegroundColor = ForegroundColor;
            }
        }

        public void changeBackgroundColorTo(string color)
        {
            this.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
        }

        public void ChangeForegroundColorTo(string color)
        {
            this.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color);
        }
    }
}
