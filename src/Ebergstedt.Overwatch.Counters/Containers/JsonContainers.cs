using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebergstedt.Overwatch.Counters.Containers
{
    public class JsonContainers
    {
        public class Map
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class MapConfig
        {
            public List<Map> Maps { get; set; }
        }

        public class MugshotLocations
        {
            public List<List<int>> EnemyLocationPoints { get; set; }

            public List<List<int>> AlliedLocationPoints { get; set; }

            public int PortraitHeight { get; set; }

            public int PortraitWidth { get; set; }
        }

        public class MugshotLocationsConfig
        {
            public MugshotLocations FullHD { get; set; }
        }

        public class HeroesConfig
        {
            public List<Hero> Heroes { get; set; }
        }

        public class Hero
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string MugshotFilePath { get; set; }
        }
    }
}
