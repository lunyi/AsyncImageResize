using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output"); ;
            string destinationPathAsync = Path.Combine(Environment.CurrentDirectory, "outputAsync"); ;

            ImageProcess imageProcess = new ImageProcess();

            var before = RunImages(imageProcess, destinationPath, sourcePath);
            var after = await RunImagesAsync(imageProcess, destinationPathAsync, sourcePath);
            var percent = 100 * (before - after) / before;

            Console.WriteLine($"效能提升百分比: {percent} %");

            Console.ReadKey();
        }

        private static long RunImages(ImageProcess imageProcess, string destinationPath, string sourcePath)
        {
            imageProcess.Clean(destinationPath);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
            sw.Stop();
            var milliseconds = sw.ElapsedMilliseconds;
            Console.WriteLine($"同步處理花費時間: {milliseconds} ms");
            return milliseconds;
        }

        private static async Task<long> RunImagesAsync(ImageProcess imageProcess, string destinationPath, string sourcePath)
        {
            imageProcess.Clean(destinationPath);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await imageProcess.ResizeImagesAsync(sourcePath, destinationPath, 2.0);
            sw.Stop();
            var milliseconds = sw.ElapsedMilliseconds;
            Console.WriteLine($"非同步處理花費時間: {milliseconds} ms");
            return milliseconds;
        }
    }
}
