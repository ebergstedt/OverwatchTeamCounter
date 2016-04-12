using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ebergstedt.Overwatch.Counters.Containers;
using Ebergstedt.Overwatch.Counters.Enums;

namespace Ebergstedt.Overwatch.Counters
{
    public class HeroFinder
    {
        string HeroConfigPath => Path.Combine(Environment.CurrentDirectory, "Heroes.json");
        string MugshotLocationsPath => Path.Combine(Environment.CurrentDirectory, "MugshotLocations.json");

        HeroLoader HeroLoader => new HeroLoader();
        HeroExtractor HeroExtractor => new HeroExtractor();

        readonly IEnumerable<JsonContainers.Hero> _heroList;
        readonly JsonContainers.MugshotLocations _mugshotLocations;
        readonly HeroIdentifier _heroIdentifier;
        public HeroFinder()
        {
            _heroList = HeroLoader.LoadHeroConfig(
                                               HeroConfigPath,
                                               Environment.CurrentDirectory);

            _mugshotLocations = HeroLoader.LoadMugshotLocations(
                                                               MugshotLocationsPath,
                                                               ScreenResolution.FullHD);

            var heroWithMetaDatas = HeroLoader.LoadWithMetaData(_heroList);


            _heroIdentifier = new HeroIdentifier(
                                                 heroWithMetaDatas.Select(h => new HeroWithMugShot()
                                                 {
                                                     Id = h.Id,
                                                     Mugshot = h.Mugshot
                                                 })
                                                 .ToList());
        }

        public IEnumerable<JsonContainers.Hero> FindEnemyHeroesByScreenshot(
                                                             Bitmap screenShot)
        {     
            var heroesByScreenshot = HeroExtractor.ExtractHeroMugshotsByScreenShot(
                                                                                   screenShot,
                                                                                   _mugshotLocations.EnemyLocationPoints,
                                                                                   _mugshotLocations.PortraitWidth,
                                                                                   _mugshotLocations.PortraitHeight
                                                                                   );



            IEnumerable<int> identifiedEnemyHeroIds = heroesByScreenshot.Select(h => _heroIdentifier.GetHeroIdByMugshot(h));

            return _heroList.Where(h => identifiedEnemyHeroIds.Contains(h.Id));            
        }

        public IEnumerable<JsonContainers.Hero> FindAlliedHeroesByScreenshot(
                                                              Bitmap screenShot)
        {
            var heroesByScreenshot = HeroExtractor.ExtractHeroMugshotsByScreenShot(
                                                                                   screenShot,
                                                                                   _mugshotLocations.AlliedLocationPoints,
                                                                                   _mugshotLocations.PortraitWidth,
                                                                                   _mugshotLocations.PortraitHeight
                                                                                   );



            IEnumerable<int> identifiedAlliedHeroIds = heroesByScreenshot.Select(h => _heroIdentifier.GetHeroIdByMugshot(h));

            return _heroList.Where(h => identifiedAlliedHeroIds.Contains(h.Id));
        }
    }
}
