using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ebergstedt.Overwatch.Counters.Containers;

namespace Ebergstedt.Overwatch.Counters
{
    public class StatisticsCalculator
    {
        public IEnumerable<int> GetBestOrderedHeroesForTeamComposition(
                                                                             IEnumerable<int> enemyHeroIds,
                                                                             int mapId)
        {
            return new List<int>();
        }

        public IEnumerable<int> GetBestOrderedHeroesForTeamComposition(
                                                                             IEnumerable<int> enemyHeroIds,
                                                                             IEnumerable<int> alliedHeroIds,
                                                                             int mapId)
        {
            return GetBestOrderedHeroesForTeamComposition(
                                                   enemyHeroIds,
                                                   mapId);
        }
    }
}
