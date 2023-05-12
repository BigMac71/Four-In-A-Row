using Four_In_A_Row___OOP_version.Presentation;

namespace Four_In_A_Row___OOP_version.Logic
{
    internal class Move
    {
        public Board Board { get; set; }
        public Player Player { get; set; }
        public int Column { get; set; }
        public int? Row { get; set; }

        public Move(Board board, Player player, int column)
        {
            Board = board;
            Player = player;
            Column = column;
            CalculateRow();
        }

        public void CalculateRow()
        {
            if (this.MoveIsLegal())
            {
                for (int i = this.Board.Columns; i > 0; i--)
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

        /* @ToDo */
        private bool MoveIsLegal()
        {
            if (!this.Board.ColumnIsFull[this.Column])
            {
                Output.Print(/* column out of range */);

                return false;
            }

            if (this.Column < 1 || this.Column > this.Board.Columns)
            {
                Outputter.Print(/* column out of range */);

                return false;
            }
        }
    }
}
