namespace Contracts.Model
{
    using System.Drawing;

    public class Robot
    {
        private static long idCounter = 1;

        public string Id { get; private set; }

        public string TeamName { get; set; }

        public Point Position { get; set; }

        public RobotActions CurrentAction { get; set; }

        public Directions CurrentDirection { get; set; }

        public int WaitTurns { get; set; }

        public int Level { get; set; }

        public int Age { get; set; }

        public IRobot RobotImplementation { get; set; }

        public Robot(Point position, int level, string teamName, IRobot robotImplementation)
        {
            this.CurrentAction = RobotActions.Idle;
            this.WaitTurns = 0;
            this.Age = 0;
            this.Level = level;
            this.TeamName = teamName;
            this.Id = (idCounter++).ToString();
            this.Position = position;
            this.RobotImplementation = robotImplementation;
        }
    }
}