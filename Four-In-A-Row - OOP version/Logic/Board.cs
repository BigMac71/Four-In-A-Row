using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Board() : this(COLUMNS, ROWS) { }

        public Board(int columns, int rows)
        {
            Columns = columns;
            Rows = rows;

            /* initialize column status to not full */
            ColumnIsFull = new bool[columns + 1];
            for (int i = 1; i <= columns; i++)
            {
                ColumnIsFull[i] = false;
            }

            /* initialize all cells as being empty (player 0) */
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
                gameBoardIsFull = gameBoardIsFull && ColumnIsFull[i]; /* turns false as soon as 1 column is not full */
            }

            return gameBoardIsFull;
        }
    }
}
