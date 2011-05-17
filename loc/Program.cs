using System;
using System.IO;
using System.Linq;
using System.Text;

namespace loc {
    class Program {
        static void Main(string[] args) {
            PrintApplicationInformation();

            if(args.Length < 1) {
                PrintUsageAndExit();
            }

            var path = args[0];
            
            var extensions = args.Skip(1).ToArray();
            if(extensions.Length == 0) {
                extensions = new[] {"*.cs", "*.aspx"};
            }

            Console.WriteLine("Path: " + path);
            Console.WriteLine("Extensions " + string.Join(" ", extensions));

            var loc = 0;
            var fileCount = 0;

            foreach (var ext in extensions) {
                var files = Directory.GetFiles(path, ext, SearchOption.AllDirectories);
                fileCount += files.Length;
                
                foreach (var file in files) {
                    var count = CountLoc(file);
                    loc += count;
                }
            }

            Console.WriteLine("Total files: " + fileCount);
            Console.WriteLine("Total lines of code: " + loc);
            Console.WriteLine("Average lines of code / file: " + Math.Round((decimal)loc / fileCount, 2));
            Console.ReadLine();
        }

        static int CountLoc(string path) {
            if (!File.Exists(path))
                return 0;

            return File.ReadAllLines(path, Encoding.UTF8).Count();
        }

        static void PrintUsageAndExit() {
            Console.WriteLine("Usage: loc.exe <path> [<extension>]");
            Console.WriteLine("Example: loc.exe C:\\projects\\myproject *.cs *.aspx *.js *.css");
            Environment.Exit(0);
        }

        static void PrintApplicationInformation() {
            var version = typeof(Program).Assembly.GetName().Version;
            Console.WriteLine("loc v{0}", version);
            Console.WriteLine(string.Format("Copyright (c) 2010-{0} Alexander Nyquist", DateTime.Today.Year));
            Console.WriteLine("http://github.com/alexandernyquist/loc");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
