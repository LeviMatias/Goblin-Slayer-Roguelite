using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler.Components
{
    public static class Tileset
    {
        public static Texture2D Texture;
        public static int Height;
        public static int Width;

        public static int Columns = 4;
        public static int Rows = 2;

        public static Rectangle? FogSourceBox { get; internal set; }

        public static void SetTexture(ContentManager content,string textureName)
        {
            Texture = content.Load<Texture2D>(textureName);
            Height = Texture.Height / Rows;
            Width = Texture.Width / Columns;
            FogSourceBox = new Rectangle(0, Height, Width, Height);
        }
    }
}
