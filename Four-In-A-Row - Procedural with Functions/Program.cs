namespace Four_In_A_Row___Procedural_with_Functions
{
    internal class Program
    {
        /* input variables */
        private static bool validInput = false;
        private static string? rawInput;

        /* constants */
        private const int minPlayers = 2;
        private const int maxPlayers = 3;
        private const int minColumns = 4;
        private const int maxColumns = 10;
        private const int defaultColumns = 7;
        private const int defaultRows = 6;
        private const int minRows = 4;
        private const int maxRows = 10;

        /* default values */
        private static int numberofPlayers = 2;
        private static string[] playerNames =
        {
            "player 1",
            "player 2",
            "player 3"
        };
        private static string[] playerTokens = new string[3]
        {
            "[]",
            "()",
            "<>"
        };
        private static Dictionary<string, int> latestMove = new(3)
        {
            {"player", 0},
            {"column", 0},
            {"row", 0}
        };
        private static Dictionary<string, int> size = new(2)
        {
            { "columns", defaultColumns },
            { "rows", defaultRows }
        };

        private static bool gameHasEnded = false;
        private static int currentTurn = 0; /* starts at 0, will be set 1 higher at start of every turn */
        private static int currentMove; /* the column in which a token has last been set */
        private static int currentPlayer;

        private static void SetPlayerColor(int playerNumber)
        {
            Console.ForegroundColor = playerNumber switch
            {
                0 => ConsoleColor.Green,
                1 => ConsoleColor.Red,
                2 => ConsoleColor.Blue,
                _ => ConsoleColor.Black,
            };
        }

        private static void GetBoardDimensionsFromInput()
        {
            Console.WriteLine($"Set board dimensions");
            Console.WriteLine($"Defaults are {defaultColumns} columns by {defaultRows} rows.");
            Console.WriteLine($"Allowed range is {minColumns} - {maxColumns} columns by {minRows} - {maxRows} rows.");
            Console.WriteLine("Press [enter] to accept default.");
            Console.WriteLine();
            foreach (string dimension in size.Keys.ToList())
            {
                validInput = false;
                Console.Write($"Number of {dimension} [{size[dimension]}]: ");
                while (!validInput)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    rawInput = Console.ReadLine();

                    Console.ResetColor();
                    if (!string.IsNullOrWhiteSpace(rawInput))
                    {
                        validInput = int.TryParse(rawInput, out int result);
                        if (validInput) size[dimension] = Math.Max(4, Math.Min(Math.Abs(result), 10));
                    }
                    else validInput = true;
                }
                validInput = false;
            }
        }

        static void Main()
        {
            /* clear and set console configuration parameters */
            Console.Clear();
            Console.Title = "Four-In-A-Row - Procedural Code with Functions";
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ResetColor();

            Program.GetBoardDimensionsFromInput();

            /* define array with boolean values for each columns, to indicate full or not */
            /* initialize with all false values */
            bool[] isColumnFull = new bool[size["columns"]];
            bool isGameBoardFull = false;
            for (int i = 0; i < isColumnFull.Length; i++)
            {
                isColumnFull[i] = false;
            }

            /* Console Input - number of players */
            Console.Write($"Number of players [2]: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            rawInput = Console.ReadLine();
            Console.ResetColor();
            while (!validInput)
            {
                if (!string.IsNullOrWhiteSpace(rawInput)) /* default value applies if player presses [ENTER] or space(s) */
                {
                    validInput = int.TryParse((string)rawInput, out int result);
                    if (validInput) numberofPlayers = Math.Min(maxPlayers, Math.Max(minPlayers, Math.Abs(result))); /* make sure it is a positive integer within min and max bounds */
                }
                else validInput = true;
            }
            validInput = false;

            /* Console Input - player names */
            for (int i = 0; i < numberofPlayers; i++)
            {
                Console.Write($"Name for {playerNames[i]}: ");
                Program.SetPlayerColor(i);
                rawInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(rawInput)) /* default value applies if player presses [ENTER] or space(s) */
                {
                    playerNames[i] = rawInput;
                }
                Console.ResetColor();
            }
            Console.WriteLine();

            /*************************************/
            /* create game boards based on sizes */
            /*************************************/

            /* visual board, the one we print to console */
            string[,] visualBoard = new string[size["columns"], size["rows"]];

            for (int column = 0; column < size["columns"]; column++)
            {
                for (int row = 0; row < size["rows"]; row++)
                {
                    visualBoard[column, row] = "  ";
                }
            }

            /* internal board, the one we use to calculate game state */
            /* 0 = empty cel */
            /* x = token player x */
            int[,] gameBoard = new int[size["columns"], size["rows"]];

            for (int column = 0; column < size["columns"]; column++)
            {
                for (int row = 0; row < size["rows"]; row++)
                {
                    gameBoard[column, row] = 0;
                }
            }

            /**************/
            /* START GAME */
            /**************/

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Press any key to start the game...");
            Console.ReadKey();

            /* display game board initially */
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int row = 0; row < size["rows"]; row++)
            {
                for (int column = 0; column < size["columns"]; column++)
                {
                    Console.SetCursorPosition(column * 3 + 2, row + 1);
                    Console.Write("|");
                    string token = visualBoard[column, row];
                    for (int i = 0; i < numberofPlayers; i++)
                    {
                        if (token == playerTokens[i])
                        {
                            Program.SetPlayerColor(i);
                        }
                    }
                    Console.Write(token);
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.Write("|");
            }

            /* horizontal bar below board and above column numbers */
            Console.SetCursorPosition(2, size["rows"] + 1);
            for (int column = 0; column < size["columns"]; column++)
            {
                Console.Write($"|--");
            }
            Console.Write("|");

            Console.SetCursorPosition(2, size["rows"] + 2);
            for (int column = 0; column < size["columns"]; column++)
            {
                Console.Write($"| {column + 1}");
            }
            Console.Write("|");

            while (!gameHasEnded && !isGameBoardFull)
            {
                currentPlayer = currentTurn % numberofPlayers; /* ronde 0 --> player 1 = players[0] */
                currentTurn++; /* new round; starts at 1, not 0 */

                /* Console Input - player X move */
                validInput = false;
                while (!validInput)
                {
                    Console.ResetColor();
                    Console.SetCursorPosition(0, size["rows"] + 4);
                    Console.Write(new String(' ', Console.BufferWidth));
                    Console.Write(new String(' ', Console.BufferWidth));
                    Console.SetCursorPosition(2, size["rows"] + 4);
                    Console.Write($"Ronde {currentTurn}.");
                    Program.SetPlayerColor(currentPlayer);
                    Console.SetCursorPosition(2, size["rows"] + 5);
                    Console.Write($"{playerNames[currentPlayer]} is aan zet: ");
                    rawInput = Console.ReadLine();

                    validInput = int.TryParse((string)rawInput, out int result);
                    if (validInput)
                    {
                        currentMove = Math.Max(1, Math.Min(Math.Abs(result), size["columns"]));
                        if (isColumnFull[currentMove - 1])
                        {
                            validInput = false;
                            Console.SetCursorPosition(2, size["rows"] + 7);
                            Console.ResetColor();
                            Console.WriteLine("Deze kolom is vol. Je kan hier geen token meer plaatsen.");
                        }
                        else
                        {
                            Console.SetCursorPosition(0, size["rows"] + 7);
                            Console.Write(new String(' ', Console.BufferWidth));
                            for (int i = size["rows"] - 1; i >= 0; i--)
                            {
                                if (gameBoard[currentMove - 1, i] == 0)
                                {
                                    gameBoard[currentMove - 1, i] = currentPlayer + 1;
                                    visualBoard[currentMove - 1, i] = playerTokens[currentPlayer];
                                    if (i == 0)
                                    {
                                        isColumnFull[currentMove - 1] = true;
                                    }
                                    latestMove["player"] = currentPlayer;
                                    latestMove["column"] = currentMove - 1;
                                    latestMove["row"] = i;
                                    break;
                                }
                            }
                        }
                    }
                }

                /* update visual board */
                Console.SetCursorPosition(latestMove["column"] * 3 + 3, latestMove["row"] + 1);
                Console.BackgroundColor = ConsoleColor.White;
                Program.SetPlayerColor(currentPlayer);
                Console.Write(playerTokens[currentPlayer]);

                /* check if 4-in-a-row has been made by this latest move */
                /* we need to check 7 out of 8 directions (north doen't need to be checked) */
                /* the token + the number of adjacent tokens of same player in 2 opposite directions, */
                /* needs to be 4 in order to have a win */

                /* initialize starting values */
                int total = 1; /* we always start with a 'row' of size 1 */
                int j = 1;

                /* 1A. north-east */
                while (
                    !gameHasEnded &&
                    latestMove["column"] + j < size["columns"] &&
                    latestMove["row"] - j >= 0 &&
                    gameBoard[latestMove["column"] + j, latestMove["row"] - j] == currentPlayer + 1
                    )
                {
                    total++;
                    j++;
                    if (total >= 4) gameHasEnded = true;
                }

                /* 1B. south-west */
                j = 1;
                while (
                    !gameHasEnded &&
                    latestMove["column"] - j >= 0 &&
                    latestMove["row"] + j < size["rows"] &&
                    gameBoard[latestMove["column"] - j, latestMove["row"] + j] == currentPlayer + 1
                    )
                {
                    total++;
                    j++;
                    if (total >= 4) gameHasEnded = true;
                }

                /* 2A. east */
                j = 1;
                total = 1;

                while (
                    !gameHasEnded &&
                    latestMove["column"] + j < size["columns"] &&
                    gameBoard[latestMove["column"] + j, latestMove["row"]] == currentPlayer + 1
                    )
                {
                    total++;
                    j++;
                    if (total >= 4) gameHasEnded = true;
                }

                /* 2B. west */
                j = 1;

                while (
                    !gameHasEnded &&
                    latestMove["column"] - j >= 0 &&
                    gameBoard[latestMove["column"] - j, latestMove["row"]] == currentPlayer + 1
                    )
                {
                    total++;
                    j++;
                    if (total >= 4) gameHasEnded = true;
                }

                /* 3. south */
                j = 1;
                total = 1;

                while (
                    !gameHasEnded &&
                    latestMove["row"] + j < size["rows"] &&
                    gameBoard[latestMove["column"], latestMove["row"] + j] == currentPlayer + 1
                    )
                {
                    total++;
                    j++;
                    if (total >= 4) gameHasEnded = true;
                }

                /* 4A. north-west */
                j = 1;
                total = 1;

                while (
                    !gameHasEnded &&
                    latestMove["column"] - j >= 0 &&
                    latestMove["row"] - j >= 0 &&
                    gameBoard[latestMove["column"] - j, latestMove["row"] - j] == currentPlayer + 1
                    )
                {
                    total++;
                    j++;
                    if (total >= 4) gameHasEnded = true;
                }

                /* 4B. south-east */
                j = 1;

                while (
                    !gameHasEnded &&
                    latestMove["column"] + j < size["columns"] &&
                    latestMove["row"] + j < size["rows"] &&
                    gameBoard[latestMove["column"] + j, latestMove["row"] + j] == currentPlayer + 1
                    )
                {
                    total++;
                    j++;
                    if (total >= 4) gameHasEnded = true;
                }

                /* we have a winner! */
                if (gameHasEnded)
                {
                    Console.SetCursorPosition(2, size["rows"] + 7);
                    Console.ResetColor();
                    Console.WriteLine($"Proficiat {playerNames[latestMove["player"]]}. U hebt gewonnen!");
                }
                else
                {
                    /* check whether the game board is full */
                    isGameBoardFull = true;
                    for (int i = 0; i < size["columns"]; i++)
                    {
                        isGameBoardFull = isGameBoardFull && isColumnFull[i];
                    }

                    if (isGameBoardFull)
                    {
                        Console.SetCursorPosition(2, size["rows"] + 7);
                        Console.ResetColor();
                        Console.WriteLine("Er zijn geen zetten meer mogelijk. Het gehele spelbord is opgevuld. Er is geen winnaar!.");
                    }
                }
            }

            Console.ReadLine(); /* keep console window open after program ends */
        }
    }
}