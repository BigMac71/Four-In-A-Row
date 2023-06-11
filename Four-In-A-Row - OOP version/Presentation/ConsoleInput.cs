using Four_In_A_Row___OOP_version.Logic;

namespace Four_In_A_Row___OOP_version.Presentation
{
    public class ConsoleInput : ConsoleConfig
    {
        public readonly ConsoleColor defaultBackgroundColor = ConsoleColor.Black;
        public readonly ConsoleColor defaultForegroundColor = ConsoleColor.Blue;
        
        public readonly ColorScheme defaultColorScheme;
        public ColorScheme CurrentColorScheme {get; set; }

        public ConsoleOutput Output { get; set; }

        private string? rawInput;

        public ConsoleInput(int left, int top)
        {
            defaultColorScheme = new(defaultBackgroundColor, defaultForegroundColor);
            CurrentColorScheme = defaultColorScheme;

            leftCursorPosition = left;
            topCursorPosition = top;
            Output = new ConsoleOutput(left, top);
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

        public void PressAnyKeyToContinue()
        {
            Output.Print("Press any key to start the game... ");
            Console.ReadKey();
        }

        public int GetPlayerCount()
        {
            SetDefaultColors();
            Output.Print($"Number of players (${Game.MINPLAYERS} - ${Game.MAXPLAYERS}) [{Game.defaultPlayerCount}]: ");
            SaveCursorPosition(Console.CursorLeft, Console.CursorTop);
            
            do 
            {
                SetCursorPosition(leftCursorPosition, topCursorPosition);
                rawInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(rawInput))
                {
                    if (int.TryParse((string)rawInput, out int playerCount))
                    {
                        if (playerCount < Game.MINPLAYERS) 
                        {
                            SetCursorPosition(2, topCursorPosition + 2);
                            Output.Print($"Ongeldig aantal spelers. Het minimum aantal spelers is {Game.MINPLAYERS}", true);
                            continue;
                        }
                        if (playerCount > Game.MAXPLAYERS)
                        {
                            SetCursorPosition(2, topCursorPosition + 2);
                            Output.Print($"Ongeldig aantal spelers. Het maximum aantal spelers is {Game.MAXPLAYERS}", true);
                            continue;
                        }
                        return playerCount;
                    }
                    SetCursorPosition(1, topCursorPosition + 2);
                    Output.Print($"Ongeldig aantal spelers. Gelieve een getal in te geven tussen {Game.MINPLAYERS} en {Game.MAXPLAYERS}", true);
                }
                // default value applies if player presses [ENTER] or space(s)
                else return Game.defaultPlayerCount;
            } 
            while (true);
        }

        public void GetPlayerNames(Player[] players )
        {
            SetDefaultColors();
            for (int i = 1; i <= players.Length; i++)
            {
                SetCursorPosition(2, topCursorPosition + 2);
                Output.Print($"Name for {players[i]}: ");

                CurrentColorScheme.ForegroundColor = players[i].Color;
                rawInput = Console.ReadLine();
                players[i].Name = rawInput ?? players[i].Name;
                Console.ResetColor();
            }
            Output.Print("", true);
        }

        public Board GetBoardDimensions()
        {
            Output.PrintBoardDimensionsText();

            int columns = GetDimension("columns", Board.COLUMNS, Board.MINCOLUMNS, Board.MAXCOLUMNS);
            int rows = GetDimension("rows", Board.ROWS, Board.MINROWS, Board.MAXROWS);

            return new Board(columns, rows);
        }

        private int GetDimension(string dimension, int defaultValue, int minValue, int maxValue)
        {
            SetDefaultColors();
            Output.Print($"Number of {dimension} [{defaultValue}]: ");
            
            rawInput = Console.ReadLine();
            bool validInput = int.TryParse(rawInput, out int result);

            // if input is valid (can be cast to int)
            //     we calculate if it's absolute value is within min and max bounds,
            //     if that is within bounds, that is returned
            //     if not, minValue is returned if abs(input) < minValue, or maxValue if abs(input) > maxValue
            // if input is not valid, the defaultValue is returned.
            return validInput ? Math.Max(minValue, Math.Min(Math.Abs(result), maxValue)) : defaultValue;
        }

        public Move GetPlayerMove(Game game)
        {
            int currentTurn = game.Moves.Count + 1;
            Player currentPlayer = game.GetCurrentPlayer();
            Move currentMove;
            bool validInput;

            Console.ResetColor();

            /* clear console lines */
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
