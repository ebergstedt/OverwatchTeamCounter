using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Ebergstedt.Overwatch.Counters
{
    public class ScreenCapturer
    {
        public Bitmap GetActiveScreenCapture(
                                             [NotNull] Rectangle bounds)
        {            
            using (Bitmap bitmap = new Bitmap(
                                              bounds.Width, 
                                              bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(
                                     new Point(
                                               bounds.Left, 
                                               bounds.Top), 
                                     Point.Empty, 
                                     bounds.Size);
                }

                return bitmap.Clone(
                                    bounds, 
                                    bitmap.PixelFormat);
            }
        }
    }
}
