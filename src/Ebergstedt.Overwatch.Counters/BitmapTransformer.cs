using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Ebergstedt.Overwatch.Counters
{
    public class BitmapTransformer
    {
        public static Bitmap TransformBitmapToJpg(
                                                  [NotNull] Bitmap bitmap)
        {
            if (bitmap == null) throw new ArgumentNullException(nameof(bitmap));

            string fileName = $"screenshot-{DateTime.UtcNow.ToString().Replace(" ", "-").Replace(":", "-")}.jpg";
            
            bitmap.Save(fileName, ImageFormat.Jpeg);

            return (Bitmap) Bitmap.FromFile(fileName);
        }
    }
}
