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
        readonly ISimpleMemoryCache _cache = new SimpleMemoryCache.SimpleMemoryCache();
        readonly JsonConfigLoader _jsonConfigLoader = new JsonConfigLoader(Environment.CurrentDirectory); //todo

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

        //http://i.imgur.com/OUVOUfP.jpg
        private IEnumerable<HeroWinRate> GetHeroWinRate(
                                                 [NotNull] int heroId)
        {
            return _jsonConfigLoader.GetCounters()
                                    .Counters
                                    .SingleOrDefault(c => c.HeroId == heroId)
                                    ?.VersusHeroWinrates
                                    .Select(v => new HeroWinRate(v.CounterValue, v.HeroId));
        }

        private IEnumerable<MapWinRate> GetMapWinrate(
                                               [NotNull] int heroId)
        {
            //todo placeholder

            Random rand = new Random();

            return Enumerable.Range(1, 12)
                             .Select(x => new MapWinRate(
                                                         50,
                                                         x));
        }
    }
}
