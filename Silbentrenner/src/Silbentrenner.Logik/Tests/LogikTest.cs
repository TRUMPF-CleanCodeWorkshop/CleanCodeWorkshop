using System.ComponentModel;
using System.Diagnostics;
using NUnit;
using System;
using System.Collections.Generic;
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
        public void WoerterZuZeilenZusammensetzen()
        {
            var woerter = ErstelleTestWoerter();

            var result = Logik.WoerterZuZeilenZusammensetzen(woerter, 15);


            Assert.That(result.Count(), Is.EqualTo(5));
            Assert.That(result.First(), Is.EqualTo("Erwartet wer-"));
            Assert.That(result.Skip(1).First(), Is.EqualTo("den in dem Ski-"));
            Assert.That(result.Skip(2).First(), Is.EqualTo("ort Top-Promis"));
            Assert.That(result.Skip(3).First(), Is.EqualTo("wie Kanzlerin"));
            Assert.That(result.Skip(4).First(), Is.EqualTo("Angie"));

        }

        private static List<Wort> ErstelleTestWoerter()
        {
            var woerter = new List<Wort>()
            {
                new Wort() {Text = "Erwartet", Silben = new Queue<string>()},
                new Wort() {Text = "werden", Silben = new Queue<string>()},
                new Wort() {Text = "in", Silben = new Queue<string>()},
                new Wort() {Text = "dem", Silben = new Queue<string>()},
                new Wort() {Text = "Skiort", Silben = new Queue<string>()},
                new Wort() {Text = "Top-Promis", Silben = new Queue<string>()},
                new Wort() {Text = "wie", Silben = new Queue<string>()},
                new Wort() {Text = "Kanzlerin", Silben = new Queue<string>()},
                new Wort() {Text = "Angie", Silben = new Queue<string>()},
            };

            woerter[0].Silben.Enqueue("Er");
            woerter[0].Silben.Enqueue("war");
            woerter[0].Silben.Enqueue("tet");

            woerter[1].Silben.Enqueue("wer");
            woerter[1].Silben.Enqueue("den");

            woerter[2].Silben.Enqueue("in");
            woerter[3].Silben.Enqueue("dem");

            woerter[4].Silben.Enqueue("Ski");
            woerter[4].Silben.Enqueue("ort");

            woerter[5].Silben.Enqueue("Top-");
            woerter[5].Silben.Enqueue("Promis");

            woerter[6].Silben.Enqueue("wie");

            woerter[7].Silben.Enqueue("Kanz");
            woerter[7].Silben.Enqueue("ler");
            woerter[7].Silben.Enqueue("in");

            woerter[8].Silben.Enqueue("An");
            woerter[8].Silben.Enqueue("gie");
            return woerter;
        }
    }
}
