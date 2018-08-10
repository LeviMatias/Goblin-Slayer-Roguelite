using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawler.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static DungeonCrawler.World.TerrainGeneration.Map;

namespace DungeonCrawler.World.TerrainGeneration
{
    public class Cell
    {
        public TerrainType Terrain = TerrainType.Empty;
        Texture2D texture;
        Rectangle hitbox;
        Rectangle sourceBox;
        public bool IsWall { get { return (Terrain == TerrainType.Wall);} }
        public bool Visible = false;

        internal void LoadTexture(ContentManager content, int x, int y)
        {
            texture = Tileset.Texture;
            switch (Terrain) {
                case TerrainType.Empty:
                    sourceBox = new Rectangle((texture.Width/ Tileset.Columns) *2, 0, texture.Width/ Tileset.Columns, texture.Height/ Tileset.Rows);
                    break;
                case TerrainType.Hall:
                    sourceBox = new Rectangle((texture.Width / Tileset.Columns), 0, texture.Width / Tileset.Columns, texture.Height/Tileset.Rows);
                    break;
                case TerrainType.Wall:
                    sourceBox = new Rectangle(0, 0, texture.Width / Tileset.Columns, texture.Height/ Tileset.Rows);
                    break;
            }
            hitbox = new Rectangle((x - 1)* texture.Width/ Tileset.Columns, (y - 1)* texture.Height/ Tileset.Rows, texture.Width/ Tileset.Columns, texture.Height / Tileset.Rows);
        }
            
        internal void Draw(SpriteBatch spriteBatch)
        {
          spriteBatch.Draw(texture, hitbox, sourceBox, Color.White);
          if (Visible || IsWall)
                Visible = false;
          else
                spriteBatch.Draw(texture, hitbox, Tileset.FogSourceBox, Color.White);

        }
    }
}
