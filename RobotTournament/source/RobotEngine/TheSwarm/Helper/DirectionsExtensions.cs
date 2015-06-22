namespace RobotEngine.TheSwarm.Helper
{
    using System;
    using System.Collections.Generic;

    using Contracts.Model;

    public static class DirectionsExtensions
    {
        public static Directions OppositeDirection(this Directions direction)
        {
            switch (direction)
            {
                case Directions.N:
                    return Directions.S;
                case Directions.NE:
                    return Directions.SW;
                case Directions.E:
                    return Directions.W;
                case Directions.SE:
                    return Directions.NW;
                case Directions.S:
                    return Directions.N;
                case Directions.SW:
                    return Directions.NE;
                case Directions.W:
                    return Directions.E;
                case Directions.NW:
                    return Directions.SE;
                default:
                    throw new ArgumentOutOfRangeException("direction");
            }
        }

        public static IEnumerable<Directions> GetReachableDirections(this Directions direction)
        {
            switch (direction)
            {
                case Directions.N:
                    yield return Directions.NE;
                    yield return Directions.NW;
                    yield return Directions.E;
                    yield return Directions.W;
                    break;
                case Directions.NE:
                    yield return Directions.N;
                    yield return Directions.E;
                    break;
                case Directions.E:
                    yield return Directions.NE;
                    yield return Directions.SE;
                    yield return Directions.N;
                    yield return Directions.S;
                    break;
                case Directions.SE:
                    yield return Directions.E;
                    yield return Directions.S;
                    break;
                case Directions.S:
                    yield return Directions.SW;
                    yield return Directions.SE;
                    yield return Directions.E;
                    yield return Directions.W;
                    break;
                case Directions.SW:
                    yield return Directions.S;
                    yield return Directions.W;
                    break;
                case Directions.W:
                    yield return Directions.NW;
                    yield return Directions.SW;
                    yield return Directions.N;
                    yield return Directions.S;
                    break;
                case Directions.NW:
                    yield return Directions.W;
                    yield return Directions.N;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction");
            }
        } 
    }
}
