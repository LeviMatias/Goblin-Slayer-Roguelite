using DungeonCrawler.Components;
using DungeonCrawler.Entities;
using DungeonCrawler.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Player : BaseEntity
    {

        public int VisionRadius = 6;

        public Player()
        {
            this.MaxHealth = 100;
            this.CurrentHealth = this.MaxHealth;
        }

        public override void LoadContent()
        {
            sourceBox = new Rectangle(Tileset.Width * 3, 0, Tileset.Width, Tileset.Height);
        }

        public bool Walk()
        {
            Update();
            return (Velocity.X != 0 || Velocity.Y != 0);
        }

        public override void Update()
        {
            int vy = 0;
            int vx = 0;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                vy--;
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
                vy++;
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
                vx--;
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                vx++;

            Velocity = new Vector2(vx, vy);
        }
    }
}
