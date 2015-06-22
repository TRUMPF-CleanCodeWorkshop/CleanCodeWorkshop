namespace RobotEngine.TheSwarm.Mind
{
    using Contracts;

    public interface ICerebellum
    {
        void ProcessInformation(HiveMemory memory, Surroundings environment, int turn, Drone drone);
    }
}
