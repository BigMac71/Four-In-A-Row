using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Four_In_A_Row___OOP_version.Presentation
{
    internal class ConsoleConfig
    {
        private const string TITLE = "Four-In-A-Row - OOP version";

        public int leftCursorPosition { get; set; }
        public int topCursorPosition { get; set; }

        public static void Reset()
        {
            Console.Title = TITLE;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ResetColor();
            Console.Clear();
        }

        public void saveCursorPosition(int left, int top)
        {
            leftCursorPosition = left;
            topCursorPosition = top;
        }

        public void setCursorPosition(int left, int top)
        {
            Console.CursorLeft = left;
            Console.CursorTop = top;
        }

    }
}
