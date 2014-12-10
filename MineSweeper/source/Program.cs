using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var quellDatei = args[0];
            var zielDatei = args[1];

            var linien = SpielfeldAdapter.LeseQuelldatei(quellDatei);
            var ergebnis = SchummelRechner.BerechneSchummelLösung(linien);

            SchummelDateiAdapter.SpeichereSchummelErgebnis(ergebnis, zielDatei);
        }
    }
}
