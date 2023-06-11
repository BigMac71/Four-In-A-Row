namespace Four_In_A_Row___OOP_version.Logic
{
    public class Game
    {
        public const int MINPLAYERS = 2;
        public const int MAXPLAYERS = 4;

        public static readonly int defaultPlayerCount = 2;

        public static readonly Player[] defaultPlayers =
{
            new Player(0, "empty cell", "  ", ConsoleColor.Black),
            new Player(1, "player 1", "[]", ConsoleColor.Red),
            new Player(2, "player 2", "()", ConsoleColor.Green),
            new Player(3, "player 3", "<>", ConsoleColor.Magenta),
            new Player(4, "player 4", "><", ConsoleColor.DarkYellow)
        };

        public Player[] Players { get; set; }

        public Board GameBoard { get; set; }

        public List<Move> Moves { get; set; }

        public Game() : this(defaultPlayerCount, new Board()) { }

        public Game(int playerCount, Board gameBoard)
        {
            Players = defaultPlayers.Take(playerCount + 1).ToArray();
            GameBoard = gameBoard;
            Moves = new List<Move>();
        }

        public Player GetCurrentPlayer()
        {
            int index = (Moves.Count) % (Players.Length - 1) + 1;

            return Players[index];
        }
    }
}
