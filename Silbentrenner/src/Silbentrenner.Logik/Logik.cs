using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silbentrenner.Logik
{
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
                        yield return VollstaendigeZeileZurueckgeben(vorigeSilbeWarLetzteDesWortes, ref aktuelleZeile);
                    }
                    aktuelleZeile += silbe;
                    vorigeSilbeWarLetzteDesWortes = wort.Silben.Last() == silbe;
                }
                aktuelleZeile += " ";
            }
            aktuelleZeile = aktuelleZeile.Trim();
            yield return aktuelleZeile;
        }

        private static string VollstaendigeZeileZurueckgeben(bool vorigeSilbeWarLetzteDesWortes, ref string aktuelleZeile)
        {
            aktuelleZeile = aktuelleZeile.Trim();
            if (!vorigeSilbeWarLetzteDesWortes) aktuelleZeile += "-";
            return aktuelleZeile;
            aktuelleZeile = "";
        }

        public static IEnumerable<Wort> TextInWoerterZerlegen(string text)
        {
            return new List<Wort>();
        }

        public static string BereinigenVonLeerzeichen(string text)
        {
            return "";
        }

        public static IEnumerable<Wort> WoerterInSilbenTrennen(IEnumerable<Wort> woerter)
        {
            return new List<Wort>();
        }
    }
}
