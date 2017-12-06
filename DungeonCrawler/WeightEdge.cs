using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    class WeightEdge
    {
        public int weight;
        public Edge edge;
        public Room origin;
        
        public WeightEdge(Edge edge, int weight, Room origin)
        {
            this.edge = edge;
            this.weight = weight;
            this.origin = origin;
        }
    }
}
