using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    class Room
    {
        private Dictionary<char,Room> neighbors;
        private int x, y;
        public Room(int x, int y)
        {
            this.x = x;
            this.y = y;
            neighbors = new Dictionary<char, Room>();
        }


        public bool addNeighbor(char direction, Room room)
        {
            if (!this.neighbors.ContainsKey(direction))
            {
                this.neighbors.Add(direction, room);
                return true;
            }
            return false;
        }

        public bool removeNeighbor(char direction,Room room)
        {
            if (this.neighbors.ContainsKey(direction))
            {
                this.neighbors.Remove(direction);
                return true;
            }
            return false;
        }


        public Room getNeighbor(char key)
        {
            if (this.neighbors.ContainsKey(key))
            {
                return this.neighbors[key];
            }
            return null;
        }

        public int getNrOfNeighbors()
        {
            return this.neighbors.Count;
        }

    }
}
