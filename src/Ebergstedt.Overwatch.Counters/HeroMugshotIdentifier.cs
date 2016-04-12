using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging;
using Ebergstedt.Overwatch.Counters.Containers;
using JetBrains.Annotations;

namespace Ebergstedt.Overwatch.Counters
{
    public class HeroMugshotIdentifier
    {
        public int GetHeroIdByMugshot(
                                      [NotNull] Bitmap targetHeroBitmap,
                                      IEnumerable<Tuple<int, Bitmap>> templateHeroIdBitmaps)
        {
            if (targetHeroBitmap == null) throw new ArgumentNullException(nameof(targetHeroBitmap));

            List<Tuple<float, int>> results = new List<Tuple<float, int>>();

            foreach (var templateHeroe in templateHeroIdBitmaps)
            {
                Bitmap template = ResizeTemplateToTargetSize(
                                                             targetHeroBitmap,
                                                             templateHeroe.Item2);

                ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0);
                TemplateMatch[] matchings = tm.ProcessImage(
                                                            targetHeroBitmap, 
                                                            template);
                results.Add(new Tuple<float, int>(
                                                  matchings[0].Similarity,
                                                  templateHeroe.Item1));
            }

            if (!results.Any())
                return 0;            

            return results.OrderByDescending(r => r.Item1).First().Item2;
        }

        private Bitmap ResizeTemplateToTargetSize(
                                                  Bitmap target, 
                                                  Bitmap template)
        {
            Bitmap result = (Bitmap) template.Clone();

            if (template.Width > target.Width)
            {
                result = result.Clone(
                                      new Rectangle(
                                                    new Point(
                                                              0,
                                                              0), 
                                                    new Size(
                                                             target.Width, 
                                                             result.Height)), 
                                      result.PixelFormat);

            }

            if (template.Height > target.Height)
            {
                result = result.Clone(
                                      new Rectangle(
                                                    new Point(
                                                              0, 
                                                              0),
                                                    new Size(
                                                             result.Width,
                                                             target.Height)),
                                      result.PixelFormat);

            }

            return result;
        }
    }
}
