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
            this.neighbors = new Dictionary<EdgeOptions, Edge>();
            this.X = x;
            this.Y = y;
        }

        public bool addNeighbor(EdgeOptions direction, Room destination)
        {
            if (!this.neighbors.ContainsKey(direction))
            {
                Edge edge = new Edge(this, destination, direction);
                this.neighbors.Add(direction, edge);
                reverseNeighbor(direction, edge);
                return true;
            }
            return false;
        }

        public bool reverseNeighbor(EdgeOptions direction, Edge edge)
        {
            Room b = this.get(direction);

            int directionInt = (int)direction * - 1;
            EdgeOptions reverseDirection = (EdgeOptions)directionInt;
            if (!b.neighbors.ContainsKey(reverseDirection))
            {
                b.neighbors.Add(reverseDirection, edge);

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

        public int GetNrOfNeighbors()
        {
            return this.neighbors.Count;
        }

        public Room get(EdgeOptions direction)
        {
            if(!neighbors.TryGetValue(direction, out Edge edge))
            {
                return null;
            }
            return edge.getOpposite(this);
        }

    }
}
