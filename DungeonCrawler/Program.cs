using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Dungeon dngcrwlr = new Dungeon(25, 25);

            Room rooma = new Room(0, 0);
            Room roomb = new Room(0, 1);

            Edge edgeA = new Edge(rooma, roomb);

            rooma.addNeighbor(EdgeOptions.EAST, edgeA);

            System.Console.ReadKey();

        }
    }
}
