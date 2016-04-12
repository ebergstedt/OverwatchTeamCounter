using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ebergstedt.Overwatch.Counters.Containers;

namespace Ebergstedt.Overwatch.Counters
{
    class HeroWinrateCalculationResult
    {
        public int HeroId { get; set; }
        public float WinResultant { get; set; }
    }

    public class HeroStatisticsCalculator
    {
        readonly OverwatchWinrateApi _overwatchWinrateApi = new OverwatchWinrateApi();    
        
        public IEnumerable<int> GetBestOrderedHeroCountersForTeamComposition(
                                                                             IEnumerable<int> enemyHeroIds,
                                                                             int mapId)
        {
            List<HeroWinrateCalculationResult> heroWinrateCalculationResults = new List<HeroWinrateCalculationResult>();

            foreach (var enemyHeroId in enemyHeroIds)
            {
                IEnumerable<HeroWinRate> heroWinratesAgainstHero = _overwatchWinrateApi.GetHeroWinratesAgainstHero(
                                                                                                                   enemyHeroId);

                foreach (var heroWinRate in heroWinratesAgainstHero)
                {
                    var singleOrDefault = heroWinrateCalculationResults.SingleOrDefault(h => h.HeroId == heroWinRate.HeroId);

                    if (singleOrDefault != null)
                    {
                        singleOrDefault.WinResultant += heroWinRate.Percentage;
                    }
                    else
                    {
                        heroWinrateCalculationResults.Add(
                                                          new HeroWinrateCalculationResult()
                                                          {
                                                              HeroId = heroWinRate.HeroId,
                                                              WinResultant = heroWinRate.Percentage
                                                          });
                    }
                }
            }

            return heroWinrateCalculationResults.OrderByDescending(w => w.WinResultant)
                                                .Select(h => h.HeroId);
        }

        public IEnumerable<int> GetBestOrderedHeroCountersForTeamComposition(
                                                                             IEnumerable<int> enemyHeroIds,
                                                                             IEnumerable<int> alliedHeroIds,
                                                                             int mapId)
        {
            //todo alliedHeroIds
            return GetBestOrderedHeroCountersForTeamComposition(
                                                          enemyHeroIds,
                                                          mapId);
        }
    }
}
