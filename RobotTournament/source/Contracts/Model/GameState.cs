using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Model
{
    public class GameState
    {
        public GameConfiguration  Configuration { get; set; }
        public int Turn { get; set; }
        public List<Robot> Robots { get; set; }
        public List<PoperUp> PoperUps { get; set; }

        public GameState()
        {
            Robots = new List<Robot>();
        }
    }
}
