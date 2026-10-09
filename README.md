# File Organizer

A simple .NET console application that scans a folder and automatically sorts files into subfolders based on file type (Images, Documents, Videos, Music, Archives, Programs, Code, Other, etc.).

## Prerequisites

- https://dotnet.microsoft.com/download installed.

## Build and Run

Open a terminal in the `FileOrganizer/FileOrganizer` directory (where the `FileOrganizer.csproj` file is located) and run:

```bash
dotnet build
dotnet run
```

You can also provide a folder path directly as an argument:

```bash
dotnet run -- "C:\Users\YourName\Downloads"
```

The application will then ask whether you want to perform a **dry run** first. A dry run shows what *would* happen without actually moving any files.

It is recommended to test with a dry run before running the program for real.

## How It Works

1. Select a folder (for example, `Downloads` or `Desktop`).
2. The application reads all files directly inside that folder (excluding subfolders).
3. Each file is categorized based on its file extension. See `FileCategoryMap.cs` for the mapping.
4. The file is moved to a subfolder matching its category, for example:

```text
Downloads/
├── Images/
│   └── vacation.jpg
├── Documents/
│   └── report.pdf
├── Videos/
│   └── clip.mp4
└── Other/
    └── unknown_file.xyz
```

If a file with the same name already exists in the destination folder, `(1)`, `(2)`, and so on will automatically be appended to the filename to prevent overwriting.

## Customizing Categories

Open `FileCategoryMap.cs` and modify the `Categories` dictionary to add, remove, or change file extensions and control how files are categorized.

## Possible Future Improvements

- Add a graphical user interface (WPF or WinForms) with a **Browse...** button for folder selection instead of console input.
- Create a log file that stores the history of file movements, making it easier to review or undo changes.
- Add rules based on file age or modification date rather than only file type.
- Continuously monitor a folder using `FileSystemWatcher` and automatically organize new files in real time.

## Technologies Used

- C#
- .NET 8
- Console Application
- File and Directory Management (`System.IO`)

## License

This project is open source and available under the MIT License.