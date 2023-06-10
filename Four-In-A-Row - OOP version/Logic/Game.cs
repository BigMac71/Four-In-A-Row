namespace Four_In_A_Row___OOP_version.Logic
{
    public class Game
    {
        public const int MINPLAYERS = 2;
        public const int MAXPLAYERS = 4;
        
        public static readonly int defaultPlayerCount = 2;

        public static Player[] Players { get; } =
{
            new Player(0, "empty cell", "  ", ConsoleColor.Black),
            new Player(1, "player 1", "[]", ConsoleColor.Red),
            new Player(2, "player 2", "()", ConsoleColor.Green),
            new Player(3, "player 3", "<>", ConsoleColor.Magenta),
            new Player(4, "player 4", "><", ConsoleColor.DarkYellow)
        };

        public static Player[]? ActivePlayers { get; set; }
        
        public Game() : this(defaultPlayerCount) { }

        public Game(int playerCount) 
        { 
            ActivePlayers = new Player[playerCount + 1];
            ActivePlayers = Players.Take(playerCount + 1).ToArray();
        }
    }
}
