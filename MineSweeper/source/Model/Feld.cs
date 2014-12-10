using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Model
{
    public class Feld
    {
        public int x { get; set; }
        public int y { get; set; }
        public int AnzahlNachbarMienen { get; set; }
        public bool IstMiene { get; set; }
    }
}
