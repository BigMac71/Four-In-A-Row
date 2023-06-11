namespace Four_In_A_Row___OOP_version.Logic
{
    public class Board
    {
        public const int MINCOLUMNS = 4;
        public const int MAXCOLUMNS = 10;
        public const int MINROWS = 4;
        public const int MAXROWS = 10;
        public const int COLUMNS = 7;
        public const int ROWS = 6;

        public int Columns { get; set; }
        public int Rows { get; set; }
        public Player[,] Cells { get; set; }
        public bool[] ColumnIsFull { get; set; }

        // constructs a gameBoard with default number of rows and columns
        public Board() : this(COLUMNS, ROWS) { }

        // constructs a gameBoard with a specific number of rows and columns
        public Board(int columns, int rows)
        {
            Columns = columns;
            Rows = rows;

            // initialize column status to not full
            // cells on column 0 are not initialized
            ColumnIsFull = new bool[columns + 1];
            for (int i = 1; i <= columns; i++)
            {
                ColumnIsFull[i] = false;
            }

            // initialize all cells as being empty (player 0) 
            // cells on row 0 and column 0 are not initialized
            Cells = new Player[columns + 1, rows + 1];
            for (int i = 1; i <= columns; i++)
            {
                for (int j = 1; j <= rows; j++)
                {
                    Cells[i, j] = Game.Players[0];
                }
            }
        }

        public bool BoardIsFull()
        {
            bool gameBoardIsFull = true;
            for (int i = 1; i <= ColumnIsFull.Length - 1; i++)
            {
                // turns false as soon as a column is not full
                gameBoardIsFull = gameBoardIsFull && ColumnIsFull[i];
            }

            return gameBoardIsFull;
        }

        public void Update(Move move)
        {
            Cells[move.Column, move.Row] = move.Player;
            CheckColumnFull(move);
        }

        public void CheckColumnFull(Move move)
        {
            if (move.Row == 1)
            {
                ColumnIsFull[move.Column] = true;
            }
        }

        public bool ColumnIsInValidRange(int column)
        {
            return column >= 1 && column <= Columns;
        }
    }
}
