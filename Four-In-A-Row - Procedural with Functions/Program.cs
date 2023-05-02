namespace Four_In_A_Row___Procedural_with_Functions
{
    internal class Program
    {
        /********************/
        /* CLASS PROPERTIES */
        /********************/

        /* input variables */
        private static bool validInput = false;
        private static string? rawInput;

        /* constants */
        private const int PLAYERS = 2;
        private const int MINPLAYERS = 2;
        private const int MAXPLAYERS = 4;
        private const int MINCOLUMNS = 4;
        private const int MAXCOLUMNS = 10;
        private const int COLUMNS = 7;
        private const int ROWS = 6;
        private const int MINROWS = 4;
        private const int MAXROWS = 10;
        private const string INPUTCOLOR = "Blue";

        /* default values */
        private static int playerCount = PLAYERS;

        /* define names and tokens for max number of players */
        /* this is ugly and prone to errors when updating the max number of players */
        /* @ToDo - improve */
        private static Dictionary<int, string[]> players = new(4)
        {
            /* key is the player number (0 for empty cell / no player) */
            /* value is a string array: player name, player token, player color */
            {0, new [] { "empty_cell", "  ", "Black" } },
            {1, new [] { "player 1", "[]", "Red" } },
            {2, new [] { "player 2", "()", "Green" } },
            {3, new [] { "player 3", "<>", "Magenta" } },
            {4, new [] { "player 4", "><", "DarkYellow" } }
        };
         
        private static Dictionary<string, int> latestMove = new(3)
        {
            {"player", 0},
            {"column", 0},
            {"row", 0}
        };

        private static Dictionary<string, int[]> gameBoardSize = new(2)
        {
            { "columns", new int[] { MINCOLUMNS, COLUMNS, MAXCOLUMNS } },
            { "rows", new int[] { MINROWS, ROWS, MAXROWS } }
        };
        private static bool[]? isColumnFull;
        private static int currentTurn = 0; /* starts at 0, will be set 1 higher at start of every turn */
        private static int currentMove; /* the column in which a token has last been placed */
        private static int currentPlayer;
        private static int[,]? gameBoard;

        /*****************/
        /* CLASS METHODS */
        /*****************/

        private static void ResetConsoleConfig()
        {
            Console.Title = "Four-In-A-Row - Procedural Code with Functions";
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ResetColor();
            Console.Clear();
        }

        private static void SetGameBoardDimensions()
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

        private static void CreateEmptyGameBoard()
        {
            /* 0 = empty cel */
            gameBoard = new int[gameBoardSize["columns"][1] + 1, gameBoardSize["rows"][1] + 1];

            for (int column = 1; column <= gameBoardSize["columns"][1]; column++)
            {
                for (int row = 1; row <= gameBoardSize["rows"][1]; row++)
                {
                    gameBoard[column, row] = 0;
                }
            }
        }

        private static void InitializeGameBoard()
        {
            SetGameBoardDimensions();
            InitializeColumnStatus();
            CreateEmptyGameBoard();
        }

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

        private static void InitializeColumnStatus()
        {
            isColumnFull = new bool[gameBoardSize["columns"][1] + 1];
            for (int i = 1; i <= isColumnFull.Length - 1; i++)
            {
                isColumnFull[i] = false;
            }
        }

        private static bool IsGameBoardFull(bool[] isColumnFull)
        {
            bool gameBoardFull = true;
            for (int i = 1; i <= isColumnFull.Length - 1; i++)
            {
                gameBoardFull = gameBoardFull && isColumnFull[i]; /* turns false as soon as 1 column is not full */
            }

            return gameBoardFull;
        }

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

        private static void GetPlayerCount()
        {
            Console.ResetColor();
            Console.Write($"Number of players [{PLAYERS}]: ");
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), INPUTCOLOR);
            validInput = false;
            while (!validInput)
            {
                rawInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(rawInput))
                {
                    validInput = int.TryParse((string)rawInput, out int result);
                    if (validInput) playerCount = Math.Min(MAXPLAYERS, Math.Max(MINPLAYERS, Math.Abs(result))); /* make sure it is a positive integer within min and max bounds */
                }
                /* default value applies if player presses [ENTER] or space(s) */
                else validInput = true;
            }
        }

        private static void GetPlayerNames()
        {
            Console.ResetColor();
            for (int i = 1; i <= playerCount; i++)
            {
                Console.Write($"Name for {players[i][0]}: ");
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), players[i][2]);
                rawInput = Console.ReadLine();
                players[i][0] = rawInput ?? players[i][0];
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        private static void PressAnyKeyToStart()
        {
            Console.ResetColor();
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), INPUTCOLOR);
            Console.Write("Press any key to start the game...");
            Console.ReadKey();
        }

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
                    currentMove = Math.Max(1, Math.Min(Math.Abs(result), gameBoardSize["columns"][1]));
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

                        /* calculate on what row the token stops */
                        /* we start checking at the bottom (highest row number) and move to the top */
                        for (int i = gameBoardSize["rows"][1]; i > 0; i--)
                        {
                            if (gameBoard[currentMove, i] == 0) /* if cell is empty */
                            {
                                /* update gameBoard */
                                gameBoard[currentMove, i] = player;

                                /* if top row, set column status to full */
                                if (i == 1) isColumnFull[currentMove] = true;

                                /* update latest move */
                                latestMove["player"] = player;
                                latestMove["column"] = currentMove;
                                latestMove["row"] = i;
                                break; /* no need to continue for-loop since an empty cell was found already */
                            }
                        }
                    }
                }
            }
        }

        private static bool GameHasAWinner(Dictionary<string, int> move)
        {
            /* check if 4-in-a-row has been made by this latest move */
            /* we need to check 7 out of 8 directions (north doen't need to be checked) */
            /* the token + the number of adjacent tokens of same player in 2 opposite directions, */
            /* needs to be 4 in order to have a win */

            /* initialize starting values */
            int total = 1; /* we always start with a series of size 1 */
            int j = 1; /* the distance 'away' from the current token position we start searching */

            bool thereIsAWinner = false; /* we haven't started searching, so no winner yet */

            /* 1A. north-east */
            while (
                !thereIsAWinner && /* as long as we haven't found a winning series */
                move["column"] + j <= gameBoardSize["columns"][1] && /* we cannot search beyond the rightmost (highest) column */
                move["row"] - j >= 1 && /* we cannot search beyond the top (lowest) row */
                gameBoard[move["column"] + j, move["row"] - j] == move["player"] /* does the cell contain a token of the current player? */
                )
            {
                total++; /* yes: we add 1 to the runnig total */
                j++; /* we will continue the search 1 further away from the currently placed token */
                if (total >= 4) thereIsAWinner = true; /* if running total reaches 4, time for champahne! */
            }

            /* 1B. south-west */
            /* complements north-east, so no need to reset running total */
            j = 1;

            while (
                !thereIsAWinner &&
                move["column"] - j > 0 &&
                move["row"] + j <= gameBoardSize["rows"][1] &&
                gameBoard[move["column"] - j, move["row"] + j] == move["player"]
                )
            {
                total++;
                j++;
                if (total >= 4) thereIsAWinner = true;
            }

            /* 2A. east */
            /* new direction, so need to reset running total */
            j = 1;
            total = 1;

            while (
                !thereIsAWinner &&
                move["column"] + j <= gameBoardSize["columns"][1] &&
                gameBoard[latestMove["column"] + j, latestMove["row"]] == move["player"]
                )
            {
                total++;
                j++;
                if (total >= 4) thereIsAWinner = true;
            }

            /* 2B. west */
            j = 1;

            while (
                !thereIsAWinner &&
                move["column"] - j > 0 &&
                gameBoard[move["column"] - j, move["row"]] == move["player"]
                )
            {
                total++;
                j++;
                if (total >= 4) thereIsAWinner = true;
            }

            /* 3. south */
            /* this direction doen't have a complimentary direction, since north never has to be checked */
            j = 1;
            total = 1;

            while (
                !thereIsAWinner &&
                move["row"] + j <= gameBoardSize["rows"][1] &&
                gameBoard[move["column"], move["row"] + j] == move["player"]
                )
            {
                total++;
                j++;
                if (total >= 4) thereIsAWinner = true;
            }

            /* 4A. north-west */
            j = 1;
            total = 1;

            while (
                !thereIsAWinner &&
                move["column"] - j > 0 &&
                move["row"] - j > 0 &&
                gameBoard[move["column"] - j, move["row"] - j] == move["player"]
                )
            {
                total++;
                j++;
                if (total >= 4) thereIsAWinner = true;
            }

            /* 4B. south-east */
            j = 1;

            while (
                !thereIsAWinner &&
                move["column"] + j <= gameBoardSize["columns"][1] &&
                move["row"] + j <= gameBoardSize["rows"][1] &&
                gameBoard[move["column"] + j, move["row"] + j] == move["player"]
                )
            {
                total++;
                j++;
                if (total >= 4) thereIsAWinner = true;
            }

            return thereIsAWinner;
        }

        /***********************************/
        /* MAIN METHOD = APPLICATION ENTRY */
        /***********************************/

        static void Main()
        {
            ResetConsoleConfig();
            InitializeGameBoard();
            GetPlayerCount();
            GetPlayerNames();

            /**************/
            /* START GAME */
            /**************/

            PressAnyKeyToStart();
            DisplayGameBoard();

            do
            {
                /* since we count starting from 1, we need to add one and start calculating before turn has been increased */
                /* example (with playerCount = 2): 
                 * [turn 1]
                 * currentTurn = 0 => currentPlayer = (0 % 2) + 1 = 1
                 * [turn 2]
                 * currentTurn = 1 => currentPlayer = (1 % 2) + 1 = 2
                 * [turn 3]
                 * currentTurn = 2 => currentPlayer = (2 % 2) + 1 = 1
                 * etc...
                 */
                currentPlayer = (currentTurn % playerCount) + 1;
                currentTurn++; /* new round; starts at 1, not 0 */

                GetPlayerMove(currentTurn, currentPlayer);
                UpdateGameBoardDisplay(latestMove); /* show the newly placed token without rebuilding the whole game board */

                if (GameHasAWinner(latestMove))
                {
                    Console.ResetColor();
                    Console.SetCursorPosition(2, gameBoardSize["rows"][1] + 7);
                    Console.WriteLine($"Proficiat {players[latestMove["player"]][0]}. U hebt gewonnen!");
                }
                else
                {
                    if (IsGameBoardFull(isColumnFull))
                    {
                        Console.SetCursorPosition(2, gameBoardSize["rows"][1] + 7);
                        Console.ResetColor();
                        Console.WriteLine("Er zijn geen zetten meer mogelijk. Het gehele spelbord is opgevuld. Er is geen winnaar!.");
                    }
                }
            }
            while (!GameHasAWinner(latestMove) && !IsGameBoardFull(isColumnFull));

            Console.ReadLine(); /* keep console window open after program ends */
        }
    }
}