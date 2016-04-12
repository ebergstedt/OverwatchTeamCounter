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

        internal readonly JsonConfigLoader JsonConfigLoader = new JsonConfigLoader(Environment.CurrentDirectory);
        internal readonly HeroMugshotBitmapExtractor HeroMugshotBitmapExtractor = new HeroMugshotBitmapExtractor();
        internal readonly ScreenCapturer ScreenCapturer = new ScreenCapturer();
    }
}
