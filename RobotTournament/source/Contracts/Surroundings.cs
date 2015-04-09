using System.Collections.Generic;

namespace Contracts
{
    public class Surroundings   
    {
        public IEnumerable<SurroundingRobot> Type { get; set; }
        public IEnumerable<SurroundingPowerUp> PowerUps { get; set; }
    }
}