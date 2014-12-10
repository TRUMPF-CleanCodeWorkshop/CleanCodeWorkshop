using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Model;

namespace ConsoleApplication1
{
    public class SchummelRechner
    {
        public static IEnumerable<string> BerechneSchummelLösung(IEnumerable<string> Linien)
        {
            var mienen = SucheDieMienen(Linien);
            var feldGröße = BerechneFeldgröße(Linien);

            var felder = BerechneNachbarmienenFürAlleFelder(mienen, feldGröße);

            return ErzeugeAusgabeFormat(felder, feldGröße);
        }

        internal static IEnumerable<string> ErzeugeAusgabeFormat(IEnumerable<Feld> Felder, Size FeldGröße)
        {
            throw new NotImplementedException();
        }

        internal static IEnumerable<Feld> BerechneNachbarmienenFürAlleFelder(IEnumerable<Miene> Mienen, Size FeldGröße)
        {
            throw new NotImplementedException();
        }

        internal static Size BerechneFeldgröße(IEnumerable<string> Linien)
        {
            var height = Linien.Count();
            var width = Linien.First().Length;

            return new Size(width, height);
        }

        internal static IEnumerable<Miene> SucheDieMienen(IEnumerable<string> Linien)
        {
            return new List<Miene>();
        }
    }
}
