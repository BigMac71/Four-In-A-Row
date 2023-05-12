using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Four_In_A_Row___OOP_version.Logic
{
    internal class Game
    {
        public const int MINPLAYERS = 2;
        public const int MAXPLAYERS = 4;
        
        public static readonly int defaultPlayerCount = 2;

        public int PlayerCount { get; set; } = defaultPlayerCount;

        public Game() { }
        public Game(int playerCount) 
        { 
            PlayerCount = playerCount; 
        }

        private static bool HasAWinner(Dictionary<string, int> move)
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


    }
}
