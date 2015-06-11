﻿using System.Drawing;

namespace Contracts.Model
{
    using System.Collections.Generic;

    public class GameState
    {
        public GameConfiguration Configuration { get; set; }

        public int Turn { get; set; }

        public List<Robot> Robots { get; set; }

        public List<PowerUp> PowerUps { get; set; }

        public bool Finished { get; set; }

        public Dictionary<string, Color> TeamColors { get; set; }

        public GameState()
        {
            this.Robots = new List<Robot>();
            this.PowerUps = new List<PowerUp>();
            Finished = false;
        }
    }
}
