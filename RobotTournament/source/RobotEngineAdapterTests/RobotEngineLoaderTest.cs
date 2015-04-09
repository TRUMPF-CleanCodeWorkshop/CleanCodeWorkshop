using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotEngineAdapter;

namespace RobotEngineAdapterTests
{
    public class RobotEngineLoaderTests
    {
        [Test]
        public void Load_lädt_alle_RobotEngines_aus_mehreren_DLL()
        {
            var path = @"C:\Data\Development\Katas\Trumpf CleanCodeWorkshop\RobotTournament\bin\engines";

            var robots = RobotLoader.Load(path);

            Assert.That(robots, Is.Not.Empty);

        } 
    }
}
