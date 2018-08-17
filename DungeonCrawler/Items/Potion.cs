using DungeonCrawler.Components;
using DungeonCrawler.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Items
{
    class Potion : BaseItem
    {
        private int heal;

        public Potion(int heal)
        {
            this.heal = heal;
            sourceBox = new Rectangle(0, Tileset.Height*2, Tileset.Width, Tileset.Height);
        }

        public override void PickedUp(BaseEntity entity)
        {
            entity.TakeDamage(-heal); 
        }

    }
}
