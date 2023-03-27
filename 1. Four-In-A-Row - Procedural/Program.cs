using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Principal;
using MySql.Data.MySqlClient;
using System.Runtime.Serialization.Formatters;

namespace _1.Four_In_A_Row___Procedural
{
    class Program
    {
        static void Main()
        {
            /* MySQL database connection */

            /* 1. connection parameters */
            string _connectionString = "server=localhost; uid=root; pwd=DeepSeaTurtles2022; database=world";

            /* 2. create new MySQL Connection, using the connection parameters */
            MySqlConnection con = new MySqlConnection(_connectionString);
            
            /* 3. open the connection = connect to the MySQL server and database */
            con.Open();

            /* 4. create a MySQL query */
            string query = "SELECT * FROM world.city WHERE Population > 1000000 ORDER BY Population DESC";
            
            /* 5. create the command to execute that query using the previously created MySQL db connection */
            MySqlCommand cmd = new MySqlCommand(query, con);

            /* 6. execute the query and gather the resultset in a MySQL Reader Object */
            MySqlDataReader reader = cmd.ExecuteReader();

            /* <optional> output headers */
            Console.Write("Name");
            Console.SetCursorPosition(30, Console.CursorTop);
            Console.Write("CountryCode");
            Console.SetCursorPosition(45, Console.CursorTop);
            Console.Write("District");
            Console.SetCursorPosition(70, Console.CursorTop);
            Console.WriteLine("Population");
            
            /* 7. output (some of) the fields of the records of the resultset as strings */
            while (reader.Read()) /* Read() reads the next row (or record) from the resultset */
            {
                Console.Write(reader["Name"].ToString());
                Console.SetCursorPosition(30, Console.CursorTop);
                Console.Write(reader["CountryCode"].ToString());
                Console.SetCursorPosition(45, Console.CursorTop);
                Console.Write(reader["District"].ToString());
                Console.SetCursorPosition(70, Console.CursorTop);
                Console.WriteLine(reader["Population"].ToString());
            }

            Console.ReadLine(); /* keep console window open after program ends */

            /*************************************/
            /* declare and initialize local vars */
            /*************************************/

            /* declare and initialize helper vars for input validation and sanitization */
            bool validInput;
            string rawInput;

            /* define look of tokens for player 1 and 2 */
            string[] playerToken = new string[2]
            {
                "\u2B1B", /* large white square */
                "\u2B1A" /* large black square */
            };

            /* set default column and row sizes */
            Dictionary<string, int> sizes = new Dictionary<string, int>(2)
            {
                { "columns", 7 },
                { "rows", 6 }
            };

            /* declare and initialize helper variables for game progress */
            bool gameHasEnded = false;
            int currentTurn = 0;
            int currentMove; /* starts at 0, will be set 1 higher at start of every turn */
            int currentPlayer;
            string[] players =
            {
                "player 1",
                "player 2"
            };

            /* clear and set console configuration parameters */
            Console.Clear();
            Console.Title = "Four-In-A-Row - Procedural Code";
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ResetColor();

            /* Console Input - size of the board */
            Console.WriteLine("Set board dimensions (min 4, max 10, defaults is 7 columns by 6 rows)");
            Console.WriteLine("Press [enter] to accept default.");
            Console.WriteLine();
            foreach (string size in sizes.Keys.ToList())
            {
                validInput = false;
                Console.Write($"Number of {size} [{sizes[size]}]: ");
                while (!validInput)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    rawInput = Console.ReadLine();
                    Console.ResetColor();
                    if (!string.IsNullOrEmpty(rawInput))
                    {
                        validInput = int.TryParse(rawInput, out int result);
                        if (validInput) sizes[size] = Math.Max(4, Math.Min(Math.Abs(result), 10));
                    }
                    else validInput = true;
                }
            }

            /* define array with boolean values for each columns, to indicate full or not */
            /* initialize with all false values */
            bool[] isColumnFull = new bool[sizes["columns"]]; 
            for (int i = 0; i < isColumnFull.Length; i++)
            {
                isColumnFull[i] = false;
            }

            /* Console Input - player names */
            for (int i = 0; i < 2; i++)
            {
                Console.Write($"Name for {players[i]}: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                rawInput = Console.ReadLine();
                Console.ResetColor();
                if (!string.IsNullOrWhiteSpace(rawInput))
                {
                    players[i] = rawInput;
                }
            }
            Console.WriteLine();

            /*************************************/
            /* create game boards based on sizes */
            /*************************************/

            /* visual board, the one we print to console */
            string[,] visualBoard = new string[sizes["columns"], sizes["rows"]];
           
            for (int column = 0; column < sizes["columns"]; column++)
            {
                for (int row = 0; row < sizes["rows"]; row++)
                {
                    visualBoard[column, row] = "\u2B1C";
                }
            }

            /* internal board, the one we use to calculate game state */
            /* 0 = empty cel */
            /* 1 = token player 1 */
            /* 2 = token player 2 */
            int[,] gameBoard = new int[sizes["columns"], sizes["rows"]];

            for (int column = 0; column < sizes["columns"]; column++)
            {
                for (int row = 0; row < sizes["rows"]; row++)
                {
                    gameBoard[column, row] = 0;
                }
            }

            /**************/
            /* START GAME */
            /**************/

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Press any key to start the game...");
            Console.ReadKey();

            while (!gameHasEnded)
            {
                Console.Clear();
                currentPlayer = currentTurn % 2; /* ronde 0 --> player 1 = players[0] */
                currentTurn++; /* new round; starts at 1, not 0 */

                /* display game board */
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                for (int row = 0; row < sizes["rows"]; row++)
                {
                    for (int column = 0; column < sizes["columns"]; column++)
                    {
                        string token = visualBoard[column, row];
                        if (token == "\u2B1B") Console.ForegroundColor = ConsoleColor.Black;
                        if (token == "\u2B1A") Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" " + token);
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.Write(" ");
                    Console.WriteLine();
                }
                Console.ResetColor();

                for (int column = 0; column < sizes["columns"]; column++)
                {
                    Console.Write($" {column}");
                }
                Console.WriteLine();
                Console.WriteLine();

                /* Console Input - player X move */
                Console.WriteLine($"Ronde {currentTurn}.");
                Console.Write($"{players[currentPlayer]} is aan zet: ");

                rawInput = Console.ReadLine();
                if (int.TryParse(rawInput, out int result))
                {
                    currentMove = Math.Min(Math.Abs(result), sizes["columns"]);
                    if (isColumnFull[currentMove]) currentMove = 0; /* cannot drop token in full column */
                }
                else currentMove = 0; /* drop token in first column if input is invalid */

                for (int i = sizes["rows"] - 1; i >= 0;  i--)
                {
                    if (gameBoard[currentMove, i] == 0)
                    {
                        gameBoard[currentMove, i] = currentPlayer + 1;
                        string newToken = playerToken[currentPlayer];
                        visualBoard[currentMove, i] = newToken;
                        break;
                    }
                    isColumnFull[currentMove] = true;
                }
            }

            gameHasEnded = true;
        
            Console.ReadLine(); /* keep console window open after program ends */
        }



    }
}
