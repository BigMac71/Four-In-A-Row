using Four_In_A_Row___OOP_version.Logic;
using Four_In_A_Row___OOP_version.Presentation;

namespace Four_In_A_Row___OOP_version
{
    internal class Program
    {
        public static ConsoleInput Input { get; private set; } = new(2, 2);
        public static ConsoleOutput Output { get; private set;  } = new(2, 2);

        public static Game? Game { get; private set; }
        public static Board? Board { get; private set; }
        public static Move? CurrentMove { get; private set; }

        static void Main()
        {
            /**************/
            /* GAME SETUP */
            /**************/

            // output.WelcomeMessage();
            Game = new Game(Input.GetPlayerCount());

            Input.GetPlayerNames();
            Input.GetBoardDimensions();

            /**************/
            /* START GAME */
            /**************/

            Input.PressAnyKeyToContinue();
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