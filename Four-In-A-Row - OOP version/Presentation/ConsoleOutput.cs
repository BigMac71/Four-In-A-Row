using Four_In_A_Row___OOP_version.Logic;

namespace Four_In_A_Row___OOP_version.Presentation
{
    public class ConsoleOutput : ConsoleConfig
    {
        public readonly ConsoleColor defaultBackgroundColor = ConsoleColor.Black;
        public readonly ConsoleColor defaultForegroundColor = ConsoleColor.White;
        
        public readonly ColorScheme defaultColorScheme;
        public ColorScheme CurrentColorScheme { get; set; }

        public ConsoleOutput(int left, int top)
        {
            leftCursorPosition = left;
            topCursorPosition = top;
            defaultColorScheme = new(defaultBackgroundColor, defaultForegroundColor);
            CurrentColorScheme = defaultColorScheme;
        }

        public void SetDefaultColors()
        {
            SetColors(defaultBackgroundColor, defaultForegroundColor);
        }

        public void SetColors(ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            CurrentColorScheme = new(backgroundColor, foregroundColor);
            CurrentColorScheme.Implement();
        }

        public void Print(string text, bool crlf = false)
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

        public void PrintBoardDimensionsText()
        {
            Console.SetCursorPosition(leftCursorPosition, topCursorPosition);
            Console.WriteLine($"Set board dimensions");
            Console.WriteLine($"Defaults are {Board.COLUMNS} columns by {Board.ROWS} rows.");
            Console.WriteLine($"Allowed range is {Board.MINCOLUMNS} - {Board.MAXCOLUMNS} columns by {Board.MINROWS} - {Board.MAXROWS} rows.");
            Console.WriteLine("Press [enter] to accept default.");
            Console.WriteLine();
        }

        public void DisplayGameBoard(Board gameBoard)
        {
            Console.Clear();
            SetColors(ConsoleColor.White, ConsoleColor.Black);
            for (int row = 1; row <= gameBoard.Rows; row++)
            {
                for (int column = 1; column <= gameBoard.Rows; column++)
                {
                    Console.SetCursorPosition((column * 3) - 1, row);
                    Console.Write("|");
                    /* gameBoard cells all belong to a player. Empty cells belong to player 'none' */
                    string token = gameBoard.Cells[column, row].Token;
                    Console.ForegroundColor = players[key: gameBoard[column, row]][2]);
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
