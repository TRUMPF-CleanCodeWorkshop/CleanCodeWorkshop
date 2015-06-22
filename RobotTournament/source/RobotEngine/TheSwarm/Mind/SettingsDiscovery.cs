namespace RobotEngine.TheSwarm.Mind
{
    using System.Collections.Generic;

    using Contracts;
    using Contracts.Model;

    public class SettingsDiscovery : ICerebellum
    {
        private readonly Dictionary<long, int> upgradeStartedTurns = new Dictionary<long, int>();
        private readonly Dictionary<long, int> splitStartedTurns = new Dictionary<long, int>(); 

        public void ProcessInformation(HiveMemory memory, Surroundings environment, int turn, Drone drone)
        {
            if (!memory.TurnsPerSplit.HasValue)
            {
                this.TryToDetermineTurnsPerSplit(memory, drone.Id, turn);
            }

            if (!memory.TurnsPerUpgrade.HasValue)
            {
                this.TryToDetermineTurnsPerUpgrade(memory, drone.Id, turn);
            }
        }

        private void TryToDetermineTurnsPerUpgrade(HiveMemory memory, long droneId, int currentTurn)
        {
            // 1. Get last action of drone
            NextRobotTurn lastAction;
            if (!memory.CurrentDroneActions.TryGetValue(droneId, out lastAction))
            {
                return;
            }

            // 2. Check if it was an upgrade
            if (lastAction.NextAction != RobotActions.Upgrading)
            {
                return;
            }
            
            // 3. Determine round count
            int upgradeStartTurn;
            if (!this.upgradeStartedTurns.TryGetValue(droneId, out upgradeStartTurn))
            {
                this.upgradeStartedTurns.Add(droneId, currentTurn);
                return;
            }

            memory.TurnsPerUpgrade = (currentTurn - upgradeStartTurn);
            this.upgradeStartedTurns.Clear();
            SwarmUtils.Log("TurnsPerUpgrade: " + memory.TurnsPerUpgrade);
        }

        private void TryToDetermineTurnsPerSplit(HiveMemory memory, long droneId, int currentTurn)
        {
            // 1. Get last action of drone
            NextRobotTurn lastAction;
            if (!memory.CurrentDroneActions.TryGetValue(droneId, out lastAction))
            {
                return;
            }

            // 2. Check if it was a split:
            if (lastAction.NextAction != RobotActions.Splitting)
            {
                return;
            }

            // 3. Determine round count
            int splitStartTurn;
            if (!this.splitStartedTurns.TryGetValue(droneId, out splitStartTurn))
            {
                this.splitStartedTurns.Add(droneId, currentTurn);
                return;
            }

            memory.TurnsPerSplit = (currentTurn - splitStartTurn);
            this.splitStartedTurns.Clear();
            SwarmUtils.Log("TurnsPerSplit: " + memory.TurnsPerSplit);
        }
    }
}
