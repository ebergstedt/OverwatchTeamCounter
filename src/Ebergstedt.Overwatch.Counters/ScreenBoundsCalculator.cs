using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ebergstedt.Overwatch.Counters.Enums;

namespace Ebergstedt.Overwatch.Counters
{

    public static class ScreenBoundsCalculator
    {
        public static Rectangle GetRectangleByScreenResolution(
                                                               ScreenResolution screenResolution = ScreenResolution.FullHD)
        {
            switch (screenResolution)
            {
                case ScreenResolution.FullHD:
                    return new Rectangle(
                                         new Point(0, 0),
                                         new Size(1920, 1080));
            }

            throw new NotImplementedException(nameof(screenResolution));
        }        
    }
}
