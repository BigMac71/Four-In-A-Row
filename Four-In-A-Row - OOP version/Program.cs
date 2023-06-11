using Four_In_A_Row___OOP_version.Logic;
using Four_In_A_Row___OOP_version.Presentation;

namespace Four_In_A_Row___OOP_version
{
    static class Program
    {
        static void Main()
        {
            ConsoleInput input  = new(2, 2);
            ConsoleOutput output = new(2, 2);

            output.PrintWelcomeMessage();

            int playerCount = input.GetPlayerCount();
            Player[] players = new Player[playerCount];
            input.GetPlayerNames(players);

            Board gameBoard = input.GetBoardDimensions();

            Game game = new(playerCount, gameBoard);

            Move latestMove;

            /**************/
            /* START GAME */
            /**************/

            input.PressAnyKeyToContinue();
            output.DisplayGameBoard(gameBoard);

            do
            {
                latestMove = input.GetPlayerMove(game);
                output.UpdateGameBoardDisplay(latestMove); // show the newly placed token without rebuilding the whole game board

                if (latestMove.WinsTheGame())
                {
                    Console.ResetColor();
                    Console.SetCursorPosition(2, gameBoard.Rows + 7);
                    Console.WriteLine($"Proficiat {latestMove.Player.Name}. U hebt gewonnen!");
                }
                else
                {
                    if (gameBoard.BoardIsFull())
                    {
                        Console.SetCursorPosition(2, gameBoard.Rows + 7);
                        Console.ResetColor();
                        Console.WriteLine("Er zijn geen zetten meer mogelijk. Het gehele spelbord is opgevuld. Er is geen winnaar!.");
                    }
                }
            }
            while (!latestMove.WinsTheGame());

            Console.ReadLine(); // keep console window open after program ends
        }
    }
}