using Four_In_A_Row___OOP_version.Logic;

namespace Four_In_A_Row___OOP_version.Presentation
{
    public class ConsoleInput : ConsoleConfig
    {
        public static readonly string defaultBackgroundColor = "Black";
        public static readonly string defaultForegroundColor = "Blue";
        public static readonly ColorScheme defaultColorScheme = new(defaultBackgroundColor, defaultForegroundColor);

        public ColorScheme currentColorScheme;

        private static string? rawInput;

        private ConsoleOutput Output {get; set; }

        public ConsoleInput(int left, int top)
        {
            leftCursorPosition = left;
            topCursorPosition = top;
            Output = new ConsoleOutput(left, top);
            currentColorScheme = defaultColorScheme;
        }

        public static void SetDefaultColors()
        {
            defaultColorScheme.Implement();
        }

        public void SetColors(string backgroundColor, string foregroundColor)
        {
            currentColorScheme = new(backgroundColor, foregroundColor);
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
                /* default value applies if player presses [ENTER] or space(s) */
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

                currentColorScheme.ChangeForegroundColorTo(players[i].Color);
                rawInput = Console.ReadLine();
                players[i].Name = rawInput ?? players[i].Name;
                Console.ResetColor();
            }
            Output.Print("", true);
        }

        private static void GetBoardDimensions()
        {
            Console.WriteLine($"Set board dimensions");
            Console.WriteLine($"Defaults are {COLUMNS} columns by {ROWS} rows.");
            Console.WriteLine($"Allowed range is {MINCOLUMNS} - {MAXCOLUMNS} columns by {MINROWS} - {MAXROWS} rows.");
            Console.WriteLine("Press [enter] to accept default.");
            Console.WriteLine();
            foreach (string dimension in gameBoardSize.Keys.ToList())
            {
                validInput = false;
                Console.Write($"Number of {dimension} [{gameBoardSize[dimension][1]}]: ");
                while (!validInput)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    rawInput = Console.ReadLine();

                    Console.ResetColor();
                    if (!string.IsNullOrWhiteSpace(rawInput))
                    {
                        validInput = int.TryParse(rawInput, out int result);
                        if (validInput) gameBoardSize[dimension][1] = Math.Max(gameBoardSize[dimension][0], Math.Min(Math.Abs(result), gameBoardSize[dimension][2]));
                    }
                    else validInput = true;
                }
            }
        }

        /* @ToDo */
        private static void GetPlayerMove(int turn, int player)
        {
            Console.ResetColor();

            /* clear console lines */
            Console.SetCursorPosition(0, gameBoardSize["rows"][1] + 4);
            Console.Write(new String(' ', Console.BufferWidth));
            Console.Write(new String(' ', Console.BufferWidth));

            Console.SetCursorPosition(2, gameBoardSize["rows"][1] + 4);
            Console.Write($"Ronde {turn}.");

            validInput = false;
            while (!validInput)
            {
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), players[player][2]);
                Console.SetCursorPosition(2, gameBoardSize["rows"][1] + 5);
                Console.Write(new String(' ', Console.BufferWidth));
                Console.SetCursorPosition(2, gameBoardSize["rows"][1] + 5);
                Console.Write($"{players[player][0]} is aan zet: ");
                rawInput = Console.ReadLine();

                validInput = int.TryParse(rawInput, out int result);
                if (validInput)
                {
                    currentMove = new Move(gameBoard, currentPlayer, result);
                    if (isColumnFull[currentMove])
                    {
                        validInput = false;
                        Console.SetCursorPosition(2, gameBoardSize["rows"][1] + 7);
                        Console.ResetColor();
                        Console.WriteLine("Deze kolom is vol. Je kan hier geen token meer plaatsen.");
                    }
                    else
                    {
                        /* clear console line with potential 'column full' message */
                        Console.SetCursorPosition(0, gameBoardSize["rows"][1] + 7);
                        Console.Write(new String(' ', Console.BufferWidth));

                        currentMove.CalculateRow();
                    }
                }
            }
        }
    }
}
