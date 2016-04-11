using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging;
using JetBrains.Annotations;

namespace Ebergstedt.Overwatch.Counters
{
    public class HeroIdentifier
    {
        private List<HeroWithMugShot> _heroesWithMugShots { get; }

        public HeroIdentifier([NotNull]List<HeroWithMugShot> heroesWithMugShots)
        {
            if (heroesWithMugShots == null) throw new ArgumentNullException(nameof(heroesWithMugShots));

            _heroesWithMugShots = heroesWithMugShots;
        }

        public int GetHeroIdByMugshot([NotNull] Bitmap targetHeroBitmap)
        {
            if (targetHeroBitmap == null) throw new ArgumentNullException(nameof(targetHeroBitmap));

            List<Tuple<float, int>> results = new List<Tuple<float, int>>();

            foreach (var heroWithMugShot in _heroesWithMugShots)
            {
                Bitmap template = ResizeTemplateToTargetSize(
                                                             targetHeroBitmap, 
                                                             heroWithMugShot.Mugshot);

                ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0);
                TemplateMatch[] matchings = tm.ProcessImage(
                                                            targetHeroBitmap, 
                                                            template);
                results.Add(new Tuple<float, int>(
                                                  matchings[0].Similarity,
                                                  heroWithMugShot.Id));
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
