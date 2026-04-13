using System;
using System.Globalization;

var tracker = new MoodTracker();
bool running = true;

while (running)
{
    Console.Clear();
    ShowMenu();

    Console.Write("Select an option: ");
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            AddNewEntry();
            break;
        case "2":
            ViewAllEntries();
            break;
        case "3":
            ViewEntryDetails();
            break;
        case "4":
            running = false;
            Console.WriteLine("Goodbye!");
            break;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            Pause();
            break;
    }
}

void ShowMenu()
{
    Console.WriteLine("==================================");
    Console.WriteLine("         Mood Tracker App         ");
    Console.WriteLine("==================================");
    Console.WriteLine("1. Add New Entry");
    Console.WriteLine("2. View All Entries");
    Console.WriteLine("3. View Entry Details");
    Console.WriteLine("4. Exit");
    Console.WriteLine("==================================");
}

void AddNewEntry()
{
    Console.Clear();
    Console.WriteLine("Add New Mood Entry");
    Console.WriteLine("==================");

    DateTime entryDate;
    while (true)
    {
        Console.Write("Enter date (yyyy-MM-dd) or press Enter for today: ");
        string? dateInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(dateInput))
        {
            entryDate = DateTime.Today;
            break;
        }

        if (DateTime.TryParseExact(dateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out entryDate))
        {
            break;
        }

        Console.WriteLine("Invalid date format.");
    }

    int moodRating;
    while (true)
    {
        Console.Write("Enter mood rating (1-10): ");
        string? moodInput = Console.ReadLine();

        if (int.TryParse(moodInput, out moodRating) && moodRating >= 1 && moodRating <= 10)
        {
            break;
        }

        Console.WriteLine("Mood rating must be a number between 1 and 10.");
    }

    double sleepHours;
    while (true)
    {
        Console.Write("Enter hours of sleep: ");
        string? sleepInput = Console.ReadLine();

        if (double.TryParse(sleepInput, out sleepHours) && sleepHours >= 0)
        {
            break;
        }

        Console.WriteLine("Please enter a valid number of hours.");
    }

    Console.Write("Enter activities for the day: ");
    string activities = Console.ReadLine() ?? "";

    Console.Write("Enter optional notes: ");
    string notes = Console.ReadLine() ?? "";

    var entry = new MoodEntry
    {
        EntryDate = entryDate,
        MoodRating = moodRating,
        SleepHours = sleepHours,
        Activities = activities,
        Notes = notes
    };

    tracker.AddEntry(entry);

    Console.WriteLine();
    Console.WriteLine("Mood entry saved successfully.");
    Pause();
}

void ViewAllEntries()
{
    Console.Clear();
    Console.WriteLine("All Mood Entries");
    Console.WriteLine("================");

    var entries = tracker.GetAllEntries();

    if (entries.Count == 0)
    {
        Console.WriteLine("No entries found.");
        Pause();
        return;
    }

    foreach (var entry in entries)
    {
        Console.WriteLine($"ID: {entry.Id} | Date: {entry.EntryDate:yyyy-MM-dd} | Mood: {entry.MoodRating}/10 | Sleep: {entry.SleepHours} hrs");
    }

    Pause();
}

void ViewEntryDetails()
{
    Console.Clear();
    Console.WriteLine("View Entry Details");
    Console.WriteLine("==================");

    var entries = tracker.GetAllEntries();

    if (entries.Count == 0)
    {
        Console.WriteLine("No entries available.");
        Pause();
        return;
    }

    foreach (var entry in entries)
    {
        Console.WriteLine($"ID: {entry.Id} | Date: {entry.EntryDate:yyyy-MM-dd}");
    }

    Console.WriteLine();
    Console.Write("Enter entry ID: ");
    string? input = Console.ReadLine();

    if (!int.TryParse(input, out int entryId))
    {
        Console.WriteLine("Invalid ID.");
        Pause();
        return;
    }

    MoodEntry? selectedEntry = tracker.GetEntryById(entryId);

    if (selectedEntry is null)
    {
        Console.WriteLine("Entry not found.");
        Pause();
        return;
    }

    Console.WriteLine();
    Console.WriteLine($"ID: {selectedEntry.Id}");
    Console.WriteLine($"Date: {selectedEntry.EntryDate:yyyy-MM-dd}");
    Console.WriteLine($"Mood Rating: {selectedEntry.MoodRating}/10");
    Console.WriteLine($"Sleep Hours: {selectedEntry.SleepHours}");
    Console.WriteLine($"Activities: {selectedEntry.Activities}");
    Console.WriteLine($"Notes: {selectedEntry.Notes}");

    Pause();
}

void Pause()
{
    Console.WriteLine();
    Console.WriteLine("Press Enter to continue...");
    Console.ReadLine();
}