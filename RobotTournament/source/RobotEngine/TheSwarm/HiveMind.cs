namespace RobotEngine.TheSwarm
{
    using System;
    using System.Collections.Generic;

    using Contracts;

    using RobotEngine.TheSwarm.Mind;

    public static class HiveMind
    {
        private static long droneIdCounter = 0;
        private static readonly object LockObj = new object();

        private static readonly ISet<ICerebellum> Brain = new HashSet<ICerebellum>();

        private static readonly HiveMemory Memory = new HiveMemory();

        public static void GrowBrain()
        {
            Brain.Add(new SettingsDiscovery());
        }

        public static long NextDroneId()
        {
            lock (LockObj)
            {
                return ++droneIdCounter;
            }
        }

        public static void NotifyAboutNextAction(long id, NextRobotTurn nextTurn)
        {
            lock (Memory)
            {
                if (Memory.CurrentDroneActions.ContainsKey(id))
                {
                    throw new InvalidOperationException("Can not register next action if last one is still in list!");
                }

                Memory.CurrentDroneActions.Add(id, nextTurn);
            }
        }

        public static void NotifyAboutFinishedAction(long id)
        {
            lock (Memory)
            {
                if (!Memory.CurrentDroneActions.Remove(id))
                {
                    throw new InvalidOperationException("No current action registered for this id: " + id);
                }
            }
        }

        public static void Learn(Drone drone, int currentTurn, Surroundings environment)
        {
            foreach (var cerebellum in Brain)
            {
                cerebellum.ProcessInformation(Memory, environment, currentTurn, drone);
            }
        }
    }
}
