# Filorganiserare 
Ett enkelt .NET-konsolprogram som skannar en mapp och sorterar filerna i
undermappar baserat på filtyp (Bilder, Dokument, Video, Musik, Arkiv, Program, Kod, Övrigt osv).

## Förutsättningar

- [.NET 8 SDK](https://dotnet.microsoft.com/download) installerat.

## Bygga och köra

Öppna en terminal i mappen `FileOrganizer/FileOrganizer` (där `.csproj`-filen ligger) och kör:

```bash
dotnet build
dotnet run
```

Du kan även skicka in mappen som argument direkt:

```bash
dotnet run -- "C:\Users\DittNamn\Downloads"
```

Programmet frågar sedan om du vill köra en **testkörning (dry-run)** först,
vilket visar vad som *skulle* hända utan att faktiskt flytta några filer.
Det rekommenderas att testa så första gången innan du kör på riktigt.

## Hur det fungerar

1. Du väljer en mapp (t.ex. `Hämtade filer` eller `Skrivbord`).
2. Programmet läser alla filer direkt i den mappen (inte undermappar).
3. Varje fil kategoriseras baserat på filändelse, se `FileCategoryMap.cs`.
4. Filen flyttas till en undermapp med kategorins namn, t.ex.:

```
Hämtade filer/
├── Bilder/
│   └── semester.jpg
├── Dokument/
│   └── rapport.pdf
├── Video/
│   └── klipp.mp4
└── Övrigt/
    └── okänd_fil.xyz
```

Om en fil med samma namn redan finns i målmappen läggs `(1)`, `(2)` osv. till
automatiskt så inget skrivs över.

## Anpassa kategorier

Öppna `FileCategoryMap.cs` och lägg till/ändra filändelser i dictionaryn
`Categories` för att styra vilka mappar filerna hamnar i.

## Möjliga vidareutvecklingar

- Grafiskt gränssnitt (WPF eller WinForms) med en "Bläddra..."-knapp för att
  välja mapp, istället för konsol-input.
- Loggfil som sparar historik över alla flyttar (bra om man vill kunna ångra).
- Regler baserat på filens ålder/datum istället för bara filtyp.
- Bevaka en mapp kontinuerligt (FileSystemWatcher) och sortera automatiskt.
