using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

namespace Ebergstedt.Overwatch.Counters
{
    public class MugshotLocations
    {
        public List<List<int>> LocationPoints { get; set; }
    }

    public class MugshotLocationsConfig
    {
        public List<List<int>> FullHD { get; set; }
    }

    public class HeroesConfig
    {
        public List<Hero> Heroes { get; set; }
    }

    public class HeroWithMugShot
    {
        public int Id { get; set; }
        public Bitmap Mugshot { get; set; }
    }

    public class Hero
    {
        public int Id { get; set; }

        public string Name { get; set; }       
        
        public string MugshotFilePath { get; set; } 
    }

    public class HeroWithMetaData
    {
        public HeroWithMetaData(
                                [NotNull] string name,
                                [NotNull] int id,
                                [NotNull] Bitmap mugShot,
                                [NotNull] List<HeroWinRate> winRatesAgainstHeroes, 
                                [NotNull] List<MapWinRate> winRatesOnMaps)
        {            
            if (name == null) throw new ArgumentNullException(nameof(name));            
            if (id <= 0) throw new ArgumentNullException(nameof(id));
            if (mugShot == null) throw new ArgumentNullException(nameof(mugShot));
            if (winRatesAgainstHeroes == null) throw new ArgumentNullException(nameof(winRatesAgainstHeroes));
            if (winRatesOnMaps == null) throw new ArgumentNullException(nameof(winRatesOnMaps));

            Name = name;
            Id = id;
            Mugshot = mugShot;

            WinRatesAgainstHeroes = winRatesAgainstHeroes;
            WinRatesOnMaps = winRatesOnMaps;
        }

        public string Name { get; }
        public int Id { get; }

        public Bitmap Mugshot { get; }

        public List<HeroWinRate> WinRatesAgainstHeroes { get; }
        public List<MapWinRate> WinRatesOnMaps { get; }
    }

    public class HeroWinRate : WinRate
    {
        public HeroWinRate(
                           float percentage, 
                           int heroId) : base(
                                              percentage)
        {
            HeroId = heroId;
        }

        public int HeroId { get; }        
    }

    public class MapWinRate : WinRate
    {
        public MapWinRate(
                          float percentage, 
                          int mapId) : base(
                                            percentage)
        {
            MapId = mapId;
        }

        public int MapId { get; }
    }

    public class WinRate
    {
        public WinRate(
                       float percentage)
        {            
            if (percentage < 0 || percentage > 100)
                throw new ArgumentException($"Percentage:{percentage}");            
            
            Percentage = percentage;
        }

        public float Percentage { get; set; }
    }
}
