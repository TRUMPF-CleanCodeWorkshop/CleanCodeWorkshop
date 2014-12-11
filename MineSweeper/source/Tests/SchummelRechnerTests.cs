using System.Collections.Generic;
using NUnit.Framework;

namespace Trumpf.Katas.MineSweeper.Tests
{
    public class SchummelRechnerTests
    {

        [Test]
        public void BerechneFeldgröße_gibt_die_Größe_3x3_aus_für_3_Linien_mit_drei_Zeichen()
        {
            var linien = new List<string>() {"   ", "   ", "   "};

            var result = SchummelRechner.BerechneFeldgröße(linien);

            Assert.That(result.Width, Is.EqualTo(3));
            Assert.That(result.Height, Is.EqualTo(3));
        }
    }
}
