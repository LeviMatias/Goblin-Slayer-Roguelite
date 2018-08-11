using DungeonCrawler.Entities;
using DungeonCrawler.Pathfinding;
using DungeonCrawler.World.TerrainGeneration;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public static class PathfindingService
    {
        static int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }

        static List<Location> GetWalkableAdjacentSquares(int x, int y, Map map)
        {
            var proposedLocations = new List<Location>();
            
            proposedLocations.Add(new Location() { X = x + 1, Y = y });
            proposedLocations.Add(new Location() { X = x - 1, Y = y });
            proposedLocations.Add(new Location() { X = x, Y = y + 1 });
            proposedLocations.Add(new Location() { X = x, Y = y - 1 });

            //sorts based on condition
            return proposedLocations.Where(
                l => (map.IsValidLocation(new Point(l.X, l.Y)) && !map[l.X,l.Y].IsWall && !map[l.X, l.Y].IsOccupied)).ToList();
        }

        private static Cell NextCell(Map map, Cell startCell, Cell targetCell)
        {
            Location start = new Location { X = startCell.x, Y = startCell.y };
            Location target = new Location { X = targetCell.x, Y = targetCell.y };
            Location current = null;
            List<Location> closedSet = new List<Location>();
            List<Location> openSet = new List<Location>();
            openSet.Add(start);
            int gScore = 0; //total cost of traveling from startCell to target Cell

            while(openSet.Count > 0 && closedSet.Count < 2)
            {
                var lowest = openSet.Min(location => location.F);
                current = openSet.First(l => l.F == lowest);
                // add the current square to the closed list
                closedSet.Add(current);
                // remove it from the open list
                openSet.Remove(current);

                if (closedSet.FirstOrDefault(location => location.X == target.X && location.Y == target.Y) != null)
                    //target was added to set
                    break;
                var adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, map);
                gScore++;

                foreach (var adjacentSquare in adjacentSquares)
                {
                    // if this adjacent square is already in the closed list, ignore it
                    if (closedSet.FirstOrDefault(l => l.X == adjacentSquare.X
                            && l.Y == adjacentSquare.Y) != null)
                        continue;

                    // if it's not in the open list...
                    if (openSet.FirstOrDefault(l => l.X == adjacentSquare.X
                            && l.Y == adjacentSquare.Y) == null)
                    {
                        // compute its score, set the parent
                        adjacentSquare.G = gScore;
                        adjacentSquare.H = ComputeHScore(adjacentSquare.X,
                            adjacentSquare.Y, target.X, target.Y);
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.PrevTile = current;

                        // and add it to the open list
                        openSet.Insert(0, adjacentSquare);
                    }
                    else
                    {
                        // test if using the current G score makes the adjacent square's F score
                        // lower, if yes update the parent because it means it's a better path
                        if (gScore + adjacentSquare.H < adjacentSquare.F)
                        {
                            adjacentSquare.G = gScore;
                            adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                            adjacentSquare.PrevTile = current;
                        }
                    }
                }
            }
            Location nextStep;
            if (closedSet.Count >= 2) nextStep = closedSet[1];
            else nextStep = closedSet[0];

            return map[nextStep.X, nextStep.Y];
        }

        public static void MoveEntity(Map map, Player player, BaseNPC entity)
        {
            Vector2 v1 = new Vector2(entity.x, entity.y);
            Vector2 v2 = new Vector2(player.x, player.y);
            Cell newPosition = null;

            if (map.DistanceFrom(v1, v2) > 10)
            {
                Cell start = map[entity.x, entity.y];
                if (!entity.HasTarget)
                {
                    entity.Target = map.RandomPointInRandomRoom();
                }
                newPosition = NextCell(map, start, map[entity.Target]);
            }
            else
            {
                Cell start = map[entity.x, entity.y];
                Cell target = map[player.x, player.y];
                newPosition = NextCell(map, start, target);
            }
            entity.SetPosition(new Point(newPosition.x, newPosition.y));
        }

    }
}
