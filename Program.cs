using System.Diagnostics;
using System.Net;

namespace MainProgram
{
    class Program
    {
        static void Main()
        {
            CreateFolders();

            Console.WriteLine("Enter the name of the file that is in the data folder");
            Console.ForegroundColor = ConsoleColor.Blue;
            string[] namesArr = Directory.GetFiles("data");
            if(namesArr.Length != 0)
            {
                Console.WriteLine($"Found files: {namesArr.Length}");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Please put video in data folder");
                Console.ResetColor();
                Console.ReadKey();
                Environment.Exit(0);
            }
            foreach (var item in namesArr)
            {
                Console.WriteLine(item[5..^0]);
            }

            Console.Write("File name: ");
            string input = Console.ReadLine() ?? "no";

            Console.WriteLine("What format to convert to (default mp4)");
            string renderExtensionInput = Console.ReadLine() ?? "mp4";
            if (renderExtensionInput == "")
            {
                renderExtensionInput = "mp4";
            }

            Process process = new();
            process.StartInfo.FileName = @"lib\ffmpeg.exe";
            string fullPath = $"-i \"data\\{input}\" \"render\\{input[0..^4]}.{renderExtensionInput}\"";
            process.StartInfo.Arguments = fullPath;
            process.Start();
            process.WaitForExit();
        }

        private static void CreateFolders()
        {
            string[] paths = { "data", "render", "lib" };
            foreach (var item in paths)
            {
                if (!Directory.Exists(item))
                {
                    Directory.CreateDirectory(item);
                    if (item == "lib")
                    {
                        bool file = File.Exists(@"lib\ffmpeg.exe");
                        if (!file)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Not Found FFmpeg binary file");
                            Console.WriteLine("Please download ffmpeg.exe ");
                            Console.ResetColor();
                            Console.ReadKey();
                            Environment.Exit(0);
                        }
                    }
                }
            }
        }
    }
}