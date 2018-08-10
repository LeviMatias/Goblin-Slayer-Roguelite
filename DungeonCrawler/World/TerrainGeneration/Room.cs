using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.World.TerrainGeneration
{
    public class Room
    {
        public Point UpperPoint;
        public Point LowerPoint;

        public Room(Point up, Point down)
        {
            UpperPoint = up;
            LowerPoint = down;
        }

        public Point RandomPointInsideRoom()
        {
            int y = RandomGenerator.IntBetween(UpperPoint.X,LowerPoint.X);
            int x = RandomGenerator.IntBetween(UpperPoint.Y, LowerPoint.Y);

            return new Point(x, y);
        }

        internal Point GetCenter()
        {
            return new Point(UpperPoint.Y + (LowerPoint.Y - UpperPoint.Y) / 2, UpperPoint.X + (LowerPoint.X - UpperPoint.X) /2);
        }
    }
}
