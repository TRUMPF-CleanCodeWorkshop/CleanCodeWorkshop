using System;
using System.Collections.Generic;
using System.Linq;
using NHunspell;
using NUnit.Framework;

namespace Silbentrenner.Logik.Tests
{
    [TestFixture]
    public class LogikTest
    {

        [Test]
        public void Ersetze_alle_Zeilenumbrüche_in_einem_Text_durch_Space()
        {
            var text = "123" + Environment.NewLine + "ab" + Environment.NewLine;

            var result = Logik.ErsetzeCRLFDurchSpace(text);

            Assert.That(result, Is.EqualTo("123 ab "));
        }

        [Test]
        public void In_einem_Text_werden_alle_überflüssigen_Leerzeichen_gelöscht()
        {
            var stringMitLeerzeichen = "A B  C   D.       F ";

            var probandString = Logik.BereinigenVonLeerzeichen(stringMitLeerzeichen);

            Assert.That(probandString, Is.EqualTo("A B C D. F"));
        }

        [Test]
        public void Für_ein_mehrsillbiges_Wort_wird_in_Silben_aufgetrennt()
        {
            var wörter = new List<Wort >(){ErstelleWort("Kaffeefahrt.")};

            var result = Logik.WoerterInSilbenTrennen(wörter).First();

            Assert.That(result.Silben.First(), Is.EqualTo("Kaf"));
            Assert.That(result.Silben.Skip(1).First(), Is.EqualTo("fee"));
            Assert.That(result.Silben.Skip(2).First(), Is.EqualTo("fahr"));
            Assert.That(result.Silben.Skip(3).First(), Is.EqualTo("t."));
        }

        [Test]
        public void Für_ein_einsilbiges_Wort_wird_nach_Trennversuch_genau_eine_Silbe_zurückgeliefert()
        {
            var wörter = new List<Wort>() { ErstelleWort("Mensch") };

            var result = Logik.WoerterInSilbenTrennen(wörter);

            Assert.That(result.First().Silben.First(), Is.EqualTo("Mensch"));
        }

        [Test]
        public void Für_alle_Wörter_werden_die_Silben_ermittelt()
        {
            var wörter = new List<Wort>()
                             {
                                 ErstelleWort("Mensch"),
                                 ErstelleWort("Helga!"),
                                 ErstelleWort("Lass"),
                                 ErstelleWort("uns"),
                                 ErstelleWort("auf"),
                                 ErstelleWort("die"),
                                 ErstelleWort("Kaffeefahrt.")
                             };

            var result = Logik.WoerterInSilbenTrennen(wörter).ToList();

            Assert.That(result.First().Silben.First(), Is.EqualTo("Mensch"));
            Assert.That(result.Skip(1).First().Silben.First(), Is.EqualTo("Hel"));
            Assert.That(result.Skip(1).First().Silben.Skip(1).First(), Is.EqualTo("ga!"));
            Assert.That(result.Skip(2).First().Silben.First(), Is.EqualTo("Lass"));
            Assert.That(result.Skip(3).First().Silben.First(), Is.EqualTo("uns"));
            Assert.That(result.Skip(4).First().Silben.First(), Is.EqualTo("auf"));
            Assert.That(result.Skip(5).First().Silben.First(), Is.EqualTo("die"));
            Assert.That(result.Skip(6).First().Silben.First(), Is.EqualTo("Kaf"));
            Assert.That(result.Skip(6).First().Silben.Skip(1).First(), Is.EqualTo("fee"));
            Assert.That(result.Skip(6).First().Silben.Skip(2).First(), Is.EqualTo("fahr"));
            Assert.That(result.Skip(6).First().Silben.Skip(3).First(), Is.EqualTo("t."));
        }

        public void EinTextWirdInEineWoerterlisteUmgewandelt()
        {
            string text = "bla bla trennen baume? ? ...";

            var wörter = Logik.TextInWoerterZerlegen(text).ToList();

            Assert.That(wörter.Count(), Is.EqualTo(6));
            Assert.That(wörter.WieOftKommtWortVor("bla"), Is.EqualTo(2));
            Assert.That(wörter.WieOftKommtWortVor("trennen"), Is.EqualTo(1));
            Assert.That(wörter.WieOftKommtWortVor("baume?"), Is.EqualTo(1));
            Assert.That(wörter.WieOftKommtWortVor("?"), Is.EqualTo(1));
            Assert.That(wörter.WieOftKommtWortVor("..."), Is.EqualTo(1));
        }

        [Test]
        public void WörterZuZeilenZusammensetzen()
        {
            var wörter = ErstelleTestWoerter();

            var result = Logik.WoerterZuZeilenZusammensetzen(wörter, 15).ToList();

            Assert.That(result.Count(), Is.EqualTo(5));
            Assert.That(result.First(), Is.EqualTo("Erwartet wer-"));
            Assert.That(result.Skip(1).First(), Is.EqualTo("den in dem Ski-"));
            Assert.That(result.Skip(2).First(), Is.EqualTo("ort Top-Promis"));
            Assert.That(result.Skip(3).First(), Is.EqualTo("wie Kanzlerin"));
            Assert.That(result.Skip(4).First(), Is.EqualTo("Angie"));

        }

        [Test]
        public void Ein_Zu_langes_Wort_wird_Hart_aufgetrennt()
        {
            var woerter = new List<Wort>() { ErstelleWort("hasudirsogedacht", "hasudirsogedacht") };

            var result = Logik.WoerterZuZeilenZusammensetzen(woerter, 10);

            Assert.That(result.First(), Is.EqualTo("hasudirsog-"));
        }

        private static List<Wort> ErstelleTestWoerter()
        {
            return new List<Wort>()
            {
                ErstelleWort("Erwartet", "Er", "war", "tet"),
                ErstelleWort("werden", "wer", "den"),
                ErstelleWort("in", "in"),
                ErstelleWort("dem", "dem"),
                ErstelleWort("Skiort", "Ski", "ort"),
                ErstelleWort("Top-Promis", "Top-", "Promis"),
                ErstelleWort("wie", "wie"),
                ErstelleWort("Kanzlerin", "Kanz", "ler", "in"),
                ErstelleWort("Angie", "An", "gie"),
            };
        }

        private static Wort ErstelleWort(string Wort)
        {
            return new Wort { Text = Wort, Silben = new Queue<string>() };
        }

        private static Wort ErstelleWort(string Wort, params string[] Silben)
        {
            return new Wort { Text = Wort, Silben = new Queue<string>(Silben) };
        }
    }
}
