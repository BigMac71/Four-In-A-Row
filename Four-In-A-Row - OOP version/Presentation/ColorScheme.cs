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
        public ColorScheme(ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
        }

        // Implement the saved background and/or foreground colors
        // boolean parameters decide whether to implement either back- and/or foreground
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
    }
}
