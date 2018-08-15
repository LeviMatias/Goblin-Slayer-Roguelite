using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public static class RandomGenerator
    {
        private static readonly Random _generator = new Random();

        public static int IntBetween(int minimumValue, int maxValue)
        {
            return _generator.Next(minimumValue, maxValue + 1);
        }
    }
}
