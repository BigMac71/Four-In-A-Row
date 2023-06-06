using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Four_In_A_Row___OOP_version.Logic
{
    public class Player
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string Color { get; set; }

        public Player(int number, string name, string token, string color) 
        {
            Number = number;
            Name = name;
            Token = token;
            Color = color;
        }
    }
}
