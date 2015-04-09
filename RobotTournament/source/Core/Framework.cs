namespace Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Contracts.Model;

    public class Framework
    {
        public static GameState CreateInitializeGameState(GameConfiguration configuration, IGameEngine gameEngine, IEnumerable<IRobotEngine> robotEngines)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            var result = new GameState
            {
                Configuration = configuration,
                Turn = 0,
                PowerUps = new List<PowerUp>(),
                Robots = new List<Robot>()
            };

            var robotEngineList = robotEngines.ToList();
            var positions = gameEngine.GetInitialRobotPositions(configuration.MapSize, robotEngineList.Count).ToList();

            var robots = robotEngineList.Zip(positions, (engine, position) => new Robot(position, configuration.RobotStartLevel, engine.TeamName)).ToList();

            result.Robots = robots;

            return result;
        }
    }
}
