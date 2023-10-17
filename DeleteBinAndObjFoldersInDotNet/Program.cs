static bool? DeleteFolders(string solutionDirectory, string folderName, bool? deleteAll)
{
    var folders = Directory.GetDirectories(solutionDirectory, folderName, SearchOption.AllDirectories);
    foreach (var folder in folders)
    {
        Console.WriteLine($"Found {folderName} directory: {folder}");

        if (deleteAll == null)
        {
            Console.Write("Do you want to delete it? (Y/N/YA/NA): ");
            var choice = Console.ReadLine().ToUpper();

            switch (choice)
            {
                case "YA":
                    deleteAll = true;
                    break;
                case "NA":
                    deleteAll = false;
                    break;
                case "Y":
                    break;
                case "N":
                    continue;
                default:
                    Console.WriteLine("Invalid choice. Skipping this folder.");
                    continue;
            }
        }

        if (deleteAll == true) // Only attempt deletion if "Yes" or "Yes to All" was chosen.
        {
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
    }

    return deleteAll;
}