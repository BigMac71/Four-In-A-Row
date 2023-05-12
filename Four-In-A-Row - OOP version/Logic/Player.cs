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
        public string Name { get; set; }
        public string Token { get; set; }
        public string Color { get; set; }

        public Player(string name, string token, string color) 
        {
            Name = name;
            Token = token;
            Color = color;
        }
    }
}
