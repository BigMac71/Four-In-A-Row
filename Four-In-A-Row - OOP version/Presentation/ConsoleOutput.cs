using Four_In_A_Row___OOP_version.Logic;
using System.Linq;

namespace Four_In_A_Row___OOP_version.Presentation
{
    public static class ConsoleOutput
    {
        public static void PrintWelcomeMessage()
        {
            // first row, column are on position 0
            Console.SetCursorPosition(0, 1);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            // write leading spaces to avoid having to set cursorPosition each time
            Console.WriteLine("  |--------------------------------------------------|");
            Console.WriteLine("  | Welcome to 4-in-a-Row, the classic tactics game. |");
            Console.WriteLine("  |--------------------------------------------------|");
        }

        public static void PrintBoardDimensionsText()
        {
            // default values
            Console.SetCursorPosition(2, Console.CursorTop + 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Defaults are ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Board.COLUMNS);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" columns by ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Board.ROWS);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" rows.");

            // allowed values
            Console.SetCursorPosition(2, Console.CursorTop);
            Console.Write("Allowed range is ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Board.MINCOLUMNS);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Board.MAXCOLUMNS);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" columns by ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Board.MINROWS);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Board.MAXROWS);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" rows.");

            // Press enter to accept defaults
            Console.SetCursorPosition(2, Console.CursorTop);
            Console.Write("Press [");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("enter");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("] to accept default.");
        }

        public static void DisplayGameBoard(Board gameBoard)
        {
            Console.Clear();
            ConsoleConfig.SetColors(ConsoleColor.White, ConsoleColor.Black);
            for (int row = 1; row <= gameBoard.Rows; row++)
            {
                for (int column = 1; column <= gameBoard.Rows; column++)
                {
                    Console.SetCursorPosition((column * 3) - 1, row);
                    Console.Write("|");

                    // gameBoard cells all belong to a player. Empty cells belong to player 0 ('empty cell')
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

        public static void UpdateGameBoardDisplay(Move latestMove)
        {
            /* column * 3 because each cell takes up 3 positions */
            /* column + 3 because each line starts with 2 spaces */
            /* row + 1 because the top line of the console is left empty */
            Console.SetCursorPosition(latestMove.Column * 3, latestMove.Row);
            ConsoleConfig.SetColors(ConsoleColor.White, latestMove.Player.Color);
            Console.Write(latestMove.Player.Token);
        }
    }
}
