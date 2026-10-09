namespace FileOrganizer;
public static class FileCategoryMap
{
    // Nyckel = filändelse (utan punkt), Värde = mappnamn
    private static readonly Dictionary<string, string> Categories = new(StringComparer.OrdinalIgnoreCase)
    {
        // Bilder
        ["jpg"] = "Bilder",
        ["jpeg"] = "Bilder",
        ["png"] = "Bilder",
        ["gif"] = "Bilder",
        ["bmp"] = "Bilder",
        ["svg"] = "Bilder",
        ["webp"] = "Bilder",
        ["heic"] = "Bilder",
        ["tiff"] = "Bilder",

        // Dokument
        ["pdf"] = "Dokument",
        ["doc"] = "Dokument",
        ["docx"] = "Dokument",
        ["txt"] = "Dokument",
        ["rtf"] = "Dokument",
        ["odt"] = "Dokument",
        ["md"] = "Dokument",

        // Kalkylark och presentationer
        ["xls"] = "Kalkylark",
        ["xlsx"] = "Kalkylark",
        ["csv"] = "Kalkylark",
        ["ppt"] = "Presentationer",
        ["pptx"] = "Presentationer",

        // Videor
        ["mp4"] = "Video",
        ["mov"] = "Video",
        ["avi"] = "Video",
        ["mkv"] = "Video",
        ["wmv"] = "Video",
        ["flv"] = "Video",

        // musik
        ["mp3"] = "Musik",
        ["wav"] = "Musik",
        ["flac"] = "Musik",
        ["aac"] = "Musik",
        ["m4a"] = "Musik",

        // zip filer (arkiv)
        ["zip"] = "Arkiv",
        ["rar"] = "Arkiv",
        ["7z"] = "Arkiv",
        ["tar"] = "Arkiv",
        ["gz"] = "Arkiv",

        // Installationsfiler
        ["exe"] = "Program",
        ["msi"] = "Program",
        ["dmg"] = "Program",
        ["apk"] = "Program",

        // Kodningsfiler
        ["cs"] = "Kod",
        ["py"] = "Kod",
        ["js"] = "Kod",
        ["ts"] = "Kod",
        ["html"] = "Kod",
        ["css"] = "Kod",
        ["json"] = "Kod",
        ["xml"] = "Kod",
        ["java"] = "Kod",
        ["cpp"] = "Kod",
        ["c"] = "Kod",
    };

   
    public static string GetCategory(string filePath)
    {
        string extension = Path.GetExtension(filePath).TrimStart('.');

        if (string.IsNullOrEmpty(extension))
        {
            return "Övrigt";
        }

        return Categories.TryGetValue(extension, out var category)
            ? category
            : "Övrigt";
    }
}
