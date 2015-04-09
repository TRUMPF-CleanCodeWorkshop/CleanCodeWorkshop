namespace Core
{
    using System;
    using System.Collections.Generic;

    using Contracts;
    using Contracts.Model;

    public class Framework
    {
        public static GameState CreateInitializeGameState(GameConfiguration configuration, IGameEngine engine, IEnumerable<IRobotEngine> robotEngines)
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

            return result;
        } 
    }
}
