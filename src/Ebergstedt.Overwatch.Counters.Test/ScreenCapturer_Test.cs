using System;
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
    class ScreenCapturer_Test : TestBase
    { 
        [Test]
        public void ScreenShot()
        {
            var activeScreenCapture = ScreenCapturer.GetActiveScreenCapture(
                                                                             ScreenBounds.GetRectangleByScreenResolution(
                                                                                                                         ScreenResolution.FullHD));

            Assert.NotNull(activeScreenCapture);
        }
    }
}
