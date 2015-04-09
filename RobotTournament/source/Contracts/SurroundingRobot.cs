using System;
using Contracts.Model;

namespace Contracts
{
    public class SurroundingRobot       
    {
        public int Level { get; set; }
        public Boolean IsEnemy { get; set; }
        public Directions Direction { get; set; }
    }
}