using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ebergstedt.Overwatch.Counters.Containers;
using JetBrains.Annotations;

namespace Ebergstedt.Overwatch.Counters
{
    public class MetaDataHelper
    {
        readonly JsonConfigLoader _jsonConfigLoader = new JsonConfigLoader(Environment.CurrentDirectory);

        public string GetHeroNameById(
                                      [NotNull] int id)
        {
            return _jsonConfigLoader.GetHeroConfig()
                                   .SingleOrDefault(h => h.Id == id)
                                   ?.Name;
        }

        public string GetMapNameById(
                                     [NotNull] int id)
        {
            return _jsonConfigLoader.GetMapConfig()
                                   .SingleOrDefault(m => m.Id == id)
                                   ?.Name;
        }

        public IEnumerable<JsonContainers.Map> GetMaps()
        {
            return _jsonConfigLoader.GetMapConfig();
        }
    }
}
