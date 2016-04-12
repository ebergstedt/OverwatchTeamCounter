using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ebergstedt.Overwatch.Counters.Containers;
using Ebergstedt.Overwatch.Counters.Enums;
using NUnit.Framework;

namespace Ebergstedt.Overwatch.Counters.Test
{
    [TestFixture]
    class HeroIdentifier_Test : TestBase
    {
        readonly JsonConfigLoader _jsonConfigLoader = new JsonConfigLoader(Environment.CurrentDirectory);        
        
        [Test]
        public void IdentifyAllEnemyHeroesByScreenShot()
        {
            HeroMugshotIdentifier heroMugshotIdentifier = new HeroMugshotIdentifier();

            var loadMugshotLocations = JsonConfigLoader.LoadMugshotLocations();

            var extractHeroMugshotsByScreenShot = HeroMugshotBitmapExtractor.ExtractHeroMugshotsByScreenShot(
                                                                                                (Bitmap)Bitmap.FromFile(
                                                                                                                         FakeScreenShotPath),
                                                                                                loadMugshotLocations.EnemyLocationPoints,
                                                                                                loadMugshotLocations.PortraitWidth,
                                                                                                loadMugshotLocations.PortraitHeight);

            List<int> correctMugshotsHeroIdsOnFakeScreenshot = new List<int>()
            {
                17,
                18,
                19,
                1,
                17,
                12
            };

            int index = 0;

            foreach (var mugshot in extractHeroMugshotsByScreenShot)
            {
                var heroIdByMugshot = heroMugshotIdentifier.GetHeroIdByMugshot(
                                                                        mugshot,
                                                                        JsonConfigLoader.LoadHeroIdMugshotBitmaps());

                Assert.True(correctMugshotsHeroIdsOnFakeScreenshot[index] == heroIdByMugshot);

                index++;
            }
        }
    }
}
