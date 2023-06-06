using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Four_In_A_Row___OOP_version.Presentation
{
    public class ConsoleConfig
    {
        private const string TITLE = "Four-In-A-Row - OOP version";

        public int leftCursorPosition { get; set; } = 2;
        public int topCursorPosition { get; set; } = 2;

        public static void Reset()
        {
            Console.Title = TITLE;
            Console.OutputEncoding = Encoding.UTF8;
            Console.ResetColor();
            Console.Clear();
        }

        public void SetCursorPosition() 
        {
            Console.CursorLeft = leftCursorPosition;
            Console.CursorTop = topCursorPosition;
        }

        public static void SetCursorPosition(int left, int top)
        {
            Console.CursorLeft = left;
            Console.CursorTop = top;
        }

        public void SaveCursorPosition(int left, int top)
        {
            leftCursorPosition = left;
            topCursorPosition = top;
        }
    }
}
