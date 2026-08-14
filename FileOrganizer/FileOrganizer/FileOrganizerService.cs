namespace FileOrganizer;


public record MoveResult(string SourcePath, string DestinationPath, bool Success, string? Error = null);


///  skanna en mapp och flytta filer till kategoriserade undermappar.

public class FileOrganizerService
{
    private readonly bool _dryRun;

    public FileOrganizerService(bool dryRun = false)
    {
        _dryRun = dryRun;
    }


    public List<MoveResult> Organize(string sourceFolder)
    {
        if (!Directory.Exists(sourceFolder))
        {
            throw new DirectoryNotFoundException($"Mappen hittades inte: {sourceFolder}");
        }

        var results = new List<MoveResult>();

        var files = Directory.GetFiles(sourceFolder);

        foreach (var filePath in files)
        {
            var fileName = Path.GetFileName(filePath);
            var category = FileCategoryMap.GetCategory(filePath);
            var categoryFolder = Path.Combine(sourceFolder, category);
            var destinationPath = Path.Combine(categoryFolder, fileName);

            try
            {
                if (!_dryRun)
                {
                    Directory.CreateDirectory(categoryFolder);
                    destinationPath = GetUniqueDestination(destinationPath);
                    File.Move(filePath, destinationPath);
                }
                else
                {
                    destinationPath = GetUniqueDestination(destinationPath, simulateExisting: false);
                }

                results.Add(new MoveResult(filePath, destinationPath, true));
            }
            catch (Exception ex)
            {
                results.Add(new MoveResult(filePath, destinationPath, false, ex.Message));
            }
        }

        return results;
    }


    /// Samma namn, lägger (1) och (2) etc

    private static string GetUniqueDestination(string destinationPath, bool simulateExisting = true)
    {
        if (!simulateExisting || !File.Exists(destinationPath))
        {
            return destinationPath;
        }

        var directory = Path.GetDirectoryName(destinationPath)!;
        var fileNameWithoutExt = Path.GetFileNameWithoutExtension(destinationPath);
        var extension = Path.GetExtension(destinationPath);

        int counter = 1;
        string newPath;
        do
        {
            var newFileName = $"{fileNameWithoutExt} ({counter}){extension}";
            newPath = Path.Combine(directory, newFileName);
            counter++;
        } while (File.Exists(newPath));

        return newPath;
    }
}
