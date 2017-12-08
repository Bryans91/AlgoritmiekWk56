﻿using System;
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
        private Room playerPosition;
        private Room upStairs;
        public List<Room> allRooms = new List<Room>();
        public List<Edge> allEdges = new List<Edge>();
        private Random rand;

        public Dungeon(int x, int y, int start, int end)
        {
            this.x = x;
            this.y = y;
            rand = new Random();
            generateDungeon(start, end);
         //   this.ExplodeBenny(initialRoom, 100000000);
            this.collapseEdges(initialRoom, 10,0);
            gameLoop();
           
        }

        private void gameLoop()
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.UpArrow && playerPosition.Y != 0)
                {
                    movePlayer(EdgeOptions.NORTH);
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow && playerPosition.Y != this.y - 1)
                {
                    movePlayer(EdgeOptions.SOUTH);
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow && playerPosition.X != 0)
                {
                    movePlayer(EdgeOptions.WEST);
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow && playerPosition.X != this.x - 1)
                {
                    movePlayer(EdgeOptions.EAST);
                }
                else if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    Console.WriteLine("Steps to steps: " + BreadthFirstSearch(playerPosition));
                }
            }
        }

        private void movePlayer(EdgeOptions direction)
        {
            playerPosition.roomName = playerPosition.originalName;
            playerPosition = playerPosition.get(direction);
            playerPosition.roomName = 'P';
            printMap();
        }

        private void printMap()
        {
            Console.Clear();

            int xCount = 0;
            Room tempRoom = initialRoom;
            for (int i = 0; i < (this.x * this.y); i++)
            {
                xCount++;

                Console.Write(tempRoom.roomName + " ");
                if(xCount == this.x)
                {
                    Console.WriteLine();
                    xCount = 0;
                }
                tempRoom = tempRoom.NextRoom;
            }
        }

        private void generateDungeon(int start, int end)
        {
            //this.startRoom = new Room(0, 0);
            //this.endRoom = new Room(49, 49);
            //this.dungeon = new Room[this.x,this.y];

            //generate field
            int xCount = 0;
            int yCount = 0;

            int randX = rand.Next(0, this.x);
            int randY = rand.Next(0, this.y);

            Room westRoom = null;
            Room northRoom = null;
            Room initialRowRoom = null;
            Room previousRoom = null;
            for (int i = 0; i < (this.x * this.y); i++)
            {
                char roomName = 'O';
                int randdd = rand.Next(0, 10);
                Room newRoom = new Room(xCount, yCount, randdd, roomName);

                if (i == start)
                {
                    startRoom = newRoom;
                    playerPosition = newRoom;
                    newRoom.roomName = 'P';
                    newRoom.originalName = 'S';
                }
                else if (i == end)
                {
                    endRoom = newRoom;
                    newRoom.roomName = 'X';
                    newRoom.originalName = 'X';
                }

                Edge tempEdge = null;
                allRooms.Add(newRoom);

                if (i == 0)
                {
                    initialRowRoom = newRoom;
                    initialRoom = newRoom;
                    westRoom = newRoom;
                    previousRoom = newRoom;
                } else
                {
                    newRoom.PreviousRoom = previousRoom;
                    previousRoom.NextRoom = newRoom;
                    previousRoom = newRoom;
                }

                if (xCount == 0)
                {
                    northRoom = initialRowRoom;
                    initialRowRoom = newRoom;
                    westRoom = newRoom;
                }

                if (xCount > 0)
                {
                    tempEdge = newRoom.addNeighbor(EdgeOptions.WEST, westRoom);
                    westRoom = newRoom;
                }
                if (yCount > 0)
                {
                    tempEdge = newRoom.addNeighbor(EdgeOptions.NORTH, northRoom);
                    northRoom = northRoom.get(EdgeOptions.EAST);
                }
                if (tempEdge != null) this.allEdges.Add(tempEdge);

                xCount++;

                if (newRoom.X == randX && newRoom.Y == randY)
                {
                    newRoom.roomName = 'U';
                    newRoom.originalName = 'U';
                    upStairs = newRoom;
                    newRoom.IsUp = true;
                }

                Console.Write(newRoom.roomName + " ");

                if (xCount == this.x)
                {
                    Console.WriteLine();
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
            Dictionary<Room, Room> visited = new Dictionary<Room, Room>();

            Room previous = null;
            int steps = 0;

            queue.Enqueue(root);

            Console.WriteLine("Should be a number: " + queue.Count);

            while (queue.Count > 0)
            {
                Room current = queue.Dequeue();
                if (current == null || visited.ContainsKey(current))
                {
                    continue;
                }

                Console.WriteLine("Checking room: " + current.roomName);

                //add neighbors to queue
                foreach (KeyValuePair<EdgeOptions, Edge> edge in current.GetNeighbors())
                {
                    if (edge.Value.A != current && !visited.Values.Contains(edge.Value.A)) queue.Enqueue(edge.Value.A);
                    if (edge.Value.B != current && !visited.Values.Contains(edge.Value.B)) queue.Enqueue(edge.Value.B);
                }

                visited.Add(current, previous);
                previous = current;

                current.roomName = 'B';

                if (current.IsUp)
                {
                    Room key = previous;
                    while (key != root)
                    {
                        if (visited.ContainsKey(key))
                        {
                            key = visited[key];
                            steps++;
                        }
                    }

                    printMap(); // Dit kun je weghalen om te zien welke rooms hij scant
                    return steps;
                }
                //endwhile
            }

            return -1;
        }

        public bool ExplodeBenny(Room root, int collapseNr)
        {
            //List<Edge> openEdges = new List<Edge>();
            List<WeightEdge> openEdges = new List<WeightEdge>();
            List<Edge> closedEdges = new List<Edge>();
            List<Room> visitedRooms = new List<Room>();
            List<Room> allRoomsInFunction = allRooms;

            visitedRooms.Add(root);
            Room current = root;
            allRoomsInFunction.Remove(current);

            while (allRoomsInFunction.Count > 0)
            {
                Dictionary<EdgeOptions, Edge> neighbours = current.GetNeighbors();

                foreach (KeyValuePair<EdgeOptions, Edge> edge in current.GetNeighbors())
                {
                    openEdges.Add(new WeightEdge(edge.Value, edge.Value.Weight, current));
                }


                WeightEdge lowestWe = null;
                foreach (WeightEdge we in openEdges)
                {
                    if (!closedEdges.Contains(we.edge) && (lowestWe == null || we.weight < lowestWe.weight))
                    {
                        if (lowestWe == null && (!visitedRooms.Contains(we.edge.A) || !visitedRooms.Contains(we.edge.B)))
                        {
                            lowestWe = we;
                        }
                        else if (!visitedRooms.Contains(we.edge.A) || !visitedRooms.Contains(we.edge.B))
                        {
                            lowestWe = we;
                        }
                    }
                }

                closedEdges.Add(lowestWe.edge);
                current = lowestWe.edge.getOpposite(lowestWe.origin);
                //visitedRooms.Add(current);
                visitedRooms.Add(lowestWe.edge.A);
                visitedRooms.Add(lowestWe.edge.B);
                allRoomsInFunction.Remove(current);
                openEdges.Remove(lowestWe);
                Console.WriteLine(lowestWe.weight + ": " + lowestWe.edge.A.roomName + " -> " + lowestWe.edge.B.roomName);
            }

            System.Console.Write("I WANT TO BREAK FREEEE");
            // Add all edges of C to openlist

            // add C -> D to closed

            // Add all edges of D to openList (except D -> C, already in closed)

            System.Console.WriteLine();
            System.Console.WriteLine();

            foreach (Edge printEdge in closedEdges)
            {
                Console.WriteLine(printEdge.A.roomName + " <-> " + printEdge.B.roomName + " = " + printEdge.Weight);
            }

            return false;
        }

        public List<Edge> mstBryan(Room root)
        {

            
            List<Edge> mst = new List<Edge>();
            List<Room> allRooms = this.allRooms;
            List<Room> visited = new List<Room>();

            List<Edge> tempAllEdges = new List<Edge>();

            //init vars
            Room current;
            root.tempLvl = 0;

            int i = 0;
            while (allRooms.Count > 0)
            {
                current = null;
                //select lowest key and set as current 
                foreach (Room r in allRooms)
                {
                    if (current == null || r.tempLvl < current.tempLvl) current = r;
                }

                //remove from loop
                allRooms.Remove(current);

                if (current.tempParent != null)
                {
                    Edge add = null;
                    //add edge to mst
                    foreach (Edge e in tempAllEdges)
                    {
                        if ((e.B == current || e.A == current) && (e.B == current.tempParent || e.A == current.tempParent))
                        {
                            if (add == null || add.Weight < e.Weight)
                            {
                                add = e;
                            }
                           
                        }
                    }

                    mst.Add(add);
                }

                foreach (KeyValuePair<EdgeOptions, Edge> edge in current.GetNeighbors())
                {
                    Edge tempEdge = edge.Value;
                    //get direction
                    Room target;
                    if (tempEdge.A == current)
                    {
                        target = tempEdge.B;
                    }
                    else
                    {
                        target = tempEdge.A;
                    }

                    //if a path is shorter than current temp add to list
                    if (tempEdge.Weight < target.tempLvl)
                    {
                        target.tempLvl = tempEdge.Weight;
                        target.tempParent = current;
                        tempAllEdges.Add(tempEdge);
                    } 
                }
                i++;
                visited.Add(current);
            }

            //MST IS NON DELETE LIST

            //reset all rooms
            allRooms = this.allRooms;
            foreach (Room r in allRooms)
            {
                r.tempLvl = 9999999;
                r.tempParent = null;
            }

            //handle expl
            return mst;
        }


        public bool collapseEdges(Room root,int nrOfCollapse,int floor)
        {
            //get edgelists
            List<Edge> mst = this.mstBryan(root);
            List<Edge> tempAll = this.allEdges;

            //SHUFFLE EDGELIST
            this.Shuffle(tempAll);

            int i = 0;
            foreach (Edge e in tempAll)
            {
                //TODO:: add check for floors later
                if(!e.isCollapsed() && !mst.Contains(e) && i < nrOfCollapse)
                {
                    e.collapse();
                    i++;
                }
            }
      
            if (i > 0) return true;
            return false;
        }

        //Fisher yates shuffle
        public void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = this.rand.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }

        
}
