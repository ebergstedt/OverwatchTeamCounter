using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ebergstedt.Overwatch.Counters.Enums;
using JetBrains.Annotations;

namespace Ebergstedt.Overwatch.Counters
{
    public class HeroExtractor
    {
        public IEnumerable<Bitmap> ExtractHeroMugshotsByScreenShot(
                                                                   [NotNull] Bitmap screenShot,
                                                                   [NotNull] MugshotLocations mugshotLocations,
                                                                   ScreenResolution screenResolution = ScreenResolution.FullHD,
                                                                   int mugshotWidth = 73,
                                                                   int mugshotHeight = 43
                                                                   )
        {
            if (screenShot == null) throw new ArgumentNullException(nameof(screenShot));
            if (mugshotLocations == null) throw new ArgumentNullException(nameof(mugshotLocations));

            switch (screenResolution)
            {
                case ScreenResolution.FullHD:
                    return HandleFullHD(
                                        screenShot,
                                        mugshotLocations,
                                        mugshotWidth,
                                        mugshotHeight);
            }

            throw new NotImplementedException(nameof(screenResolution));
        }

        private IEnumerable<Bitmap> HandleFullHD(
                                                 [NotNull] Bitmap screenShot,
                                                 [NotNull] MugshotLocations mugshotLocations,
                                                 int mugshotWidth,
                                                 int mugshotHeight)
        {
            if (screenShot == null) throw new ArgumentNullException(nameof(screenShot));
            if (mugshotLocations == null) throw new ArgumentNullException(nameof(mugshotLocations));

            foreach (var mugshotLocationPoint in mugshotLocations.LocationPoints)
            {
                Rectangle bounds = new Rectangle(
                                                 new Point(mugshotLocationPoint[0], mugshotLocationPoint[1]), 
                                                 new Size(mugshotWidth, mugshotHeight));

                yield return screenShot.Clone(bounds, screenShot.PixelFormat);
            }
        }
    }
}
