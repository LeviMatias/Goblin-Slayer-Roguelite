using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawler.Components;
using DungeonCrawler.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonCrawler.Items
{
    public class BaseWeapon : BaseItem
    {
        public enum WeaponType {Shiv, Sword }

        public int Durability;
        public int DamageBonus;

        public BaseWeapon (WeaponType weapon)
        {
            if(weapon == WeaponType.Shiv)
            {
                Durability = 1;
                DamageBonus = 2;
                sourceBox = new Rectangle(Tileset.Width, Tileset.Height, Tileset.Width, Tileset.Height);
            }
            else
            {
                Durability = 3;
                DamageBonus = 4;
                sourceBox = new Rectangle(Tileset.Width*2, Tileset.Height, Tileset.Width, Tileset.Height);
            }
        }

        public override bool PickedUp(BaseEntity entity)
        {
            if(entity.Weapon == null || entity.Weapon.DamageBonus < this.DamageBonus)
            {
                entity.Weapon = this;
                return true;
            }
            return false;
        }
    }
}
