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
        private Random rand;
        private char[] chars = { 'n', 'o', 'w', 'z' };

        public Dungeon(int x, int y)
        {
            this.x = x;
            this.y = y;
            rand = new Random();
            generateDungeon();
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
                Room newRoom = new Room(xCount, yCount);
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
                System.Console.Write(newRoom.X + "," + newRoom.Y + " ");


                if (xCount == this.x)
                {
                    System.Console.WriteLine();
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
    }
}
