using DungeonCrawler.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Entities
{
    public abstract class BaseEntity : DrawComponent
    {
        public int x;
        public int y;
        public Rectangle sourceBox;
        public Vector2 Velocity;

        public abstract void LoadContent(ContentManager content);

        public abstract void Update();

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle hitbox = new Rectangle((x - 1) * Tileset.Width, (y - 1) * Tileset.Height, Tileset.Width, Tileset.Height);
            spriteBatch.Draw(Tileset.Texture, hitbox, sourceBox, Color.White);
        }

        internal void SetPosition(Point point)
        {
            x = point.X;
            y = point.Y;
        }

    }
}
