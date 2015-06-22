namespace RobotEngine.TheSwarm
{
    using System;
    using System.IO;

    using Contracts;

    public static class SwarmUtils
    {
        public static NextRobotTurn Copy(this NextRobotTurn turn)
        {
            return new NextRobotTurn()
                       {
                           NextAction = turn.NextAction,
                           NextDirection = turn.NextDirection
                       };
        }

        private static StreamWriter logWriter;

        public static bool LogEnabled { get; set; }
        
        public static StreamWriter LogWriter
        {
            get
            {
                return logWriter ?? (logWriter = new StreamWriter(@"C:\swarm.log", false) { AutoFlush = true });
            }
        }

        public static void Log(string text)
        {
            if (!LogEnabled)
            {
                return;
            }

            LogWriter.WriteLine("{0}: {1}", DateTime.Now.ToString("HH:mm:ss.ffff"), text);
            LogWriter.Flush();
        }

        public static void Log(string text, params object[] args)
        {
            var message = string.Format(text, args);
            Log(message);
        }

    }
}
