namespace Four_In_A_Row___OOP_version.Logic
{
    public class Move
    {
        public Board Board { get; set; }
        public Player Player { get; set; }
        public int Column { get; set; }
        public int Row { get; set; } = 0; // zero works a lot easier than allowing Row to be nullable
        public bool ErrorColumnFull { get; set; } = false;
        public bool ErrorColumnOutOfRange { get; set; } = false;


        public Move(Board board, Player player, int column)
        {
            Board = board;
            Player = player;
            Column = column;
            Row = CalculateRow();
        }

        public int CalculateRow()
        {
            if (this.MoveIsLegal())
            {
                for (int i = this.Board.Rows; i > 0; i--)
                {
                    if (Board.Cells[Column, i].Number == 0) // if cell is empty
                    {
                        Board.Update(this); // we can already update the game board with this new legal move

                        return i; // no need to continue since an empty cell was found already
                    }
                }
            }

            return 0; // the move is not legal, so row remains zero
        }

        private bool MoveIsLegal()
        {
            if (this.Board.ColumnIsFull[this.Column])
            {
                ErrorColumnFull = true;

                return false;
            }

            if (this.Column < 1 || this.Column > this.Board.Columns)
            {
                ErrorColumnOutOfRange = true;

                return false;
            }

            return true;
        }

        // check if 4-in-a-row has been made by the latest move
        public bool WinsTheGame()
        {
            // if Row is still zero (because e.g. move is not legal), we cannot start checking if this move is winning the game
            // this design pattern is called 'early return'
            // it involves returning as soon as possible when a certain condition can be checked
            // this makes code more readable because often, a lot of (nested) if..then conditions are avoided this way
            if (Row == 0) return false;

            // we need to check 7 out of 8 directions (north doen't need to be checked for obvious reasons)
            // the token + the number of adjacent tokens of the same player in 2 opposite directions,
            // needs to be 4 in order to have a winner

            // initialize starting values
            int runningTotal = 1; // we always start with a series of size 1
            int j = 1; // the distance 'away' from the current token position we start searching

            // 1A. north-east
            while (
                Column + j <= Board.Columns && // we cannot search beyond the rightmost (highest) column
                Row - j >= 1 && // we cannot search beyond the top (lowest) row
                Board.Cells[Column + j, (int)(Row - j)] == Player // does the cell contain (a token of) the current player?
                )
            {
                runningTotal++; // yes: we add 1 to the runnig total
                j++; // we will continue the search 1 further away from the currently placed token
                if (runningTotal >= 4) return true; // if running total reaches 4, time for champagne!
            }

            // 1B. south-west
            // complements north-east, so no need to reset the running total
            j = 1;
            while (
                Column - j > 0 &&
                Row + j <= Board.Rows &&
                Board.Cells[Column - j, Row + j] == Player
                )
            {
                runningTotal++;
                if (runningTotal >= 4) return true;
                j++;
            }

            // 2A. east
            // new direction, so we need to reset the running total
            j = 1;
            runningTotal = 1;
            while (
                Column + j <= Board.Columns &&
                Board.Cells[Column + j, Row] == Player
                )
            {
                runningTotal++;
                if (runningTotal >= 4) return true;
                j++;
            }

            // 2B. west
            j = 1;
            while (
                Column - j > 0 &&
                Board.Cells[Column - j, Row] == Player
                )
            {
                runningTotal++;
                if (runningTotal >= 4) return true;
                j++;
            }

            // 3. south
            // this direction doen't have a complimentary direction, since north never has to be checked
            j = 1;
            runningTotal = 1;
            while (
                Row + j <= Board.Rows &&
                Board.Cells[Column, Row + j] == Player
                )
            {
                runningTotal++;
                if (runningTotal >= 4) return true;
                j++;
            }

            // 4A. north-west
            j = 1;
            runningTotal = 1;
            while (
                Column - j > 0 &&
                Row - j > 0 &&
                Board.Cells[Column - j, Row - j] == Player
                )
            {
                runningTotal++;
                if (runningTotal >= 4) return true;
                j++;
            }

            // 4B. south-east
            j = 1;

            while (
                Column + j <= Board.Columns &&
                Row + j <= Board.Rows &&
                Board.Cells[Column + j, Row + j] == Player
                )
            {
                runningTotal++;
                if (runningTotal >= 4) return true;
                j++;
            }

            // we are done searching. If no winning series has been found by now, it wasn't a winning move, alas.
            return false;
        }
    }
}
