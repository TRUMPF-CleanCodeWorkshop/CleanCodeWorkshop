namespace Silbentrenner.Logik
{
    using System.Collections.Generic;
    using System.Linq;

    public static class TextWirdInWoerterlisteUmgewandeltHelper
    {
        public static int WieOftKommtWortVor(this IEnumerable<Wort> woerterliste, string wort)
        {
            return woerterliste.Count(w => w.Text == wort);
        }
    }
}