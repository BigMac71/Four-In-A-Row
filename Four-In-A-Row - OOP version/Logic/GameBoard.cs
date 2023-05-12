using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Four_In_A_Row___OOP_version.Logic
{
    internal class GameBoard
    {
        private const int MINCOLUMNS = 4;
        private const int MAXCOLUMNS = 10;
        private const int MINROWS = 4;
        private const int MAXROWS = 10;
        private const int COLUMNS = 7;
        private const int ROWS = 6;

        private int Columns { get; set; }
        private int Rows { get; set; }

        public Collection<Cell> Cells { get; set; }

        public GameBoard(int columns, int rows)
        {
            Columns = columns;
            Rows = rows;


        }
    }
}
