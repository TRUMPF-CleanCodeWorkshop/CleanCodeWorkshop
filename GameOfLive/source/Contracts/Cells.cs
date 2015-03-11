using System.Collections.Generic;
using System.Drawing;

namespace GameOfLife.Contracts
{
    public class Cells : List<Point>
    {
        public int Population { get; set; }
        public int Generation { get; set; }
    }
}