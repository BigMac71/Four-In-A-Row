using Four_In_A_Row___OOP_version.Logic;
using System.Runtime.CompilerServices;

namespace Four_In_A_Row___OOP_version.Presentation
{
    public static class ConsoleInput
    {
        public static void PressAnyKeyToContinue()
        {
            Console.SetCursorPosition(1, +1);
            ConsoleConfig.SetColors(ConsoleColor.Black, ConsoleColor.White);
            Console.Write("Press any key to continue... ");
            Console.ReadKey();
        }

        public static int GetPlayerCount()
        {
            Console.SetCursorPosition(2, Console.CursorTop + 2);
            ConsoleConfig.SetColors(ConsoleColor.Black, ConsoleColor.White);
            Console.Write("Number of players (");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Game.MINPLAYERS);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Game.MAXPLAYERS);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(") [");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Game.defaultPlayerCount);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("]: ");

            // save input position
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            string? rawInput;

            do 
            {
                Console.SetCursorPosition(left, top);
                Console.ForegroundColor = ConsoleColor.Blue;
                rawInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(rawInput))
                {
                    if (int.TryParse((string)rawInput, out int playerCount))
                    {
                        if (playerCount < Game.MINPLAYERS) 
                        {
                            Console.SetCursorPosition(2, Console.CursorTop + 1);
                            Console.ForegroundColor= ConsoleColor.Red;
                            Console.Write($"Ongeldig aantal spelers. Het minimum aantal spelers is ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(Game.MINPLAYERS);
                            continue;
                        }
                        if (playerCount > Game.MAXPLAYERS)
                        {
                            Console.SetCursorPosition(2, Console.CursorTop + 1);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"Ongeldig aantal spelers. Het maximum aantal spelers is ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(Game.MAXPLAYERS);
                            continue;
                        }
                        return playerCount;
                    }
                    Console.SetCursorPosition(2, Console.CursorTop + 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Ongeldig aantal spelers. Gelieve een getal in te geven tussen ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(Game.MINPLAYERS);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" en ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(Game.MAXPLAYERS);
                }
                // default value applies if player presses [ENTER] or space(s)
                else return Game.defaultPlayerCount;
            } 
            while (true);
        }

        public static Player[] GetPlayerNames(Player[] players )
        {
            string? rawInput;
            for (int i = 1; i < players.Length; i++)
            {
                Console.SetCursorPosition(2, Console.CursorTop + 1);
                ConsoleConfig.SetColors(ConsoleColor.Black, ConsoleColor.White);
                Console.Write($"Name for {players[i].Name}: ");
                
                Console.ForegroundColor = players[i].Color;
                rawInput = Console.ReadLine();
                players[i].Name = rawInput ?? players[i].Name;
            }

            return players;
        }

        public static Board GetBoardDimensions()
        {
            ConsoleOutput.PrintBoardDimensionsText();

            int columns = GetDimension("columns", Board.COLUMNS, Board.MINCOLUMNS, Board.MAXCOLUMNS);
            int rows = GetDimension("rows", Board.ROWS, Board.MINROWS, Board.MAXROWS);

            return new Board(columns, rows);
        }

        private static int GetDimension(string dimension, int defaultValue, int minValue, int maxValue)
        {
            Console.SetCursorPosition(2, Console.CursorTop + 1);
            ConsoleConfig.SetColors(ConsoleColor.Black, ConsoleColor.White);
            Console.Write($"Number of {dimension} [");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(defaultValue);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("]: ");

            string? rawInput = Console.ReadLine();
            bool validInput = int.TryParse(rawInput, out int result);

            // if input is valid (can be cast to int)
            //     we calculate if it's absolute value is within min and max bounds,
            //     if that is within bounds, that is returned
            //     if not, minValue is returned if abs(input) < minValue, or maxValue if abs(input) > maxValue
            // if input is not valid, the defaultValue is returned.
            return validInput ? Math.Max(minValue, Math.Min(Math.Abs(result), maxValue)) : defaultValue;
        }

        public static Move GetPlayerMove(Game game)
        {
            int currentTurn = game.Moves.Count + 1;
            Player currentPlayer = game.GetCurrentPlayer();
            Move currentMove;
            string? rawInput;
            bool validInput;

            Console.ResetColor();

            // clear console lines
            Console.SetCursorPosition(0, game.GameBoard.Rows + 4);
            Console.Write(new String(' ', Console.BufferWidth));
            Console.Write(new String(' ', Console.BufferWidth));

            Console.SetCursorPosition(2, game.GameBoard.Rows + 4);
            Console.Write($"Ronde {currentTurn}.");

            do
            {
                Console.ForegroundColor = currentPlayer.Color;
                Console.SetCursorPosition(2, game.GameBoard.Rows + 5);
                Console.Write(new String(' ', Console.BufferWidth));
                Console.SetCursorPosition(2, game.GameBoard.Rows + 5);
                Console.Write($"{currentPlayer.Name} is aan zet: ");

                rawInput = Console.ReadLine();
                validInput = int.TryParse(rawInput, out int result);

                if (validInput)
                {
                    currentMove = new Move(game.GameBoard, currentPlayer, result);
                    if (game.GameBoard.ColumnIsFull[currentMove.Column])
                    {
                        Console.SetCursorPosition(2, game.GameBoard.Rows + 7);
                        Console.ResetColor();
                        Console.WriteLine("Deze kolom is vol. Je kan hier geen token meer plaatsen.");
                    }
                    else if (game.GameBoard.ColumnIsInValidRange(result))
                    {
                        // clear console line with potential 'column full' message
                        Console.SetCursorPosition(0, game.GameBoard.Rows + 7);
                        Console.Write(new String(' ', Console.BufferWidth));

                        return currentMove;
                    }
                    else
                    {
                        Console.SetCursorPosition(2, game.GameBoard.Rows + 7);
                        Console.ResetColor();
                        Console.WriteLine($"Dit is een ongeldig kolomnummer. Vul een nummer in tussen 1 en {game.GameBoard.Columns}.");
                    }
                }
            } while (true);
        }
    }
}
