using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawler.Entities
{
    public abstract class BaseNPC : BaseEntity
    {
        public Point Target;

        public bool HasTarget { get{ return Target != null; } }

    }
}
