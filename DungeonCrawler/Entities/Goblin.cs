using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawler.Components;
using DungeonCrawler.World.TerrainGeneration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Entities
{
    public class Goblin : BaseNPC
    {

        public Goblin(int Health, int Damage)
        {
            this.BaseDamage = Damage;
            MaxHealth = Health;
            CurrentHealth = MaxHealth;
        }

        //sourcebox for the image
        public override void LoadContent()
        {
            sourceBox = new Rectangle(Tileset.Width * 3, Tileset.Height, Tileset.Width, Tileset.Height);
        }

        public override void Update()
        {
          
        }
    }
}
