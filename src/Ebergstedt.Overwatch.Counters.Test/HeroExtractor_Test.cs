using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ebergstedt.Overwatch.Counters.Enums;
using NUnit.Framework;

namespace Ebergstedt.Overwatch.Counters.Test
{
    [TestFixture]
    public class HeroExtractor_Test : TestBase
    {
        [Test]
        public void ExtractEnemyHeroMugshotsByScreenShot()
        {
            var loadMugshotLocations = HeroLoader.LoadMugshotLocations(
                                                                        MugshotLocationsConfigPath,
                                                                        ScreenResolution.FullHD);

            var extractHeroMugshotsByScreenShot = HeroExtractor.ExtractHeroMugshotsByScreenShot(
                                                                                                (Bitmap) Bitmap.FromFile(
                                                                                                                         FakeScreenShotPath),
                                                                                                loadMugshotLocations.EnemyLocationPoints,
                                                                                                loadMugshotLocations.PortraitWidth,
                                                                                                loadMugshotLocations.PortraitHeight);

            Assert.True(extractHeroMugshotsByScreenShot.Any());
        }

        [Test]
        public void ExtractAlliedHeroMugshotsByScreenShot()
        {
            var loadMugshotLocations = HeroLoader.LoadMugshotLocations(
                                                                        MugshotLocationsConfigPath,
                                                                        ScreenResolution.FullHD);

            var extractHeroMugshotsByScreenShot = HeroExtractor.ExtractHeroMugshotsByScreenShot(
                                                                                                (Bitmap)Bitmap.FromFile(
                                                                                                                         FakeScreenShotPath),
                                                                                                loadMugshotLocations.AlliedLocationPoints,
                                                                                                loadMugshotLocations.PortraitWidth,
                                                                                                loadMugshotLocations.PortraitHeight);

            Assert.True(extractHeroMugshotsByScreenShot.Any());
        }
    }
}
