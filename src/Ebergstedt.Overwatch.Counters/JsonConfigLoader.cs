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
    public class JsonConfigLoader
    {
        private readonly string _rootPath;
        private readonly string _heroesConfigPath;
        private readonly string _mugshotLocationConfigPath;

        public JsonConfigLoader([NotNull] string rootPath)
        {
            if (rootPath == null) throw new ArgumentNullException(nameof(rootPath));

            _rootPath = rootPath;

            _heroesConfigPath = Path.Combine(rootPath, "Heroes.json");
            _mugshotLocationConfigPath = Path.Combine(rootPath, "MugshotLocations.json");
        }

        public JsonContainers.MugshotLocations LoadMugshotLocations(
                                                                    ScreenResolution screenResolution = ScreenResolution.FullHD)
        {
            var readAllText = File.ReadAllText(_mugshotLocationConfigPath);

            var mugshotLocationsConfig = JsonConvert.DeserializeObject<JsonContainers.MugshotLocationsConfig>(readAllText);

            switch (screenResolution)
            {
                case ScreenResolution.FullHD:
                    return mugshotLocationsConfig.FullHD;
            }

            throw new NotImplementedException(nameof(screenResolution));
        }

        public IEnumerable<Tuple<int, Bitmap>> LoadHeroIdMugshotBitmaps()
        {
            return LoadHeroConfig()
                   .Select(h => new Tuple<int, Bitmap>(
                                                       h.Id, 
                                                       (Bitmap) Bitmap.FromFile(h.MugshotFilePath)));
        }

        public IEnumerable<JsonContainers.Hero> LoadHeroConfig()
        {
            var readAllText = File.ReadAllText(_heroesConfigPath);

            JsonContainers.HeroesConfig heroesConfig = JsonConvert.DeserializeObject<JsonContainers.HeroesConfig>(
                                                                                    readAllText);

            foreach (var hero in heroesConfig.Heroes)
            {
                hero.MugshotFilePath = Path.Combine(
                                                    _rootPath, 
                                                    hero.MugshotFilePath);
            }

            return heroesConfig.Heroes;
        }
    }
}
