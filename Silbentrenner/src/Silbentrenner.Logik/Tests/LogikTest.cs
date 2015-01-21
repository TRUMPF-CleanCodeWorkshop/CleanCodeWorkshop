﻿using NUnit;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Silbentrenner.Logik
{
    [TestFixture]
    public class LogikTest
    {

        [Test]
        public void ErsetzeCRLFDurchSpace_zwei_Returns()
        {
            var text = "123" + Environment.NewLine + "ab" + Environment.NewLine;

            var result = Logik.ErsetzeCRLFDurchSpace(text);

            Assert.That(result, Is.EqualTo("123 ab "));

        }

        [Test]
        public void Bereinigen_Von_Leerzeichen()
        {
            
        }

        [Test]
        public void Woerter_In_Silben_Trennen()
        {
            
        }

        [Test]
        public void EinTextWirdInEineWoerterlisteUmgewandelt()
        {
            string text = "bla bla trennen baume? ? ...";

            var woerterliste = Logik.TextInWoerterZerlegen(text);

            Assert.That(woerterliste.Count(), Is.EqualTo(6));
            Assert.That(woerterliste.WieOftKommtWortVor("bla"), Is.EqualTo(2));
            Assert.That(woerterliste.WieOftKommtWortVor("trennen"), Is.EqualTo(1));
            Assert.That(woerterliste.WieOftKommtWortVor("baume?"), Is.EqualTo(1));
            Assert.That(woerterliste.WieOftKommtWortVor("?"), Is.EqualTo(1));
            Assert.That(woerterliste.WieOftKommtWortVor("..."), Is.EqualTo(1));
        }
    }
}
