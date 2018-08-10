using DungeonCrawler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.World
{
    public class TurnManager
    {
        private int turnSpeed = 8;
        private int turnStep = 0;
        private static TurnManager _instance;
        private static readonly object padlock = new object();

        public static TurnManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance != null) return _instance;
                    _instance = new TurnManager();
                    return _instance;
                }
            }
        }

        private TurnManager() { }

        public bool Update(Player player, List<BaseEntity> entities)
        {
            bool NewTurn = false;
            if ((turnStep % turnSpeed) == 0)
            {
                turnStep = 1;
                if (player.Walk())
                {
                    NewTurn = true;
                    foreach (BaseEntity entity in entities)
                    {
                        entity.Update();
                    }
                }
            }
            turnStep++;
            return NewTurn;
        }
    }
}
