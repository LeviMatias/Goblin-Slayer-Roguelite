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
                map[(player.x), (player.y)].Occupant = null;
                player.SetPosition(new Point((int)(player.x + player.Velocity.X), (int)(player.y + player.Velocity.Y)));
                nextCell.Occupant = player;
            }
            foreach (BaseNPC entity in entities)
            {
                map[entity.x, entity.y].Occupant = null;
                PathfindingService.MoveEntity(map, player, entity);
                map[entity.x, entity.y].Occupant = entity;
            }
        }
    }
}
