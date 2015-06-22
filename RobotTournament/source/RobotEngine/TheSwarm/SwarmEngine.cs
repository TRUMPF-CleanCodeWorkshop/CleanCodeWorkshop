namespace RobotEngine.TheSwarm
{
    using System.Drawing;

    using Contracts;

    public class SwarmEngine : IRobotEngine
    {
        public SwarmEngine()
        {
            HiveMind.GrowBrain();
        }
        
        public string TeamName
        {
            get { return "TheSwarm"; }
        }

        public IRobot GetNewRobot()
        {
            return new Drone();
        }

        public Color GetTeamColor()
        {
            return Color.DarkMagenta;
        }
    }
}
