﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ebergstedt.Overwatch.Counters.Containers;
using Ebergstedt.Overwatch.Counters.Enums;
using JetBrains.Annotations;

namespace Ebergstedt.Overwatch.Counters
{
    public class HeroScreenshotIdentityExtractor
    {
        JsonConfigLoader JsonConfigLoader => new JsonConfigLoader(Environment.CurrentDirectory);

        HeroMugshotBitmapExtractor HeroMugshotBitmapExtractor => new HeroMugshotBitmapExtractor();
        
        HeroMugshotIdentifier HeroMugshotIdentifier => new HeroMugshotIdentifier();

        readonly IEnumerable<JsonContainers.Hero> _heroList;
        readonly JsonContainers.MugshotLocations _mugshotLocations;
        readonly IEnumerable<Tuple<int, Bitmap>> _heroIdMugshotBitmaps;

        public HeroScreenshotIdentityExtractor()
        {
            _heroList = JsonConfigLoader.GetHeroConfig();
            _mugshotLocations = JsonConfigLoader.GetMugshotLocations();
            _heroIdMugshotBitmaps = JsonConfigLoader.GetHeroIdMugshotBitmaps();
        }
        
        public IEnumerable<int> FindEnemyHeroesByScreenshot(
                                                            [NotNull] Bitmap screenShot)
        {
            if (screenShot == null) throw new ArgumentNullException(nameof(screenShot));

            var heroesByScreenshot = HeroMugshotBitmapExtractor.ExtractHeroMugshotsByScreenShot(
                                                                                               screenShot,
                                                                                               _mugshotLocations.EnemyLocationPoints,
                                                                                               _mugshotLocations.PortraitWidth,
                                                                                               _mugshotLocations.PortraitHeight
                                                                                               );



            IEnumerable<int> identifiedEnemyHeroIds = heroesByScreenshot
                                                      .Select(bitmap => HeroMugshotIdentifier.GetHeroIdByMugshot(
                                                                                                                 bitmap,
                                                                                                                 _heroIdMugshotBitmaps));

            return _heroList
                   .Where(h => identifiedEnemyHeroIds.Contains(
                                                               h.Id))
                   .Select(h => h.Id);            
        }

        public IEnumerable<int> FindAlliedHeroesByScreenshot(
                                                             [NotNull] Bitmap screenShot)
        {
            if (screenShot == null) throw new ArgumentNullException(nameof(screenShot));

            var heroesByScreenshot = HeroMugshotBitmapExtractor.ExtractHeroMugshotsByScreenShot(
                                                                                               screenShot,
                                                                                               _mugshotLocations.AlliedLocationPoints,
                                                                                               _mugshotLocations.PortraitWidth,
                                                                                               _mugshotLocations.PortraitHeight
                                                                                               );



            IEnumerable<int> identifiedAlliedHeroIds = heroesByScreenshot
                                                       .Select(bitmap => HeroMugshotIdentifier.GetHeroIdByMugshot(
                                                                                                                  bitmap, 
                                                                                                                  _heroIdMugshotBitmaps));

            return _heroList
                   .Where(h => identifiedAlliedHeroIds.Contains(
                                                                h.Id))
                   .Select(h => h.Id);
        }
    }
}
