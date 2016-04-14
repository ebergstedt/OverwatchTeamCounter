using System;
using System.Collections;
using System.Collections.Generic;
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
            Cleanup();

            ScreenCapturer screenCapturer = new ScreenCapturer();
            HeroScreenshotIdentityExtractor heroScreenshotIdentityExtractor = new HeroScreenshotIdentityExtractor();
            HeroStatisticsCalculator heroStatisticsCalculator = new HeroStatisticsCalculator();
            
            while (true)
            {
                int mapId = 1;

                Console.WriteLine($"Chosen map: {mapId}");

                Console.WriteLine("Taking sceenshot.");

                var activeScreenCapture = screenCapturer.GetActiveScreenCapture(
                                                                                 ScreenBoundsCalculator.GetRectangleByScreenResolution(
                                                                                                                                       ScreenResolution.FullHD));

                IEnumerable<int> enemyHeroIds = heroScreenshotIdentityExtractor.FindEnemyHeroesByScreenshot(activeScreenCapture);

                Console.WriteLine($"Enemy heroes found: { JsonConvert.SerializeObject(enemyHeroIds?.Select(id => new { id }))}");

                IEnumerable<int> friendlyHeroIds = heroScreenshotIdentityExtractor.FindAlliedHeroesByScreenshot(activeScreenCapture);

                Console.WriteLine($"Friendly heroes found: { JsonConvert.SerializeObject(friendlyHeroIds?.Select(id => new { id }))}");

                Console.WriteLine("Calculating winrates.");

                IEnumerable<int> bestHeroIds = heroStatisticsCalculator.GetBestOrderedHeroIdCountersForTeamComposition(
                                                                                                                       enemyHeroIds,
                                                                                                                       mapId);

                Console.WriteLine($"Best hero to chose (in order): { JsonConvert.SerializeObject(bestHeroIds?.Select(id => new { id }))}");

                Console.WriteLine("Sleeping 5000ms.");
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
