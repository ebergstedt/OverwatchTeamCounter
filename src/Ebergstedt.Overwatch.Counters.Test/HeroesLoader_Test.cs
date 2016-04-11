using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Ebergstedt.Overwatch.Counters.Test
{
    [TestFixture]
    public class HeroesLoader_Test : TestBase
    {

        [Test]
        public void LoadMugshotsConfig()
        {
            var mugshotLocations = HeroLoader.LoadEnemyMugshotLocations(
                                                                   MugshotLocationsConfigPath);

            Assert.True(mugshotLocations.LocationPoints.Any());
        }

        [Test]
        public void HeroesConfig()
        {            

            var heroes = HeroLoader.LoadHeroConfig(
                                                   HeroConfigPath,
                                                   Environment.CurrentDirectory);

            Assert.True(heroes.Any());
        }

        [Test]
        public void MetaData()
        {
            var heroes = HeroLoader.LoadHeroConfig(
                                                   HeroConfigPath,
                                                   Environment.CurrentDirectory);

            var heroWithMetaDatas = HeroLoader.LoadWithMetaData(
                                                                heroes);

            Assert.True(heroWithMetaDatas.Any());
        }
    }
}
