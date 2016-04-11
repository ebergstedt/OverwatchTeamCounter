using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ebergstedt.Overwatch.Counters.Enums;

namespace Ebergstedt.Overwatch.Counters
{
    public class HeroFinder
    {
        string HeroConfigPath => Path.Combine(Environment.CurrentDirectory, "Heroes.json");
        string MugshotLocationsPath => Path.Combine(Environment.CurrentDirectory, "MugshotLocations.json");

        HeroLoader HeroLoader => new HeroLoader();
        HeroExtractor HeroExtractor => new HeroExtractor();

        BitmapTransformer BitmapTransformer => new BitmapTransformer();

        IEnumerable<Hero> heroList;
        MugshotLocations mugshotLocations;
        public HeroFinder()
        {
            heroList = HeroLoader.LoadHeroConfig(
                                               HeroConfigPath,
                                               Environment.CurrentDirectory);

            mugshotLocations = HeroLoader.LoadEnemyMugshotLocations(
                                                               MugshotLocationsPath,
                                                               ScreenResolution.FullHD);
        }

        public IEnumerable<Hero> FindEnemyHeroesByScreenshot(Bitmap screenShot)
        {            
            
            var heroWithMetaDatas = HeroLoader.LoadWithMetaData(heroList);

            var heroesByScreenshot = HeroExtractor.ExtractHeroMugshotsByScreenShot(
                                                                                   screenShot,
                                                                                   mugshotLocations
                                                                                   );


            HeroIdentifier heroIdentifier = new HeroIdentifier(
                                                               heroWithMetaDatas.Select(h => new HeroWithMugShot()
                                                               {
                                                                   Id = h.Id,
                                                                   Mugshot = h.Mugshot
                                                               })
                                                               .ToList());

            IEnumerable<int> identifiedEnemyHeroIds = heroesByScreenshot.Select(h => heroIdentifier.GetHeroIdByMugshot(h));

            return heroList.Where(h => identifiedEnemyHeroIds.Contains(h.Id));            
        }

        public IEnumerable<Hero> FindAlliedHeroesByScreenshot(Bitmap screenShot)
        {
            return null;
            throw new NotImplementedException();
        }
    }
}
