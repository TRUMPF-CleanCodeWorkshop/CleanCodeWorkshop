namespace RobotEngine.TheSwarm
{
    using System;
    using System.Linq;

    using Contracts;

    using RobotEngine.TheSwarm.Helper;

    public class Drone : IRobot
    {
        private static readonly Random Rand = new Random();

        private bool firstAction = true;

        private int currentTurn = 0;
        private int attacksLastTurn = 0;
        private int flightsLastTurn = 0;

        public long Id { get; private set; }

        public int Level { get; private set; }

        public NextRobotTurn LastAction { get; private set; }

        public NextRobotTurn DoNextTurn(int turn, int myLevel, Surroundings environment)
        {
            SwarmUtils.LogEnabled = false;
            if (this.currentTurn != turn)
            {
                this.attacksLastTurn = 0;
                this.flightsLastTurn = 0;
                this.currentTurn = turn;
            }

            this.Level = myLevel;
            /*if (!this.firstAction)
            {
                //HiveMind.NotifyAboutFinishedAction(this.Id);
            }*/

            this.firstAction = false;

            SurroundingPowerUp powerUp = environment.GetBestPowerUp();
            SurroundingRobot weakestEnemy = environment.GetWeakestEnemy();
            if (weakestEnemy == null)
            {
                if (powerUp != null)
                {
                    return Actions.Move(powerUp.Direction);
                }

                if (turn % 50 == 0 || this.attacksLastTurn > this.flightsLastTurn * 2)
                {
                    return Actions.Split(environment.BestSplitDirection(myLevel));
                }

                var friendCount = environment.Robots.Count(r => !r.IsEnemy);
                if (friendCount >= 4 && (myLevel < turn) && Rand.NextDouble() <= (friendCount + 1) * 12.5d)
                {
                    return Actions.Upgrade();
                }

                return Actions.Split(environment.BestSplitDirection(myLevel));
            }

            if (powerUp != null && ((this.Level + powerUp.Level > environment.EnemiesWhoCanReach(powerUp.Direction).Sum(e => e.Level))))
            {
                return Actions.Move(powerUp.Direction);
            }

            if (weakestEnemy.Level * 2 < this.Level)
            {
                this.attacksLastTurn++;
                return Actions.Split(weakestEnemy.Direction);
            }
            
            if (weakestEnemy.Level < this.Level)
            {
                this.attacksLastTurn++;
                return Actions.Move(weakestEnemy.Direction);
            }

            if (weakestEnemy.Level == this.Level)
            {
                return Actions.Upgrade();
            }

            // Gibt es einen Freund im Umkreis der von einem starken Feind bedroht wird? Sind wir zusammen stark genug? Ja => Hinlaufen
            /*var threatenedFriends = environment.ThreatenedFriendsIMustHelp(myLevel).ToList();
            if (threatenedFriends.Any())
            {
                return Actions.Move(threatenedFriends.First().Direction);
            }*/

            var safeDirections = environment.GetSafeDirections(myLevel).ToList();
            if (safeDirections.Any())
            {
                this.flightsLastTurn++;
                return Actions.Move(safeDirections.First());
            }
            return Actions.Upgrade();

            /*
            result = Actions.Split(environment.FriendAtPos(Directions.N) == null ? Directions.N : Directions.NE);
            SwarmUtils.Log("Drone {0}: Splitting to {1}", this.Id, result.NextDirection);

            //HiveMind.NotifyAboutNextAction(this.Id, result.Copy());
            //HiveMind.Learn(this, turn, environment);
            this.LastAction = result.Copy();
            return result;*/
        }

    }
}
