using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ebergstedt.Overwatch.Counters;
using Ebergstedt.Overwatch.Counters.Containers;
using Ebergstedt.Overwatch.Counters.Enums;
using Newtonsoft.Json;

namespace Ebergstedt.Overwatch.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {            
            bool debugMode = args.Contains("-debug");             

            Cleanup();

            ScreenCapturer screenCapturer = new ScreenCapturer();
            HeroScreenshotIdentityExtractor heroScreenshotIdentityExtractor = new HeroScreenshotIdentityExtractor();
            StatisticsCalculator statisticsCalculator = new StatisticsCalculator();
            MetaDataHelper metaDataHelper = new MetaDataHelper();

            while (true)
            {
                int mapId = 1;

                Console.WriteLine($"Chosen map: {metaDataHelper.GetMapNameById(mapId)}");

                Console.WriteLine("Taking sceenshot.");

                var activeScreenCapture = screenCapturer.GetActiveScreenCapture(
                                                                                 ScreenBoundsCalculator.GetRectangleByScreenResolution(
                                                                                                                                       ScreenResolution.FullHD));

                IEnumerable<int> enemyHeroIds = heroScreenshotIdentityExtractor.FindEnemyHeroesByScreenshot(activeScreenCapture);
                
                Console.WriteLine($"Enemy heroes found: { JsonConvert.SerializeObject(enemyHeroIds?.Select(heroId => new { Hero = metaDataHelper.GetHeroNameById(heroId) }))}");

                IEnumerable<int> friendlyHeroIds = heroScreenshotIdentityExtractor.FindAlliedHeroesByScreenshot(activeScreenCapture);
                
                Console.WriteLine($"Friendly heroes found: { JsonConvert.SerializeObject(friendlyHeroIds?.Select(heroId => new { Hero = metaDataHelper.GetHeroNameById(heroId) }))}");

                Console.WriteLine("Calculating winrates.");

                IEnumerable<int> bestHeroIds = statisticsCalculator.GetBestOrderedHeroIdCountersForTeamComposition(
                                                                                                                   enemyHeroIds,
                                                                                                                   mapId);

                Console.WriteLine($"\nBest hero to chose (in order): ");
                int i = 1;
                foreach (var heroId in bestHeroIds.Take(10))
                {                    
                    Console.WriteLine($"{i}. { metaDataHelper.GetHeroNameById(heroId)} ");
                    i++;
                }

                Console.WriteLine("\nSleeping 5000ms.");
                Thread.Sleep(5000);

                Console.Clear();
            }
        }

        private static void Cleanup()
        {
            var filePaths = Directory.GetFiles(
                                               Environment.CurrentDirectory,
                                               "screenshot*.jpg",
                                               SearchOption.TopDirectoryOnly);

            foreach (var filePath in filePaths)
            {
                File.Delete(filePath);
            }
        }
    }
}
