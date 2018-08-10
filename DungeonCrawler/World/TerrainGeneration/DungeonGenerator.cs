using DungeonCrawler.World.TerrainGeneration;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DungeonCrawler.World.TerrainGeneration.Map;

namespace DungeonCrawler
{
    class DungeonGenerator
    {
        public Map map;

        public Map Generate()
        {
            map = new Map(70, 70);
            int numberOfRooms = RandomGenerator.IntBetween(10, 16);
            for(int i =0; i < numberOfRooms; i++)
            {
                CreateRoom();
            }
            MakeCorridors();
            ConnectRooms(map.mainRoom, map.rooms.First());
            return map;
        }

        private void CreateRoom()
        {
            int x = RandomGenerator.IntBetween(16, map.cells.GetUpperBound(0) - 16);
            int y = RandomGenerator.IntBetween(16, map.cells.GetUpperBound(1) - 16);
            Point upPoint = new Point(x, y);

            x = RandomGenerator.IntBetween(upPoint.X + 3, map.InsideXBound(upPoint.X + 8));
            y = RandomGenerator.IntBetween(upPoint.Y + 3, map.InsideYBound(upPoint.Y + 8));
            Point downPoint = new Point(x, y);

            Room room = new Room(upPoint, downPoint);
            map.rooms.Add(room);

            for (int i = upPoint.X; i <= downPoint.X; i++)
            {
                for (int j = upPoint.Y; j <= downPoint.Y; j++)
                {
                    if (i == upPoint.X || i == downPoint.X || j == upPoint.Y || j == downPoint.Y)
                    {
                        map.AssignMaterial(map.cells[j, i], Map.TerrainType.Wall);
                        if (map.Halls.Contains(map.cells[j, i])) map.Halls.Remove(map.cells[j, i]);
                    }
                    else map.AssignMaterial(map.cells[j, i], Map.TerrainType.Hall);
                }
            }
        }

        private void MakeCorridors()
        {
            Room previousRoom = null;
            foreach (Room room in map.rooms)
            {
                for (int i = 0; i < RandomGenerator.IntBetween(1, 4); i++)
                {
                    if (previousRoom == null)
                    {
                        previousRoom = room;
                    }
                    else
                    {
                        ConnectRooms(previousRoom, room);
                        previousRoom = room;
                    }
                }
            }
            map.mainRoom = previousRoom;
        }

        private void ConnectRooms(Room roomStart, Room roomEnd)
        {
            Point currentPoint = roomStart.RandomPointInsideRoom();
            Point endPoint = roomEnd.RandomPointInsideRoom();
            do
            {
                CreateCorridor(currentPoint);
                if (RandomGenerator.IntBetween(1, 10) <= 0)
                {
                    currentPoint = MoveRandomDirection(currentPoint);
                }
                else
                {
                    currentPoint = MoveTo(currentPoint, endPoint);
                }
            } while (currentPoint != endPoint);
        }

        private void CreateCorridor(Point currentPoint)
        {

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (map.IsValidLocation(new Point(currentPoint.X + x, currentPoint.Y + y)) &&
                    map.cells[currentPoint.X + x, currentPoint.Y + y].Terrain == TerrainType.Empty)
                    {
                        map.AssignMaterial(map.cells[currentPoint.X + x, currentPoint.Y + y], TerrainType.Wall);
                    }
                }
             }
            map.AssignMaterial(map.cells[currentPoint.X, currentPoint.Y], TerrainType.Hall);
        }

        private Point MoveRandomDirection(Point point)
        {
            DirectionPicker directionPicker = new DirectionPicker();
            do
            {
                point = directionPicker.RandomDirection(point);
            } while (!map.IsValidLocation(point));

            return point;
        }

        private Point MoveTo(Point start, Point end)
        {
            int x = end.X - start.X;
            int y = end.Y - start.Y;

            Point point = start;
            if (x == 0 && y != 0)
            {
                point = new Point(start.X, start.Y + (y / Math.Abs(y)));
            }
            else if (x != 0)
            {
                point = new Point(start.X + (x / Math.Abs(x)), start.Y);
            }
            return point;
        }
    }
}
