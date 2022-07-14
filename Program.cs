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
                Console.WriteLine(item[5..^4]);
            }
            Console.Write("File name: ");
            string? input = Console.ReadLine();

            Console.WriteLine("What is the extension of the file (mkv by default)");
            string extensionInput = Console.ReadLine() ?? "mkv";
            if (extensionInput == "")
            {
                extensionInput = "mkv";
            }

            Console.WriteLine("What format to convert to (default mp4)");
            string renderExtensionInput = Console.ReadLine() ?? "mp4";
            if (renderExtensionInput == "")
            {
                renderExtensionInput = "mp4";
            }

            Process process = new();
            process.StartInfo.FileName = @"lib\ffmpeg.exe";
            process.StartInfo.Arguments = $"-i \"data\\{input}.{extensionInput}\" \"render\\{input}.{renderExtensionInput}\"";
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
                        Console.WriteLine("First run install ffmpeg, it will take some time");
                        WebClient webClient = new();
                        string path = @"lib\ffmpeg.exe";
                        webClient.DownloadFile("https://drive.google.com/uc?export=download&id=15TzAf6oe52kPUnQN2GoOcYGrWQyA5QyP", path);
                    }
                }
            }
        }
    }
}