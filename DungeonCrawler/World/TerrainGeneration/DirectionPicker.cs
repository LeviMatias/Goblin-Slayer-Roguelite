using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DungeonCrawler.World.TerrainGeneration.Map;

namespace DungeonCrawler.World.TerrainGeneration
{
    class DirectionPicker
    {
        public Point RandomDirection(Point point)
        {
            Direction dir = (Direction)RandomGenerator.IntBetween(0, 3);

            switch (dir)
            {
                case Direction.North:
                    point = new Point(point.X, point.Y + 1);
                    break;
                case Direction.South:
                    point = new Point(point.X, point.Y - 1);
                    break;
                case Direction.East:
                    point = new Point(point.X + 1, point.Y);
                    break;
                case Direction.West:
                    point = new Point(point.X - 1, point.Y);
                    break;

            }

            return point;
        }
    }
}
