namespace Four_In_A_Row___OOP_version.Presentation
{
    public class ConsoleOutput : ConsoleConfig
    {
        public static readonly string backgroundColor = "Black";
        public static readonly string foregroundColor = "White";

        public ConsoleOutput(int left, int top)
        {
            leftCursorPosition = left;
            topCursorPosition = top;
        }

        public static void SetDefaultColors()
        {
            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), backgroundColor);
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), foregroundColor);
        }

        public void Print(string text, bool crlf)
        {
            Console.SetCursorPosition(leftCursorPosition, topCursorPosition);
            if (crlf)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.Write(text);
            }
        }

        /* @ToDo */
        private static void DisplayGameBoard()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int row = 1; row <= gameBoardSize["rows"][1]; row++)
            {
                for (int column = 1; column <= gameBoardSize["columns"][1]; column++)
                {
                    Console.SetCursorPosition((column * 3) - 1, row);
                    Console.Write("|");
                    /* gameBoard gives player number, index 1 gives 2nd value of string array = token string */
                    string token = players[key: gameBoard[column, row]][1];
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), players[key: gameBoard[column, row]][2]);
                    Console.Write(token);
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("|");
            }

            /* horizontal bar below board and above column numbers */
            Console.SetCursorPosition(2, gameBoardSize["rows"][1] + 1);
            for (int column = 1; column <= gameBoardSize["columns"][1]; column++)
            {
                Console.Write($"|--");
            }
            Console.Write("|");

            /* column numbers */
            Console.SetCursorPosition(2, gameBoardSize["rows"][1] + 2);
            for (int column = 1; column <= gameBoardSize["columns"][1]; column++)
            {
                char[] charsToTrim = { ' ' };
                string columnString = column.ToString().Trim(charsToTrim).PadLeft(2);
                Console.Write($"|{columnString}");
            }
            Console.Write("|");
        }

        /* @ToDo */
        private static void UpdateGameBoardDisplay(Dictionary<string, int> move)
        {
            /* column * 3 because each cell takes up 3 positions */
            /* column + 3 because each line starts with 2 spaces */
            /* row + 1 because the top line of the console is left empty */
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), players[move["player"]][2]);
            Console.SetCursorPosition(move["column"] * 3, move["row"]);
            Console.Write(players[move["player"]][1]);
        }
    }
}
