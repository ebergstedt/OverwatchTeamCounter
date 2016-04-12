using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ebergstedt.Overwatch.Counters.Containers;

namespace Ebergstedt.Overwatch.Counters
{
    public class OverwatchWinrateApi
    {
        public IEnumerable<HeroWinRate> GetHeroWinratesAgainstHero(int heroId)
        {
            return new List<HeroWinRate>();
        }

        public IEnumerable<MapWinRate> GetMapWinratesForHero(int heroId)
        {
            return new List<MapWinRate>();
        }
    }
}
