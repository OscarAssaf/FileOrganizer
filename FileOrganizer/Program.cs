using FileOrganizer;

Console.WriteLine("=== File Organizer ===");
Console.WriteLine();

//  Fråga användaren efter mapp att organisera
string? folderPath = null;

if (args.Length > 0)
{
    folderPath = args[0];
}
else
{
    Console.Write("Ange sökvägen till mappen du vill organisera: ");
    folderPath = Console.ReadLine();
}

if (string.IsNullOrWhiteSpace(folderPath))
{
    Console.WriteLine("Ingen sökväg angavs. Avslutar.");
    return;
}

folderPath = folderPath.Trim().Trim('"');

if (!Directory.Exists(folderPath))
{
    Console.WriteLine($"Mappen '{folderPath}' hittades inte.");
    return;
}

// Fråga om dry-run (bara visa vad som skulle hända, utan att flytta något)
Console.Write("Vill du köra en testkörning (dry-run) utan att faktiskt flytta filer? (j/n): ");
var dryRunAnswer = Console.ReadLine()?.Trim().ToLower();
bool dryRun = dryRunAnswer == "j" || dryRunAnswer == "ja" || dryRunAnswer == "y" || dryRunAnswer == "yes";

Console.WriteLine();
Console.WriteLine(dryRun
    ? $"Kör TESTLÄGE (inga filer flyttas) på: {folderPath}"
    : $"Organiserar filer i: {folderPath}");
Console.WriteLine();

//  Kör organiseringen
var organizer = new FileOrganizerService(dryRun);

List<MoveResult> results;
try
{
    results = organizer.Organize(folderPath);
}
catch (Exception ex)
{
    Console.WriteLine($"Ett fel uppstod: {ex.Message}");
    return;
}

// Visa resultat
if (results.Count == 0)
{
    Console.WriteLine("Inga filer hittades i mappen (undermappar räknas inte).");
    return;
}

int successCount = 0;
int failCount = 0;

foreach (var result in results)
{
    var sourceName = Path.GetFileName(result.SourcePath);
    var destFolder = Path.GetFileName(Path.GetDirectoryName(result.DestinationPath)) ?? "?";

    if (result.Success)
    {
        successCount++;
        var verb = dryRun ? "SKULLE FLYTTAS TILL" : "Flyttad till";
        Console.WriteLine($"[OK] {sourceName} -> {destFolder}/  ({verb})");
    }
    else
    {
        failCount++;
        Console.WriteLine($"[FEL] {sourceName}: {result.Error}");
    }
}

Console.WriteLine();
Console.WriteLine($"Klart! {successCount} fil(er) lyckades, {failCount} misslyckades.");
