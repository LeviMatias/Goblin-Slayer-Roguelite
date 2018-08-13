using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.World.TerrainGeneration
{
    public class Map
    {
        public enum TerrainType {Hall, Empty, Wall }
        public enum Direction {North = 0, South = 1, East = 2, West = 3 }

        public List<Room> rooms;
        public Room mainRoom;

        public Cell[,] cells;
        public List<Cell> Walls;
        public List<Cell> Halls;

        public Cell this[int x, int y]
        {
            get { return cells[x, y]; }
        }

        public Cell this[Point point]
        {
            get { return cells[point.X, point.Y]; }
        }

        public Map(int x, int y)
        {
            cells = new Cell[x, y];
            int number = 0;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++) {
                    cells[i, j] = new Cell(i,j);
                }
            }

            Console.WriteLine(number);

            rooms = new List<Room>();
            Walls = new List<Cell>();
            Halls = new List<Cell>();
        }

        public void LoadContent(ContentManager content)
        {
            for (int x = 0; x < cells.GetUpperBound(0); x++)
            {
                for (int y = 0; y < cells.GetUpperBound(1); y++)
                {
                    cells[x, y].LoadTexture(content, x, y);
                }
            }
        }

        public int InsideXBound(int x)
        {
            if (x >= cells.GetUpperBound(0)) x = cells.GetUpperBound(0);
            return x;
        }

        public int InsideYBound(int y)
        {
            if (y >= cells.GetUpperBound(1)) y = cells.GetUpperBound(1);
            return y;
        }

        public bool IsValidLocation(Point location)
        {
            return ((location.X >= 0 && location.X < cells.GetUpperBound(0) - 1) &&
                (location.Y >= 0 && location.Y < cells.GetUpperBound(1) - 1));
        }

        public void AssignMaterial(Cell cell, TerrainType terrain)
        {
            cell.Terrain = terrain;
            if (TerrainType.Hall == terrain)
            {
                Halls.Add(cell);
            }
            else if(TerrainType.Wall == terrain)
            {
                Walls.Add(cell);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 lightSource, int lightRadius)
        {
            LightFill(lightSource, (int)lightSource.X, (int)lightSource.Y, lightRadius);
            for(int x =0; x < cells.GetUpperBound(0); x++)
            {
                for (int y = 0; y < cells.GetUpperBound(1); y++)
                {
                   cells[x, y].Draw(spriteBatch);
                }
            }
        }

        private void LightFill(Vector2 sourcePoint, int x, int y, int radius)
        {
            Cell cell = cells[x, y];
            int distance = DistanceFrom(sourcePoint, new Vector2(x, y));
            if (cell.Visible || cell.IsWall || distance >= radius) return;

            cell.Visible = true;
            if (distance + 3 >= radius) return;
            LightFill(sourcePoint, x + 1, y, radius);
            LightFill(sourcePoint, x - 1, y, radius);
            LightFill(sourcePoint, x, y + 1, radius);
            LightFill(sourcePoint, x, y - 1, radius);
        }

        public int DistanceFrom(Vector2 v1, Vector2 v2)
        {
            return (int)Math.Ceiling((v1 - v2).Length());
        }

        public Point RandomPointInRandomRoom()
        {
            Point pointInRoom = rooms[RandomGenerator.IntBetween(0, rooms.Count - 1)].RandomPointInsideRoom();
            return pointInRoom;
        }
    }
}
