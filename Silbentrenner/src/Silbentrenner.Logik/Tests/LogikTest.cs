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
