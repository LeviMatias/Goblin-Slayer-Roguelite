using DungeonCrawler.Components;
using DungeonCrawler.Items;
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
        private int wriggleOffset = 0;
        private int lungeOffset = 0;

        public int x;
        public int y;
        public Rectangle sourceBox;
        public Vector2 Velocity;
        public bool Dead;
        public BaseWeapon Weapon;

        public int CurrentHealth;
        public int MaxHealth;

        public int BaseDamage = 10;
        public int Damage { get {
                int damage = BaseDamage;
                if (Weapon != null)
                {
                    damage += Weapon.DamageBonus;
                }
                return damage;
            } }

        public abstract void LoadContent();

        public abstract void Update();

        public event EventHandler Died;

        public void Draw(SpriteBatch spriteBatch)
        {
            if (wriggleOffset > 0)
            {
                wriggleOffset--;
            }

            if (lungeOffset > 0)
            {
                lungeOffset--;
            }

            Rectangle hitbox = new Rectangle(
                (x - 1) * Tileset.Width + 3*((int)Math.Pow(-1, wriggleOffset%6)), 
                (y - 1) * Tileset.Height + 3 * ((int)Math.Pow(-1, lungeOffset % 6))
                , Tileset.Width, Tileset.Height);

            spriteBatch.Draw(Tileset.Texture, hitbox, sourceBox, Color.White);
        }

        public void PlayWriggleAnimation()
        {
            wriggleOffset = 6;
        }

        public void PlayLungeAnimation()
        {
            lungeOffset = 6;
        }

        internal void SetPosition(Point point)
        {
            x = point.X;
            y = point.Y;
        }

        public void TakeDamage(int dmg)
        {
            CurrentHealth -= dmg;
            if (CurrentHealth <= 0) OnDeath(EventArgs.Empty);
        }

        protected virtual void OnDeath(EventArgs e)
        {
            Died?.Invoke(this, e);
        }

    }
}
