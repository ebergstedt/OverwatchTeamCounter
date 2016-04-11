﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            var identifyHeroId = heroIdentifier.GetHeroIdByMugshot(heroWithMetaData.Mugshot);

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
        public void IdentifyAllHeroesByScreenShot()
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

            var extractHeroMugshotsByScreenShot = HeroExtractor.ExtractHeroMugshotsByScreenShot(
                                                                                                (Bitmap)Bitmap.FromFile(FakeScreenShotPath),
                                                                                                HeroLoader.LoadMugshotLocations(MugshotLocationsConfigPath, ScreenResolution.FullHD),
                                                                                                ScreenResolution.FullHD);

            foreach (var mugshot in extractHeroMugshotsByScreenShot)
            {
                var heroIdByMugshot = heroIdentifier.GetHeroIdByMugshot(mugshot);

                Assert.True(heroIdByMugshot > 0);
            }
        }
    }
}