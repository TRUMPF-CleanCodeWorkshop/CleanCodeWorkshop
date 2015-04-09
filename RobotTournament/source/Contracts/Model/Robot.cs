using System.Drawing;

namespace Contracts.Model
{
    public class Robot
    {
        public string Id { get; set; }
        public string TeamName { get; set; }
        public Point Position { get; set; }
        public RobotActions CurrentAction { get; set; }
        public int WaitTurns { get; set; }
        public int Level { get; set; }
        public int Age { get; set; }
    }
}