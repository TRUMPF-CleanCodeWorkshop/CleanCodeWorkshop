using GameEngineAdapter;
using NUnit.Framework;

namespace GameEngineAdaperTests
{
    public class EngineLoaderTests
    {
        [Test]
        public void Load_lädt_eine_GameEngine_aus_einer_DLL()
        {
            var path = @"C:\Data\Development\Katas\Trumpf CleanCodeWorkshop\RobotTournament\bin\engines";

            var engine = EngineLoader.Load(path);

            Assert.That(engine, Is.Not.Null);

        } 
    }
}
