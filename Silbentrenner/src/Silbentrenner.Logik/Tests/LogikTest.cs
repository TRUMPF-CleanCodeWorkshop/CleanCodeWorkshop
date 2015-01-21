using System.ComponentModel;
using System.Diagnostics;
using NUnit;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Silbentrenner.Logik.Tests
{
    using System.Collections.Generic;

    using NHunspell;

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
            var stringMitLeerzeichen = "A B  C   D.       F ";
            var gewuenschtesStringergebnis = "A B C D. F";

            var probandString = Silbentrenner.Logik.Logik.BereinigenVonLeerzeichen(stringMitLeerzeichen);

            Assert.That(probandString, Is.EqualTo(gewuenschtesStringergebnis));
        }

        [Test]
        public void Wort_Mit_Silben_Trennen()
        {
            var eingabeWortMitSilben = new List<Wort>
                {
                    new Wort { Text = "Kaffeefahrt.", Silben = new Queue<string>() }
                };

            var result = Logik.WoerterInSilbenTrennen(eingabeWortMitSilben);

            Assert.That(result.First().Silben.First(), Is.EqualTo("Kaf"));
            Assert.That(result.First().Silben.Skip(1).First(), Is.EqualTo("fee"));
            Assert.That(result.First().Silben.Skip(2).First(), Is.EqualTo("fahr"));
            Assert.That(result.First().Silben.Skip(3).First(), Is.EqualTo("t."));
        }

        [Test]
        public void Wort_Ohne_Silben_Trennen()
        {
            var eingabeWortOhneSilben = new List<Wort>
                {
                    new Wort { Text = "Mensch", Silben = new Queue<string>() }
                };

            var result = Logik.WoerterInSilbenTrennen(eingabeWortOhneSilben);

            Assert.That(result.First().Silben.First(), Is.EqualTo("Mensch"));
        }

        [Test]
        public void Text_Mit_Silben_Trennen()
        {
            var silbentrenner = new Hyphen("hyph_de_DE.dic");

            var silbenqueueVon_Mensch = new Queue<string>();
            silbenqueueVon_Mensch.Enqueue("Mensch");

            var silbenqueueVon_Helga = new Queue<string>();
            silbenqueueVon_Helga.Enqueue("Hel");
            silbenqueueVon_Helga.Enqueue("ga!");

            var silbenqueueVon_Lass = new Queue<string>();
            silbenqueueVon_Lass.Enqueue("Lass");

            var silbenqueueVon_Uns = new Queue<string>();
            silbenqueueVon_Uns.Enqueue("uns");

            var silbenqueueVon_Auf = new Queue<string>();
            silbenqueueVon_Auf.Enqueue("auf");

            var silbenqueueVon_Die = new Queue<string>();
            silbenqueueVon_Die.Enqueue("die");

            
            var eingabeTextMitSilben = new List<Wort>
                {
                    new Wort{ Text = "Mensch", Silben = null },
                    new Wort{ Text = "Helga!", Silben = null },
                    new Wort{ Text = "Lass", Silben = null },
                    new Wort{ Text = "uns", Silben = null },
                    new Wort{ Text = "auf", Silben = null },
                    new Wort{ Text = "die", Silben = null },
                    new Wort{ Text = "Kaffeefahrt.", Silben = null }
                };

            //var ausgabeWortMitSilben = new List<Wort>
            //    {
            //        new Wort { Text = "Kaffeefahrt.", Silben = silbenqueueVon_Kaffeefahrt }
            //    };
            //var ausgabeWortOhneSilben = new List<Wort>
            //    {
            //        new Wort { Text = "Mensch", Silben = silbenqueueVon_Mensch }
            //    };
            //var ausgabeTextMitSilben = new List<Wort>
            //    {
            //        new Wort{ Text = "Mensch", Silben = silbenqueueVon_Mensch },
            //        new Wort{ Text = "Helga!", Silben = silbenqueueVon_Helga },
            //        new Wort{ Text = "Lass", Silben = silbenqueueVon_Lass },
            //        new Wort{ Text = "uns", Silben = silbenqueueVon_Uns },
            //        new Wort{ Text = "auf", Silben = silbenqueueVon_Auf },
            //        new Wort{ Text = "die", Silben = silbenqueueVon_Die },
            //        new Wort{ Text = "Kaffeefahrt.", Silben = silbenqueueVon_Kaffeefahrt }
            //    };

            var ergebnis = silbentrenner.Hyphenate("Kaffeefahrt.");
            Assert.That(silbentrenner.Hyphenate("Kaffeefahrt.").HyphenatedWord, Is.EqualTo("Kaf=fee=fahr=t."));
            Assert.That(silbentrenner.Hyphenate("Mensch").HyphenatedWord, Is.EqualTo("Mensch"));
        
            //Assert.That(Logik.WoerterInSilbenTrennen(eingabeWortOhneSilben).First().Silben.First(), Is.EqualTo(ausgabeWortOhneSilben.First().Silben.First()));
            //Assert.That(Logik.WoerterInSilbenTrennen(eingabeWortMitSilben), Is.EqualTo(ausgabeWortMitSilben));
            //Assert.That(Logik.WoerterInSilbenTrennen(eingabeTextMitSilben), Is.EqualTo(ausgabeTextMitSilben));
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
