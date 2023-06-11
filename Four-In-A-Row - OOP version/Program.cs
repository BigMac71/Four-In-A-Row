using Four_In_A_Row___OOP_version.Logic;
using Four_In_A_Row___OOP_version.Presentation;

namespace Four_In_A_Row___OOP_version
{
    static class Program
    {
        static void Main()
        {
            ConsoleConfig.Reset();
            ConsoleOutput.PrintWelcomeMessage();

            int playerCount = ConsoleInput.GetPlayerCount();

            Board gameBoard = ConsoleInput.GetBoardDimensions();

            Game game = new(playerCount, gameBoard);

            game.Players = ConsoleInput.GetPlayerNames(game.Players);

            Move latestMove;

            /**************/
            /* START GAME */
            /**************/

            ConsoleInput.PressAnyKeyToContinue();
            ConsoleOutput.DisplayGameBoard(gameBoard);

            do
            {
                latestMove = ConsoleInput.GetPlayerMove(game);
                
                // update game and board with latest Move
                game.Moves.Add(latestMove);
                game.GameBoard.Update(latestMove);

                // show the newly placed token without rebuilding the whole game board
                ConsoleOutput.UpdateGameBoardDisplay(latestMove);

                // if latestMove wins the game,
                // quit the loop and give feedback
                if (latestMove.WinsTheGame())
                {
                    Console.ResetColor();
                    Console.SetCursorPosition(2, gameBoard.Rows + 7);
                    Console.WriteLine($"Proficiat {latestMove.Player.Name}. U hebt gewonnen!");
                    break;
                }

                // if there's no winner, but the gameboard has become completely full,
                // quit the loop and give feedback
                if (gameBoard.BoardIsFull())
                {
                    Console.SetCursorPosition(2, gameBoard.Rows + 7);
                    Console.ResetColor();
                    Console.WriteLine("Er zijn geen zetten meer mogelijk. Het gehele spelbord is opgevuld. Er is geen winnaar!.");
                    break;
                }
            }
            while (!latestMove.WinsTheGame());

            ConsoleInput.PressAnyKeyToContinue();
        }
    }
}