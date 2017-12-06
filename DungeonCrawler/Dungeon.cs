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
        private Room[,] dungeon;
        private Random rand;
        private char[] chars = { 'n', 'o', 'w', 'z' };

        public Dungeon(int x, int y)
        {
            this.x = x;
            this.y = y;
            rand = new Random();
        }

        private void generateDungeon()
        {
           
            this.startRoom = new Room(-1,-1);
            this.endRoom = new Room(-2,-2);
            this.dungeon = new Room[this.x,this.y];

            //generate field
            int xCount = 0;
            int yCount = 0;
            for (int i = 0; i < (this.x * this.y); i++)
            {
                dungeon[xCount, yCount] = new Room(xCount,yCount);
                xCount++;
         
                if(xCount == this.x)
                {
                    xCount = 0;
                    yCount++;
                }       
            }

            
           
            xCount = 0;
            yCount = 0;
            Room current = null;
            Room temp = null;

            
            for (int i = 0; i < (this.x * this.y); i++)
            {
                current = dungeon[xCount, yCount];
                
              //set all delete 1 random



                //if(current.getNrOfNeighbors() == 0)
                //{
                //    if(yCount == 0)
                //    {
                //        //no north
                //    }

                //    if(yCount == this.y)
                //    {
                //        //no south
                //    }

                //    if(xCount == 0)
                //    {
                //        //no west
                //    }
                //    if(xCount == this.x)
                //    {
                //        //no east
                //    }
                    
                //}
                

               

                if (xCount == this.x)
                {
                    xCount = 0;
                    yCount++;
                }
            }






        }


    }
}
