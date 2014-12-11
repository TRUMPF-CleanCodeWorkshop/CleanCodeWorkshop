using ConsoleApplication1;

namespace Trumpf.Katas.MineSweeper
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
