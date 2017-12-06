using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    class Room
    {

        private Dictionary<EdgeOptions, Edge> neighbors;
        private bool _isUp;
        private int _x, _y;

        // Getters and setters
        public bool IsUp { get => _isUp; set => _isUp = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }

        public Room(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool addNeighbor(EdgeOptions direction, Edge edge)
        {
            if (!this.neighbors.ContainsKey(direction))
            {
                this.neighbors.Add(direction, edge);
                return true;
            }
            return false;
        }

        public Edge GetNeighbor(EdgeOptions key)
        {
            if (this.neighbors.ContainsKey(key))
            {
                return this.neighbors[key];
            }
            return null;
        }
        public Dictionary<EdgeOptions, Edge> GetNeighbors()
        {
            return this.neighbors;
        }


        public int GetNrOfNeighbors()
        {
            return this.neighbors.Count;
        }

    }
}
