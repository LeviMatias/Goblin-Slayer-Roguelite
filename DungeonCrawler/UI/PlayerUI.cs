using DungeonCrawler.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.UI
{
    class PlayerUI
    {
        private int healthPer = 100;
        private Texture2D _texture;

        private Rectangle weaponSource;
        private bool hasWeapon;

        public void ConnectToEvents(Player player)
        {
            player.HealthChanged += UpdateHealthBar;
            player.PickedWeapon += UpdateWeapon;
        }

        public void Draw(SpriteBatch spriteBatch,int x, int y)
        {
            DrawHP(spriteBatch, x, y);
            DrawWeapon(spriteBatch, x, y);
        }

        private void UpdateHealthBar(object sender, EventArgs e)
        {
            Player plr = (Player)sender;
            int healthPercentage = (int)(((float)plr.CurrentHealth/ (float)plr.MaxHealth)*100);
            if (healthPercentage < 0) healthPercentage = 0;
            else if (healthPercentage > 100) healthPercentage = 100;

            healthPer = healthPercentage;
        }


        private void UpdateWeapon(object sender, EventArgs e)
        {
            Player plr = (Player)sender;
            if (plr.Weapon != null)
            {
                weaponSource = plr.Weapon.sourceBox;
                hasWeapon = true;
            }
            else
            {
                hasWeapon = false;
            }
        }

        private void DrawHP(SpriteBatch spriteBatch, int x, int y)
        {
            if (_texture == null)
            {
                _texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _texture.SetData(new Color[] { Color.White });
            }

            Rectangle healthBar = new Rectangle(x + 20, y + 20, healthPer, 20);
            spriteBatch.Draw(_texture, healthBar, Color.Red);
        }

        private void DrawWeapon(SpriteBatch spriteBatch, int x, int y)
        {
            if (hasWeapon)
            {
                Rectangle position = new Rectangle(x + 20, y + 50, Tileset.Width + 20, Tileset.Height + 20);
                spriteBatch.Draw(Tileset.Texture, position, weaponSource, Color.White);
            }
        }
    }
}
