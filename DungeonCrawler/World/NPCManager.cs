using DungeonCrawler.Entities;
using DungeonCrawler.World.TerrainGeneration;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.World
{
    class NPCManager
    {
        public List<BaseNPC> Enemies = new List<BaseNPC> ();

        private static NPCManager _instance;
        private static readonly object padlock = new object();

        public event EventHandler EnemyDied;

        public static NPCManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance != null) return _instance;
                    _instance = new NPCManager();
                    return _instance;
                }
            }
        }

        private NPCManager() { }

        public void SpawnEnemies(ContentManager Content, Map CurrentMap)
        {
            for (int i = 0; i < 10; i++)
            {
                Enemies.Add(new Goblin(1, 1));
                Enemies[i].SetPosition(CurrentMap.RandomPointInRandomRoom());
                Enemies[i].LoadContent(Content);
                Enemies[i].Died += RaiseEnemyDied;
            }
        }

        public void DrawEnemies(SpriteBatch spriteBatch)
        {
            foreach (BaseNPC npc in Enemies) npc.Draw(spriteBatch);
        }

        public void HandleEnemyDeath(Map map, Player player, BaseNPC enemyThatDied)
        {
            map[enemyThatDied.x, enemyThatDied.y].Occupant = null;
        }

        private void RaiseEnemyDied(object sender, EventArgs e)
        {
            BaseNPC enemy = (BaseNPC)sender;
            Enemies.Remove(enemy);
            enemy.Dead = true;
            enemy.Died -= RaiseEnemyDied;
            EnemyDied?.Invoke(sender, e);
        }
    }
}
