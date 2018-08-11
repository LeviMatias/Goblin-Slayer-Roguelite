using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Pathfinding
{
    class Location
    {
        public int X;
        public int Y;
        public int F; // g + h
        public int G;//distance from starting point
        public int H;// estimated distance from destination
        public Location PrevTile;

    }
}
