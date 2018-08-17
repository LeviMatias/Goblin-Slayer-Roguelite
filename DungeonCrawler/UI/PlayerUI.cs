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

        public void ConnectToEvents(Player player)
        {
            player.HealthChanged += UpdateHealthBar;
        }

        public void DrawBar(SpriteBatch spriteBatch,int x, int y)
        {
            if (_texture == null)
            {
                _texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _texture.SetData(new Color[] { Color.White });
            }

            Rectangle healthBar = new Rectangle(x + 20, y + 20, healthPer, 20);
            spriteBatch.Draw(_texture, healthBar, Color.Red);
        }

        private void UpdateHealthBar(object sender, EventArgs e)
        {
            Player plr = (Player)sender;
            int healthPercentage = (int)(((float)plr.CurrentHealth/ (float)plr.MaxHealth)*100);
            if (healthPercentage < 0) healthPercentage = 0;
            else if (healthPercentage > 100) healthPercentage = 100;

            healthPer = healthPercentage;
        }
    }
}
