namespace GameEngine
{
    public class GameResults
    {
        public string Winner { get; set; }

        public int WinnersTotalForce { get; set; }

        public int RoundsTaken { get; set; }

        public GameResults()
        {
            RoundsTaken = 0;
        }

    }
}