namespace Contracts.Model
{
    using System.Collections.Generic;

    public class GameState
    {
        public GameConfiguration Configuration { get; set; }

        public int Turn { get; set; }

        public List<Robot> Robots { get; set; }

        public List<PowerUp> PowerUps { get; set; }

        public GameState()
        {
            this.Robots = new List<Robot>();
            this.PowerUps = new List<PowerUp>();
        }
    }
}
