﻿using System;
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
        private Cell target;

        public int CurrentHealth;
        public int MaxHealth;
        public int Damage;

        public Goblin(int Health, int Damage)
        {
            this.Damage = Damage;
            MaxHealth = Health;
            CurrentHealth = MaxHealth;
        }

        //sourcebox for the image
        public override void LoadContent(ContentManager content)
        {
            sourceBox = new Rectangle(Tileset.Width * 3, 0, Tileset.Width, Tileset.Height);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
