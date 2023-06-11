using System.Text;

namespace Four_In_A_Row___OOP_version.Presentation
{
    public static class ConsoleConfig
    {
        private static readonly string TITLE = "Four-In-A-Row - OOP version";

        public static void Reset()
        {
            Console.Title = TITLE;
            Console.OutputEncoding = Encoding.UTF8;
            Console.ResetColor();
            Console.Clear();
        }

        public static void SetCursorPosition(int left, int top)
        {
            Console.CursorLeft = left;
            Console.CursorTop = top;
        }

        public static void SetColors(ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
        }
    }
}
