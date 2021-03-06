﻿namespace Contracts.Model
{
    using System.Drawing;

    public class GameConfiguration
    {
        public Size MapSize{ get; set; }

        public double PowerupPropability { get; set; }

        public int RobotStartLevel { get; set; }

        public int MaxTurns { get; set; }
    }
}
