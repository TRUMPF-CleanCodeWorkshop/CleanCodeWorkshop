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

        public static IEnumerable<String> WoerterZuZeilenZusammensetzen(IEnumerable<Wort> woerter, int zeilenlaenge)
        {
            var aktuelleZeile = "";
            var vorigeSilbeWarLetzteDesWortes = false;

            foreach (var wort in woerter)
            {
                foreach (var silbe in wort.Silben)
                {
                    if ((aktuelleZeile.Length + silbe.Length) >= zeilenlaenge)
                    {
                        yield return VollstaendigeZeileZurueckgeben(vorigeSilbeWarLetzteDesWortes, aktuelleZeile);
                        aktuelleZeile = "";
                    }
                    aktuelleZeile += silbe;
                    vorigeSilbeWarLetzteDesWortes = wort.Silben.Last() == silbe;
                }
                aktuelleZeile += " ";
            }
            aktuelleZeile = aktuelleZeile.Trim();
            yield return aktuelleZeile;
        }

        private static string VollstaendigeZeileZurueckgeben(bool vorigeSilbeWarLetzteDesWortes, string aktuelleZeile)
        {
            aktuelleZeile = aktuelleZeile.Trim();
            if (!vorigeSilbeWarLetzteDesWortes) aktuelleZeile += "-";
            return aktuelleZeile;
        }

        public static IEnumerable<Wort> TextInWoerterZerlegen(string text)
        {
            return text.Split(' ').Select(W => new Wort() {Text = W}).ToList();
        }

        public static string BereinigenVonLeerzeichen(string text)
        {
            string pattern = "\\s+";
            string replacement = " ";
            Regex rgx = new Regex(pattern);
            var cleanedString = rgx.Replace(text, replacement);

            return cleanedString.TrimEnd(' ');
        }

        public static IEnumerable<Wort> WoerterInSilbenTrennen(IEnumerable<Wort> woerter)
        {
            var silbentrenner = new Hyphen("hyph_de_DE.dic");
            var wortMitSilben = "Kaffeefahrt";
            var libTest = silbentrenner.Hyphenate(wortMitSilben);
            //// Ergebnis : Kaf=fee=fahrt
            


            return new List<Wort>();
        }
    }
}
