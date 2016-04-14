using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Ebergstedt.Overwatch.Counters.Containers;
using Ebergstedt.Overwatch.Counters.Enums;
using Ebergstedt.SimpleMemoryCache.Interfaces;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Ebergstedt.Overwatch.Counters
{
    public class JsonConfigLoader
    {
        readonly string _rootPath;
        readonly string _heroesConfigPath;
        readonly string _mugshotLocationConfigPath;
        readonly string _mapsConfigPath;

        readonly ISimpleMemoryCache _cache = new SimpleMemoryCache.SimpleMemoryCache();

        public JsonConfigLoader(
                                [NotNull] string rootPath)
        {
            if (rootPath == null) throw new ArgumentNullException(nameof(rootPath));

            _rootPath = rootPath;

            _heroesConfigPath = Path.Combine(rootPath, "Heroes.json");
            _mugshotLocationConfigPath = Path.Combine(rootPath, "MugshotLocations.json");
            _mapsConfigPath = Path.Combine(rootPath, "Maps.json");
        }

        public JsonContainers.MugshotLocations GetMugshotLocations(
                                                                   ScreenResolution screenResolution = ScreenResolution.FullHD)
        {
            return _cache.Get(
                              $"GetMugshotLocations({screenResolution})",
                              _GetMugshotLocations(screenResolution));
        }

        private JsonContainers.MugshotLocations _GetMugshotLocations(
                                                                     ScreenResolution screenResolution = ScreenResolution.FullHD)
        {
            if(!File.Exists(_mugshotLocationConfigPath))
                throw new FileNotFoundException(_mugshotLocationConfigPath);

            var readAllText = File.ReadAllText(_mugshotLocationConfigPath);

            var mugshotLocationsConfig = JsonConvert.DeserializeObject<JsonContainers.MugshotLocationsConfig>(readAllText);

            switch (screenResolution)
            {
                case ScreenResolution.FullHD:
                    return mugshotLocationsConfig.FullHD;
            }

            throw new NotImplementedException(nameof(screenResolution));
        }

        public IEnumerable<Tuple<int, Bitmap>> GetHeroIdMugshotBitmaps()
        {
            return GetHeroConfig()
                   .Select(h => new Tuple<int, Bitmap>(
                                                       h.Id, 
                                                       (Bitmap) Bitmap.FromFile(h.MugshotFilePath)));
        }

        public IEnumerable<JsonContainers.Hero> GetHeroConfig()
        {
            return _cache.Get(
                              "GetHeroConfig()",
                              _GetHeroConfig());
        }

        private IEnumerable<JsonContainers.Hero> _GetHeroConfig()
        {
            if (!File.Exists(_heroesConfigPath))
                throw new FileNotFoundException(_heroesConfigPath);

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

        public IEnumerable<JsonContainers.Map> GetMapConfig()
        {
            return _cache.Get(
                              "GetMapConfig()",
                              _GetMapConfig());
        }

        private IEnumerable<JsonContainers.Map> _GetMapConfig()
        {
            if (!File.Exists(_mapsConfigPath))
                throw new FileNotFoundException(_mapsConfigPath);

            var readAllText = File.ReadAllText(_mapsConfigPath);

            return JsonConvert.DeserializeObject<JsonContainers.MapConfig>(
                                                                           readAllText)
                               .Maps;
        }
    }
}
