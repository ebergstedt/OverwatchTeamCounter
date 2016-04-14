using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ebergstedt.Overwatch.Counters.Containers;
using JetBrains.Annotations;

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
        
        public IEnumerable<int> GetBestOrderedHeroIdCountersForTeamComposition([NotNull] IEnumerable<int> enemyHeroIds,
                                                                               [NotNull] int mapId)
        {
            if (enemyHeroIds == null) throw new ArgumentNullException(nameof(enemyHeroIds));

            List<HeroWinrateCalculationResult> heroWinrateCalculationResults = new List<HeroWinrateCalculationResult>();

            foreach (var enemyHeroId in enemyHeroIds.Distinct())
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

            foreach (var heroWinrateCalculationResult in heroWinrateCalculationResults)
            {
                MapWinRate mapWinrateForHero = _overwatchWinrateApi.GetMapWinratesForHero(
                                                                                          heroWinrateCalculationResult.HeroId)
                                                                   .SingleOrDefault(m => m.MapId == mapId);

                if(mapWinrateForHero == null)
                    continue;

                var winrateCalculationResult = heroWinrateCalculationResults.Single(h => h.HeroId == heroWinrateCalculationResult.HeroId);

                //add a single resultant to the total winrate
                winrateCalculationResult.WinResultant += mapWinrateForHero.Percentage;
            }

            //sort by hero having the most total winrate against the enemy team
            return heroWinrateCalculationResults.OrderByDescending(w => w.WinResultant)
                                                .Select(h => h.HeroId);
        }

        public IEnumerable<int> GetBestOrderedHeroIdCountersForTeamComposition([NotNull] IEnumerable<int> enemyHeroIds,
            [NotNull] IEnumerable<int> alliedHeroIds,
                                                                               [NotNull] int mapId)
        {
            if (enemyHeroIds == null) throw new ArgumentNullException(nameof(enemyHeroIds));
            if (alliedHeroIds == null) throw new ArgumentNullException(nameof(alliedHeroIds));

            //todo alliedHeroIds
            return GetBestOrderedHeroIdCountersForTeamComposition(
                                                          enemyHeroIds,
                                                          mapId);
        }
    }
}
