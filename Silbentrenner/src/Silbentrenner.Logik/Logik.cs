using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silbentrenner.Logik
{
    using System.Text.RegularExpressions;

    using NHunspell;

    public class Logik
    {
        public static string ErsetzeCRLFDurchSpace(string text)
        {
            return text.Replace(Environment.NewLine, " ");
        }

        public static IEnumerable<String> WoerterZuZeilenZusammensetzen(IEnumerable<Wort> wörter, int zeilenlänge)
        {
            var aktuelleZeile = "";
            var trennzeichenNötig = true;

            foreach (var wort in wörter)
            {
                foreach (var silbe in wort.Silben)
                {
                    if ((aktuelleZeile.Length + silbe.Length) >= zeilenlänge)
                    {
                        yield return VollständigeZeileZurückgeben(trennzeichenNötig, aktuelleZeile);
                        aktuelleZeile = "";
                    }
                    aktuelleZeile += silbe;
                    trennzeichenNötig = wort.Silben.Last() != silbe;
                }
                aktuelleZeile += " ";
            }

            yield return VollständigeZeileZurückgeben(false, aktuelleZeile); ;
        }

        private static string VollständigeZeileZurückgeben(bool trennzeichenNötig, string aktuelleZeile)
        {
            aktuelleZeile = aktuelleZeile.Trim();

            if (trennzeichenNötig)
            {
                aktuelleZeile += "-";
            }

            return aktuelleZeile;
        }

        public static IEnumerable<Wort> TextInWoerterZerlegen(string text)
        {
            return text.Split(' ').Select(W => new Wort() {Text = W}).ToList();
        }

        public static string BereinigenVonLeerzeichen(string text)
        {
            var rgx = new Regex("\\s+");

            var cleanedString = rgx.Replace(text, " ");

            return cleanedString.TrimEnd(' ');
        }

        public static IEnumerable<Wort> WoerterInSilbenTrennen(IEnumerable<Wort> woerter)
        {
            var silbentrenner = new Hyphen("hyph_de_DE.dic");
            return woerter.Select(Wort => FügeSilbenDemWortHinzu(silbentrenner,Wort)).ToList();
        }

        private static Wort FügeSilbenDemWortHinzu(Hyphen Silbentrenner, Wort Wort)
        {
            var hyphenationResult = Silbentrenner.Hyphenate(Wort.Text);

            Wort.Silben = new Queue<string>(hyphenationResult.HyphenatedWord.Split('='));

            return Wort;
        }
    }
}
