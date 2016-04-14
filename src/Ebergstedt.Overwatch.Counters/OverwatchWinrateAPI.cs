using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Ebergstedt.Overwatch.Counters.Containers;
using Ebergstedt.SimpleMemoryCache.Interfaces;
using JetBrains.Annotations;

namespace Ebergstedt.Overwatch.Counters
{
    public class OverwatchWinrateApi
    {
        private ISimpleMemoryCache _cache = new SimpleMemoryCache.SimpleMemoryCache();

        public IEnumerable<HeroWinRate> GetHeroWinratesAgainstHero(
                                                                   [NotNull] int heroId)
        {
            return _cache.Get(
                              $"GetHeroWinratesAgainstHero({heroId})",
                              GetHeroWinRate(heroId));            
        }

        public IEnumerable<MapWinRate> GetMapWinratesForHero(
                                                             [NotNull] int heroId)
        {
            return _cache.Get(
                              $"GetMapWinratesForHero({heroId})",
                              GetMapWinrate(heroId));
        }

        private List<HeroWinRate> GetHeroWinRate(
                                                 [NotNull] int heroId)
        {
            //todo placeholder

            Random rand = new Random();
            
            return Enumerable.Range(1, 21)
                             .Select(x => new HeroWinRate(
                                                          (float) rand.Next(45, 55),
                                                          x))
                             .ToList();
        }

        private List<MapWinRate> GetMapWinrate(
                                               [NotNull] int heroId)
        {
            //todo placeholder

            Random rand = new Random();

            return Enumerable.Range(1, 12)
                             .Select(x => new MapWinRate(
                                                          (float)rand.Next(45, 55),
                                                          x))
                             .ToList();
        }
    }
}
