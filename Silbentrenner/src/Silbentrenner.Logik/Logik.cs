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
            return new List<String>();
        }

        public static IEnumerable<Wort> TextInWoerterZerlegen(string text)
        {
            return text.Split(' ').Select(W => new Wort() {Text = W}).ToList();
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
