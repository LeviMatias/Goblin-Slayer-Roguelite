using DungeonCrawler.Components;
using DungeonCrawler.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Items
{
    public abstract class BaseItem
    {
        public Rectangle sourceBox;
        public Rectangle hitbox;

        public int x;
        public int y;

        public void DrawItem(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Tileset.Texture, hitbox, sourceBox, Color.White); ;
        }

        public void Dropped(int x, int y)
        {
            this.x = x;
            this.y = y;
            hitbox = new Rectangle(x, y, Tileset.Height, Tileset.Width);
        }

        public abstract void PickedUp(BaseEntity entity);
    }
}
