using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ebergstedt.Overwatch.Counters.Containers;
using Ebergstedt.Overwatch.Counters.Enums;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Ebergstedt.Overwatch.Counters
{
    public class HeroLoader
    {

        private OverwatchWinrateApi OverwatchWinrateApi = new OverwatchWinrateApi();

        public JsonContainers.MugshotLocations LoadMugshotLocations(
                                                          [NotNull] string configPath,
                                                          ScreenResolution screenResolution = ScreenResolution.FullHD)
        {
            if (configPath == null) throw new ArgumentNullException(nameof(configPath));

            var readAllText = File.ReadAllText(configPath);

            var mugshotLocationsConfig = JsonConvert.DeserializeObject<JsonContainers.MugshotLocationsConfig>(readAllText);

            switch (screenResolution)
            {
                case ScreenResolution.FullHD:
                    return mugshotLocationsConfig.FullHD;
            }

            throw new NotImplementedException(nameof(screenResolution));
        }

        public IEnumerable<JsonContainers.Hero> LoadHeroConfig(
                                                [NotNull] string configPath,
                                                [NotNull] string rootPath)
        {
            if (configPath == null) throw new ArgumentNullException(nameof(configPath));
            if (rootPath == null) throw new ArgumentNullException(nameof(rootPath));

            var readAllText = File.ReadAllText(configPath);

            JsonContainers.HeroesConfig heroesConfig = JsonConvert.DeserializeObject<JsonContainers.HeroesConfig>(
                                                                                    readAllText);

            foreach (var hero in heroesConfig.Heroes)
            {
                hero.MugshotFilePath = Path.Combine(
                                                    rootPath, 
                                                    hero.MugshotFilePath);
            }

            return heroesConfig.Heroes;
        }

        public IEnumerable<HeroWithMetaData> LoadWithMetaData(
                                                              [NotNull] IEnumerable<JsonContainers.Hero> heroes)
        {
            if (heroes == null) throw new ArgumentNullException(nameof(heroes));            

            foreach (var hero in heroes)
            {
                yield return new HeroWithMetaData(
                                                  hero.Name,
                                                  hero.Id,
                                                  (Bitmap) Image.FromFile(hero.MugshotFilePath), 
                                                  OverwatchWinrateApi.GetHeroWinratesAgainstHero(hero.Id),
                                                  OverwatchWinrateApi.GetMapWinratesForHero(hero.Id));
            }
        }
    }
}
