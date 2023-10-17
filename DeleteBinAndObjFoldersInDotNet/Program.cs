using System;
using System.IO;
using System.Linq;

namespace CleanUpUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the path of the root directory to search for .NET solution folders:");
            string rootPath = Console.ReadLine();

            // Check if root directory exists
            if (!Directory.Exists(rootPath))
            {
                Console.WriteLine($"Directory '{rootPath}' not found.");
                return;
            }

            // Get all .sln files in directory and its sub directories
            var solutionFiles = Directory.GetFiles(rootPath, "*.sln", SearchOption.AllDirectories);

            if (!solutionFiles.Any())
            {
                Console.WriteLine("No .NET solution files found in the specified directory.");
                return;
            }

            bool? deleteAllBin = null;
            bool? deleteAllObj = null;

            foreach (var solutionFile in solutionFiles)
            {
                Console.WriteLine($"Found solution: {solutionFile}");

                string solutionDirectory = Path.GetDirectoryName(solutionFile);

                deleteAllBin = DeleteFolders(solutionDirectory, "bin", deleteAllBin);
                deleteAllObj = DeleteFolders(solutionDirectory, "obj", deleteAllObj);
            }

            Console.WriteLine("Process completed.");
        }

        static bool? DeleteFolders(string solutionDirectory, string folderName, bool? deleteAll)
        {
            var folders = Directory.GetDirectories(solutionDirectory, folderName, SearchOption.AllDirectories);
            foreach (var folder in folders)
            {
                Console.WriteLine($"Found {folderName} directory: {folder}");

                if (deleteAll == null)
                {
                    Console.Write("Do you want to delete it? (Y/N/YA/NA): ");
                    var choice = Console.ReadLine();

                    if (choice?.ToUpper() == "YA")
                    {
                        deleteAll = true;
                    }
                    else if (choice?.ToUpper() == "NA")
                    {
                        deleteAll = false;
                    }
                    else if (choice?.ToUpper() != "Y")
                    {
                        continue;
                    }
                }

                try
                {
                    Directory.Delete(folder, true);
                    Console.WriteLine($"Deleted {folderName} directory: {folder}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting {folderName} directory: {ex.Message}");
                }
            }

            return deleteAll;
        }
    }
}
