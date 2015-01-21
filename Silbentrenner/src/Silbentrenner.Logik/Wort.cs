using System.Collections.Generic;

namespace Silbentrenner.Logik
{
    public class Wort
    {
        public string Text { get; set; }
        public Queue<string> Silben { get; set; }
    }
}