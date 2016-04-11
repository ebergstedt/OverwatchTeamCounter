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
        public void ExtractHeroMugshotsByScreenShot()
        {
            var extractHeroMugshotsByScreenShot = HeroExtractor.ExtractHeroMugshotsByScreenShot(
                                                                                                (Bitmap) Bitmap.FromFile(
                                                                                                                         FakeScreenShotPath),
                                                                                                HeroLoader.LoadEnemyMugshotLocations(
                                                                                                                                MugshotLocationsConfigPath, 
                                                                                                                                ScreenResolution.FullHD));

            Assert.True(extractHeroMugshotsByScreenShot.Any());
        }
    }
}
