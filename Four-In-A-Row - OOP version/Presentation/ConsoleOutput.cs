using Four_In_A_Row___OOP_version.Logic;
using System.Data.Common;

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

        public void PrintWelcomeMessage()
        {
            Print("Welcome to 4-in-a-Row, the classic game.", true);
            Print("", true);
        }

        public void PrintBoardDimensionsText()
        {
            Console.SetCursorPosition(leftCursorPosition, topCursorPosition);
            Print("Set board dimensions", true);
            Print($"Defaults are {Board.COLUMNS} columns by {Board.ROWS} rows.", true);
            Print($"Allowed range is {Board.MINCOLUMNS} - {Board.MAXCOLUMNS} columns by {Board.MINROWS} - {Board.MAXROWS} rows.", true);
            Print("Press [enter] to accept default.", true);
            Print("", true);
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
                    Console.ForegroundColor = gameBoard.Cells[column, row].Color;
                    Console.Write(gameBoard.Cells[column, row].Token);
                    Console.ForegroundColor = ConsoleColor.Black;
                }
            Console.Write(" |");
            }

            // horizontal bar below board and above column numbers
            Console.SetCursorPosition(2, gameBoard.Rows + 1);
            for (int column = 1; column <= gameBoard.Columns; column++)
            {
                Console.Write($"|--");
            }
            Console.Write("|");

            /* column numbers */
            Console.SetCursorPosition(2, gameBoard.Rows + 2);
            for (int column = 1; column <= gameBoard.Columns; column++)
            {
                char[] charsToTrim = { ' ' };
                string columnString = column.ToString().Trim(charsToTrim).PadLeft(2);
                Console.Write($"|{columnString}");
            }
            Console.Write("|");
        }

        public void UpdateGameBoardDisplay(Move latestMove)
        {
            /* column * 3 because each cell takes up 3 positions */
            /* column + 3 because each line starts with 2 spaces */
            /* row + 1 because the top line of the console is left empty */
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = latestMove.Player.Color;
            Console.SetCursorPosition(latestMove.Column * 3, latestMove.Row);
            Console.Write(latestMove.Player.Token);
        }
    }
}
