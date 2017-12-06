using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    class Dungeon
    {
        private int x;
        private int y;
        private Room startRoom;
        private Room endRoom;
        private Room initialRoom;
        public List<Room> allRooms = new List<Room>();
        private Random rand;
        private char[] chars = { 'n', 'o', 'w', 'z' };

        public Dungeon(int x, int y)
        {
            this.x = x;
            this.y = y;
            rand = new Random();
            generateDungeon();
            Explode(initialRoom, 100000000);
        }

        private void generateDungeon()
        {
            //this.startRoom = new Room(0, 0);
            //this.endRoom = new Room(49, 49);
            //this.dungeon = new Room[this.x,this.y];

            //generate field
            int xCount = 0;
            int yCount = 0;

            Room westRoom = null;
            Room northRoom = null;
            Room initialRowRoom = null;
            for (int i = 0; i < (this.x * this.y); i++)
            {
                Room newRoom = new Room(xCount, yCount, rand.Next(1, 10));
                allRooms.Add(newRoom);

                if(i == 0)
                {
                    initialRowRoom = newRoom;
                    initialRoom = newRoom;
                    westRoom = newRoom;
                }

                if(xCount == 0)
                {
                    northRoom = initialRowRoom;
                    initialRowRoom = newRoom;
                    westRoom = newRoom;
                }

                if (xCount > 0)
                {
                    newRoom.addNeighbor(EdgeOptions.WEST, westRoom);
                    westRoom = newRoom;
                }
                if(yCount > 0)
                {
                    newRoom.addNeighbor(EdgeOptions.NORTH, northRoom);
                    northRoom = northRoom.get(EdgeOptions.EAST);
                }

                xCount++;

                if (xCount == this.x)
                {
                    xCount = 0;
                    yCount++;
                }
            }
        }
        
        /**
         *  Seaches for stairs
         **/
        public int BreadthFirstSearch(Room root)
        {
            Queue<Room> queue = new Queue<Room>();
            Dictionary<Room,Room> visited = new Dictionary<Room, Room>();

            Room previous = null;
            int steps = 0;

            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Room current = queue.Dequeue();
                if (current != null) continue;

                //add neighbors to queue
                foreach (KeyValuePair<EdgeOptions, Edge> edge in current.GetNeighbors())
                {
                    if(edge.Value.A != current && visited.Values.Contains(edge.Value.A)) queue.Enqueue(edge.Value.A);
                    if(edge.Value.B != current && visited.Values.Contains(edge.Value.B)) queue.Enqueue(edge.Value.B);
                }
            
                visited.Add(current, previous);
                previous = current;

                if (current.IsUp)
                {
                    Room key = previous;
                    while(key != root)
                    {
                        if (visited.ContainsKey(key))
                        {
                            key = visited[key];
                            steps++;
                        }
                    }

                    return steps;
                }
                //endwhile
            }

            return -1;
        }

        public bool FireInTheHole()
        {

        }

        public bool Explode(Room root, int collapseNr)
        {
            //List<Edge> openEdges = new List<Edge>();
            List<WeightEdge> openEdges = new List<WeightEdge>();
            List<Edge> closedEdges = new List<Edge>();
            List<Room> visitedRooms = new List<Room>();
            List<Room> allRooms = new List<Room>();
            
            visitedRooms.Add(root);
            Room current = root;

            while (allRooms.Count < 0)
            {
                Dictionary<EdgeOptions, Edge> neighbours = current.GetNeighbors();

                foreach (KeyValuePair<EdgeOptions, Edge> edge in current.GetNeighbors())
                {
                    if (!closedEdges.Contains(edge.Value))
                    {
                        openEdges.Add(new WeightEdge(edge.Value, edge.Value.Weight, current));
                    }
                }


                WeightEdge lowestWe = null;
                foreach (WeightEdge we in openEdges)
                {
                    if ((lowestWe == null || we.weight < lowestWe.weight))
                    {
                        lowestWe = we;
                    }
                }
                if (lowestWe != null)
                {
                    closedEdges.Add(lowestWe.edge);
                    current = lowestWe.edge.getOpposite(lowestWe.origin);
                    allRooms.Remove(current);
                    openEdges.Remove(lowestWe);
                } else
                {
                    current = null;
                }
            }

            System.Console.Write("I WANT TO BREAK FREEEE");
            // Add all edges of C to openlist

            // add C -> D to closed

            // Add all edges of D to openList (except D -> C, already in closed)

            return false;
        }

    }
}
