namespace Mockups
{
    using System.Drawing;

    using Contracts.Model;

    public static class TestGameConfiguration
    {
        public static GameConfiguration TestConfiguration()
        {
            return new GameConfiguration()
            {
                MapSize = new Size(5, 5),
                PowerupPropability = 1 / 2,
                RobotStartLevel = 1
            };
        }
    }
}
