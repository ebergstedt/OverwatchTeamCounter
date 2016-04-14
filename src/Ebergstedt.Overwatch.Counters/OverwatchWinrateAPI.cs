using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Ebergstedt.Overwatch.Counters.Containers;
using Ebergstedt.SimpleMemoryCache.Interfaces;

namespace Ebergstedt.Overwatch.Counters
{
    public class OverwatchWinrateApi
    {
        private ISimpleMemoryCache _cache = new SimpleMemoryCache.SimpleMemoryCache();

        public IEnumerable<HeroWinRate> GetHeroWinratesAgainstHero(int heroId)
        {
            return _cache.Get(
                              $"GetHeroWinratesAgainstHero({heroId})",
                              () => new List<HeroWinRate>());            
        }

        public IEnumerable<MapWinRate> GetMapWinratesForHero(int heroId)
        {
            return _cache.Get(
                              $"GetMapWinratesForHero({heroId})",
                              () => new List<MapWinRate>());
        }
    }
}
