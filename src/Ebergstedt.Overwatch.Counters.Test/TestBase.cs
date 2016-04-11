using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebergstedt.Overwatch.Counters.Test
{
    public class TestBase
    {
        internal readonly string FakeScreenShotPath = Path.Combine(Environment.CurrentDirectory, "FakeScreenshot.jpg");
        internal readonly string MugshotLocationsConfigPath = Path.Combine(Environment.CurrentDirectory, "MugshotLocations.json");
        internal readonly string HeroConfigPath = Path.Combine(Environment.CurrentDirectory, "Heroes.json");

        internal readonly HeroLoader HeroLoader = new HeroLoader();
        internal readonly HeroExtractor HeroExtractor = new HeroExtractor();
        internal readonly ScreenCapturer ScreenCapturer = new ScreenCapturer();
    }
}
