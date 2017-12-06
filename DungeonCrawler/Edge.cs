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
            this.A = a;
            this.b = b;            
        }

       
        internal Room A { get => a; set => a = value; }
        internal Room B { get => b; set => b = value; }

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
