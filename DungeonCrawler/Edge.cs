using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    class Edge
    {
        int weight = 0;
        private Room a;
        private Room b;
        private bool collapsed = false;

        public Edge(Room a, Room b, EdgeOptions direction)
        {
            this.a = a;
            this.b = b;          
        }

        public void collapse()
        {
            this.collapsed = true;
        }

        public Room getOpposite(Room room)
        {
            if(room == a)
            {
                return b;
            } else if (room == b)
            {
                return a;
            }

            return null;
        }

        public bool isCollapsed()
        {
            return this.collapsed;
        }
    }
}
