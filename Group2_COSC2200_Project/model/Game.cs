namespace Group2_COSC2200_Project.model
{
    public class Game
    {
        public List<Player> Players { get; private set; }
        public List<Team> Teams { get; private set; }
        public Deck Deck { get; private set; }

        public Game()
        {
            Players = new List<Player>();
            Teams = new List<Team>();
            Deck = new Deck();
        }

        public void Initialize()
        {
            Players.Add(new Player(1, "Player 1"));
            Players.Add(new Player(2, "Player 2"));
            Players.Add(new Player(3, "Player 3"));
            Players.Add(new Player(4, "Player 4"));

            Teams.Add(new Team(Team.TeamID.TeamOne, new List<Player> { Players[0], Players[2] }));
            Teams.Add(new Team(Team.TeamID.TeamTwo, new List<Player> { Players[1], Players[3] }));

            GameFunctionality.DealCards(Deck, Players);
        }
    }
}
