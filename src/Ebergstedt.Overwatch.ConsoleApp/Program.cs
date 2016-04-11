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
            HeroFinder heroFinder = new HeroFinder();

            //ghetto solution to be fixed
            while (true)
            {
                Console.WriteLine("Taking sceenshot.");

                var activeScreenCapture = screenCapturer.GetActiveScreenCapture(
                                                                                 ScreenBounds.GetRectangleByScreenResolution(
                                                                                                                             ScreenResolution.FullHD));
                IEnumerable<Hero> enemyHeroes = heroFinder.FindEnemyHeroesByScreenshot(activeScreenCapture);
                
                Console.WriteLine($"Enemy heroes found: { JsonConvert.SerializeObject(enemyHeroes?.Select(s => new { s.Name }))}");

                IEnumerable<Hero> friendlyHeroes = heroFinder.FindAlliedHeroesByScreenshot(activeScreenCapture);

                Console.WriteLine($"Friendly heroes found: { JsonConvert.SerializeObject(friendlyHeroes?.Select(s => new { s.Name }))}");

                Console.WriteLine("Calculating winrates.");

                Console.WriteLine("Best hero to chose currently:");

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
