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
        readonly HeroLoader _heroLoader = new HeroLoader();        

        [Test]
        public void IdentifyHero()
        {
            var loadFromConfig = _heroLoader.LoadHeroConfig(
                                                            HeroConfigPath,
                                                            Environment.CurrentDirectory);

            var heroWithMetaDatas = _heroLoader.LoadWithMetaData(loadFromConfig);

            HeroIdentifier heroIdentifier = new HeroIdentifier(
                                                               heroWithMetaDatas.Select(h => new HeroWithMugShot()
                                                               {
                                                                   Id = h.Id,
                                                                   Mugshot = h.Mugshot
                                                               })
                                                               .ToList());

            var heroWithMetaData = heroWithMetaDatas.First();

            var identifyHeroId = heroIdentifier.GetHeroIdByMugshot(
                                                                   heroWithMetaData.Mugshot);

            Assert.True(heroWithMetaData.Id == identifyHeroId);
        }

        [Test]
        public void IdentifyHeroWrong()
        {
            var loadFromConfig = _heroLoader.LoadHeroConfig(
                                                            HeroConfigPath,
                                                            Environment.CurrentDirectory);

            var heroWithMetaDatas = _heroLoader.LoadWithMetaData(loadFromConfig);

            HeroIdentifier heroIdentifier = new HeroIdentifier(
                                                               heroWithMetaDatas.Select(h => new HeroWithMugShot()
                                                               {
                                                                   Id = h.Id,
                                                                   Mugshot = h.Mugshot
                                                               })
                                                               .ToList());

            var heroWithMetaData = heroWithMetaDatas.First();
            var heroWithMetaData2 = heroWithMetaDatas.ToList()[2];

            var identifyHeroId = heroIdentifier.GetHeroIdByMugshot(heroWithMetaData2.Mugshot);

            Assert.True(heroWithMetaData.Id != identifyHeroId);
        }

        [Test]
        public void IdentifyAllEnemyHeroesByScreenShot()
        {
            var loadFromConfig = _heroLoader.LoadHeroConfig(
                                                            HeroConfigPath,
                                                            Environment.CurrentDirectory);

            var heroWithMetaDatas = _heroLoader.LoadWithMetaData(loadFromConfig);

            HeroIdentifier heroIdentifier = new HeroIdentifier(
                                                               heroWithMetaDatas.Select(h => new HeroWithMugShot()
                                                               {
                                                                   Id = h.Id,
                                                                   Mugshot = h.Mugshot
                                                               })
                                                               .ToList());

            var loadMugshotLocations = HeroLoader.LoadMugshotLocations(
                                                                        MugshotLocationsConfigPath,
                                                                        ScreenResolution.FullHD);

            var extractHeroMugshotsByScreenShot = HeroExtractor.ExtractHeroMugshotsByScreenShot(
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
                var heroIdByMugshot = heroIdentifier.GetHeroIdByMugshot(mugshot);

                Assert.True(correctMugshotsHeroIdsOnFakeScreenshot[index] == heroIdByMugshot);

                index++;
            }
        }
    }
}
