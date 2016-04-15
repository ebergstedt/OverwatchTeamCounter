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
        
        static readonly ScreenCapturer ScreenCapturer = new ScreenCapturer();
        static readonly HeroScreenshotIdentityExtractor HeroScreenshotIdentityExtractor = new HeroScreenshotIdentityExtractor();
        static readonly StatisticsCalculator StatisticsCalculator = new StatisticsCalculator();
        static readonly MetaDataHelper MetaDataHelper = new MetaDataHelper();

        static int CurrentMapId { get; set; }

        static void Main(string[] args)
        {            
            bool debugMode = args.Contains("-debug");             

            Cleanup();

            CurrentMapId = GetMapId();

            while (true)
            {
                Console.WriteLine("\n\n--------------------------\nPress ENTER to take a screenshot and start the calculation.");
                Console.WriteLine("Write 'map' to choose map.");

                var readLine = Console.ReadLine();

                if (readLine == "map")
                {
                    CurrentMapId = GetMapId();
                    continue;
                }

                Console.WriteLine("Taking screenshot in 3 seconds...");

                Thread.Sleep(3000);

                Console.WriteLine("Taking sceenshot.");

                var activeScreenCapture = ScreenCapturer.GetActiveScreenCapture(
                                                                                 ScreenBoundsCalculator.GetRectangleByScreenResolution(
                                                                                                                                       ScreenResolution.FullHD));

                Console.WriteLine("Screenshot saved. Extracting hero data from screenshot.");

                IEnumerable<int> enemyHeroIds = HeroScreenshotIdentityExtractor.FindEnemyHeroesByScreenshot(activeScreenCapture);
                
                Console.WriteLine($"Enemy heroes found: { JsonConvert.SerializeObject(enemyHeroIds?.Select(heroId => new { Hero = MetaDataHelper.GetHeroNameById(heroId) }))}");

                IEnumerable<int> friendlyHeroIds = HeroScreenshotIdentityExtractor.FindAlliedHeroesByScreenshot(activeScreenCapture);
                
                Console.WriteLine($"Friendly heroes found: { JsonConvert.SerializeObject(friendlyHeroIds?.Select(heroId => new { Hero = MetaDataHelper.GetHeroNameById(heroId) }))}");

                Console.WriteLine("Calculating winrates.");

                IEnumerable<int> bestHeroIds = StatisticsCalculator.GetBestOrderedHeroIdCountersForTeamComposition(
                                                                                                                   enemyHeroIds,
                                                                                                                   CurrentMapId);
                Console.WriteLine("Calculation finished.");

                Console.WriteLine($"\nBest hero to chose (in order): ");
                int i = 1;
                foreach (var heroId in bestHeroIds.Take(10))
                {                    
                    Console.WriteLine($"{i}. { MetaDataHelper.GetHeroNameById(heroId)} ");
                    i++;
                }
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

        private static int GetMapId()
        {
            DisplayChooseMap();
            var readLine = Console.ReadLine();

            int mapId;
            while (!int.TryParse(readLine, out mapId))
            {
                DisplayChooseMap();
                readLine = Console.ReadLine();
            }

            Console.WriteLine($"Chosen map: {MetaDataHelper.GetMapNameById(CurrentMapId)}");

            return mapId;
        }

        private static void DisplayChooseMap()
        {
            Console.WriteLine("-- Choose a map --");
            foreach (var map in MetaDataHelper.GetMaps())
            {
                Console.WriteLine($"Id: {map.Id} Name: {map.Name}");
            }

            Console.WriteLine("Write the Id number and press ENTER: ");
        }
    }
}
