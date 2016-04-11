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
                                                                   [NotNull] MugshotLocations mugshotLocations)
        {
            if (screenShot == null) throw new ArgumentNullException(nameof(screenShot));
            if (mugshotLocations == null) throw new ArgumentNullException(nameof(mugshotLocations));


            foreach (var mugshotLocationPoint in mugshotLocations.LocationPoints)
            {
                Rectangle bounds = new Rectangle(
                                                 new Point(
                                                           mugshotLocationPoint[0],
                                                           mugshotLocationPoint[1]),
                                                 new Size(
                                                          mugshotLocations.PortraitWidth,
                                                          mugshotLocations.PortraitHeight));

                yield return screenShot.Clone(
                                              bounds,
                                              screenShot.PixelFormat);
            }
        }
    }
}
