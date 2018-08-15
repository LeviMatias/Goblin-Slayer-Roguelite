using DungeonCrawler.Entities;
using DungeonCrawler.Items;
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

        private void CreateGoblin(Map CurrentMap)
        {
            BaseNPC goblin = new Goblin(1, 1);
            Enemies.Add(goblin);
            goblin.SetPosition(CurrentMap.RandomPointInRandomRoom());
            goblin.LoadContent();
            goblin.Died += RaiseEnemyDied;

            if (RandomGenerator.IntBetween(1,2) == 1)
            {
                int result = RandomGenerator.IntBetween(1, 4);
                // could use a factory class
                switch (result)
                {
                    case 1:
                        goblin.Loot = new BaseWeapon(BaseWeapon.WeaponType.Sword);
                        break;
                    case 2:
                        goblin.Loot = new BaseWeapon(BaseWeapon.WeaponType.Shiv);
                        break;
                    case 3:
                        goblin.Loot = new Potion(RandomGenerator.IntBetween(2, 5));
                        break;
                }
            }
        }

        public void SpawnEnemies(ContentManager Content, Map CurrentMap)
        {
            for (int i = 0; i < 10; i++)
            {
                CreateGoblin(CurrentMap);
            }
        }

        public void HandleEnemyDeath(Map map, Player player, BaseNPC enemyThatDied)
        {
            map[enemyThatDied.x, enemyThatDied.y].Occupant = null;
            if (enemyThatDied.Loot != null)
            {
                enemyThatDied.Loot.Dropped(enemyThatDied.x, enemyThatDied.y);
                map[enemyThatDied.x, enemyThatDied.y].Item = enemyThatDied.Loot;
            }
            CreateGoblin(map);
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
