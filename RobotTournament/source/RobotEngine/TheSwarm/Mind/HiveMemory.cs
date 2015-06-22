namespace RobotEngine.TheSwarm.Mind
{
    using System.Collections.Generic;

    using Contracts;

    public class HiveMemory
    {
        public HiveMemory()
        {
            this.CurrentDroneActions = new Dictionary<long, NextRobotTurn>();
        }

        public Dictionary<long, NextRobotTurn> CurrentDroneActions { get; private set; }

        public int StrongestKnownEnemy { get; set; }

        public int StrongestDroneLevel { get; set; }

        public int? TurnsPerUpgrade { get; set; }

        public int? TurnsPerSplit { get; set; }
    }
}
