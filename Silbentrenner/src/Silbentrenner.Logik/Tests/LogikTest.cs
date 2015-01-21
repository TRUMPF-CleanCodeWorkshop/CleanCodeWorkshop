using NUnit;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Silbentrenner.Logik
{
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
        public void Woerter_In_Silben_Trennen()
        {
            var silbentrenner = new Hyphen("hyph_de_DE.dic");
            
            var silbenqueueVon_Kaffeefahrt = new Queue<string>();
            silbenqueueVon_Kaffeefahrt.Enqueue("Kaf");
            silbenqueueVon_Kaffeefahrt.Enqueue("fee");
            silbenqueueVon_Kaffeefahrt.Enqueue("fahr");
            silbenqueueVon_Kaffeefahrt.Enqueue("t.");

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

            var eingabeWortMitSilben = new List<Wort>
                {
                    new Wort { Text = "Kaffeefahrt.", Silben = null }
                };
            var eingabeWortOhneSilben = new List<Wort>
                {
                    new Wort { Text = "Mensch", Silben = null }
                };
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

            var ausgabeWortMitSilben = new List<Wort>
                {
                    new Wort { Text = "Kaffeefahrt.", Silben = silbenqueueVon_Kaffeefahrt }
                };
            var ausgabeWortOhneSilben = new List<Wort>
                {
                    new Wort { Text = "Mensch", Silben = silbenqueueVon_Mensch }
                };
            var ausgabeTextMitSilben = new List<Wort>
                {
                    new Wort{ Text = "Mensch", Silben = silbenqueueVon_Mensch },
                    new Wort{ Text = "Helga!", Silben = silbenqueueVon_Helga },
                    new Wort{ Text = "Lass", Silben = silbenqueueVon_Lass },
                    new Wort{ Text = "uns", Silben = silbenqueueVon_Uns },
                    new Wort{ Text = "auf", Silben = silbenqueueVon_Auf },
                    new Wort{ Text = "die", Silben = silbenqueueVon_Die },
                    new Wort{ Text = "Kaffeefahrt.", Silben = silbenqueueVon_Kaffeefahrt }
                };

            var ergebnis = silbentrenner.Hyphenate("Kaffeefahrt.");
            Assert.That(silbentrenner.Hyphenate("Kaffeefahrt.").HyphenatedWord, Is.EqualTo("Kaf=fee=fahr=t."));
            Assert.That(silbentrenner.Hyphenate("Mensch").HyphenatedWord, Is.EqualTo("Mensch"));
        
            Assert.That(Logik.WoerterInSilbenTrennen(eingabeWortOhneSilben), Is.EqualTo(ausgabeWortOhneSilben));
            Assert.That(Logik.WoerterInSilbenTrennen(eingabeWortMitSilben), Is.EqualTo(ausgabeWortMitSilben));
            Assert.That(Logik.WoerterInSilbenTrennen(eingabeTextMitSilben), Is.EqualTo(ausgabeTextMitSilben));
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
