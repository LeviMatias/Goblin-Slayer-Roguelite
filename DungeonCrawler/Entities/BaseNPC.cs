using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonCrawler.World.TerrainGeneration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace DungeonCrawler.Entities
{
    public abstract class BaseNPC : BaseEntity
    {
        //public Point Target;
        //public bool HasTarget { get{ return Target.X != 0 && Target.Y != 0 && x != Target.X && y!= Target.Y; } }
        public int Aggro = 4;
        private List<Cell> path;

        public bool HasPath { get { return (path != null && path.Count() > 0); } }

        public int PathLength { get { return path.Count(); } }

        public void AssignPath(List<Cell> path)
        {
            this.path = path;
        }

        public Cell GetNextCell()
        {
            Cell next = path.First();
            path.Remove(next);
            return next;
        }

    }
}
