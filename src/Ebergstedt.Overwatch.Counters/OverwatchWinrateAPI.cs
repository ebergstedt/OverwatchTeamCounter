using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebergstedt.Overwatch.Counters
{
    public class OverwatchWinrateApi
    {
        public List<HeroWinRate> GetHeroWinratesAgainstHero(int heroId)
        {
            return new List<HeroWinRate>();
        }

        public List<MapWinRate> GetMapWinratesForHero(int heroId)
        {
            return new List<MapWinRate>();
        }
    }
}
