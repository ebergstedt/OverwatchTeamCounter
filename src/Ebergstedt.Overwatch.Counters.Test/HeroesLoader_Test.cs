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
            var mugshotLocations = JsonConfigLoader.GetMugshotLocations();

            Assert.True(mugshotLocations.EnemyLocationPoints.Any());
        }

        [Test]
        public void HeroesConfig()
        {            

            var heroes = JsonConfigLoader.GetHeroConfig();

            Assert.True(heroes.Any());
        }
    }
}
