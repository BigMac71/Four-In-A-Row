namespace Four_In_A_Row___OOP_version.Logic
{
    internal class Move
    {
        public Board Board { get; set; }
        public Player Player { get; set; }
        public int Column { get; set; }
        public int? Row { get; set; }
        public bool ErrorColumnFull { get; set; } = false;
        public bool ErrorColumnOutOfRange { get; set; } = false;


        public Move(Board board, Player player, int column)
        {
            Board = board;
            Player = player;
            Column = column;
            CalculateRow();
        }

        /* @ToDo */
        public void CalculateRow()
        {
            if (this.MoveIsLegal())
            {
                for (int i = this.Board.Rows; i > 0; i--)
                {
                    if (Board.Cells[Column, i].Number == 0) /* if cell is empty */
                    {
                        /* update gameBoard */
                        Board.Cells[Column, i] = Player;

                        /* if top row, set column status to full */
                        if (i == 1) Board.ColumnIsFull[Column] = true;

                        break; /* no need to continue for-loop since an empty cell was found already */
                    }
                }
            }
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
    }
}
