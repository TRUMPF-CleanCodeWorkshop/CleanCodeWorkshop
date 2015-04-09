using GameEngineAdapter;
using NUnit.Framework;

namespace GameEngineAdaperTests
{
    public class EngineLoaderTests
    {
        [Test]
        public void Load_lädt_eine_GameEngine_aus_einer_DLL()
        {
            var path = @"..\..\..\..\bin\engines";

            var engine = EngineLoader.Load(path);

            Assert.That(engine, Is.Not.Null);

        } 
    }
}
