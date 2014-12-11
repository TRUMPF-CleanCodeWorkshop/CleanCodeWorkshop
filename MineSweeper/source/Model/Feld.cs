namespace Trumpf.Katas.MineSweeper.Model
{
    public class Feld
    {
        public int x { get; set; }
        public int y { get; set; }
        public int AnzahlNachbarMienen { get; set; }
        public bool IstMiene { get; set; }
    }
}
