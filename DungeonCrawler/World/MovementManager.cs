using DungeonCrawler.Entities;
using DungeonCrawler.World.TerrainGeneration;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.World
{
    public class MovementManager
    {

        public void Update(Map map, Player player, List<BaseNPC> entities)
        {
            Cell nextCell = map[(int)(player.x + player.Velocity.X), (int)(player.y + player.Velocity.Y)];
            if (map.Halls.Contains(nextCell))
            {
                if (nextCell.IsOccupied)
                {
                    nextCell.Occupant.TakeDamage(player.Damage);
                    player.PlayWriggleAnimation();
                }
                else
                {
                    map[(player.x), (player.y)].Occupant = null;
                    player.SetPosition(new Point((int)(player.x + player.Velocity.X), (int)(player.y + player.Velocity.Y)));
                    nextCell.Occupant = player;
                }
            }
            else return;

            foreach (BaseNPC entity in entities)
            {
                Vector2 v1 = new Vector2(player.x, player.y);
                Vector2 v2 = new Vector2(entity.x, entity.y);
                if (map.DistanceFrom(v1, v2) <= 1)
                {
                    player.TakeDamage(entity.Damage);
                    Console.WriteLine(player.CurrentHealth);
                    entity.PlayWriggleAnimation();
                }
                else
                {
                    map[entity.x, entity.y].Occupant = null;
                    PathfindingService.MoveEntity(map, player, entity);
                    map[entity.x, entity.y].Occupant = entity;
                }
            }
        }
    }
}
