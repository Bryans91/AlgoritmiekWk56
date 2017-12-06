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
            printDungeon();
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

        private void printDungeon()
        {
            
        }
    }
}
