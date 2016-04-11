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

            foreach (var heroWithMugShot in _heroesWithMugShots)
            {
                ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0);
                // compare two images
                TemplateMatch[] matchings = tm.ProcessImage(targetHeroBitmap, heroWithMugShot.Mugshot);
                //// check similarity level
                if (matchings[0].Similarity > 0.95)
                {
                    return heroWithMugShot.Id;
                }
            }

            return 0;
        }
    }
}
