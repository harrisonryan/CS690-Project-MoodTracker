using System;
using System.Globalization;

var storage = new MoodStorage();
var tracker = new MoodTracker(storage);
var analysisService = new MoodAnalysisService();

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
            EditEntry();
            break;
        case "5":
            DeleteEntry();
            break;
        case "6":
            ViewMoodSummary();
            break;
        case "7":
            ViewMoodInsight();
            break;
        case "8":
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
    Console.WriteLine("4. Edit Entry");
    Console.WriteLine("5. Delete Entry");
    Console.WriteLine("6. View Mood Summary");
    Console.WriteLine("7. View Mood Insight");
    Console.WriteLine("8. Exit");
    Console.WriteLine("==================================");
}

void AddNewEntry()
{
    Console.Clear();
    Console.WriteLine("Add New Mood Entry");
    Console.WriteLine("==================");

    MoodEntry entry = BuildEntryFromInput();
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

    List<MoodEntry> entries = tracker.GetAllEntries();

    if (entries.Count == 0)
    {
        Console.WriteLine("No entries found.");
        Pause();
        return;
    }

    foreach (MoodEntry entry in entries)
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

    MoodEntry? entry = GetEntryFromUser();

    if (entry is null)
    {
        Pause();
        return;
    }

    DisplayEntry(entry);
    Pause();
}

void EditEntry()
{
    Console.Clear();
    Console.WriteLine("Edit Entry");
    Console.WriteLine("==========");

    MoodEntry? existingEntry = GetEntryFromUser();

    if (existingEntry is null)
    {
        Pause();
        return;
    }

    Console.WriteLine();
    Console.WriteLine("Enter updated values.");

    MoodEntry updatedEntry = BuildEntryFromInput();
    updatedEntry.Id = existingEntry.Id;

    bool updated = tracker.UpdateEntry(updatedEntry);

    Console.WriteLine(updated
        ? "Entry updated successfully."
        : "Unable to update entry.");

    Pause();
}

void DeleteEntry()
{
    Console.Clear();
    Console.WriteLine("Delete Entry");
    Console.WriteLine("============");

    MoodEntry? entry = GetEntryFromUser();

    if (entry is null)
    {
        Pause();
        return;
    }

    Console.Write($"Are you sure you want to delete entry {entry.Id}? (y/n): ");
    string? confirm = Console.ReadLine();

    if (confirm?.Trim().ToLower() != "y")
    {
        Console.WriteLine("Delete cancelled.");
        Pause();
        return;
    }

    bool deleted = tracker.DeleteEntry(entry.Id);

    Console.WriteLine(deleted
        ? "Entry deleted successfully."
        : "Unable to delete entry.");

    Pause();
}

void ViewMoodSummary()
{
    Console.Clear();
    Console.WriteLine("Mood Summary");
    Console.WriteLine("============");

    List<MoodEntry> entries = tracker.GetAllEntries();
    Console.WriteLine(analysisService.GetSummaryReport(entries));

    Pause();
}

void ViewMoodInsight()
{
    Console.Clear();
    Console.WriteLine("Mood Insight");
    Console.WriteLine("============");

    List<MoodEntry> entries = tracker.GetAllEntries();
    Console.WriteLine(analysisService.GetSleepMoodInsight(entries));

    Pause();
}

MoodEntry BuildEntryFromInput()
{
    DateTime entryDate = ReadDate();
    int moodRating = ReadMoodRating();
    double sleepHours = ReadSleepHours();

    Console.Write("Enter activities for the day: ");
    string activities = Console.ReadLine() ?? "";

    Console.Write("Enter optional notes: ");
    string notes = Console.ReadLine() ?? "";

    return new MoodEntry
    {
        EntryDate = entryDate,
        MoodRating = moodRating,
        SleepHours = sleepHours,
        Activities = activities,
        Notes = notes
    };
}

DateTime ReadDate()
{
    while (true)
    {
        Console.Write("Enter date (yyyy-MM-dd) or press Enter for today: ");
        string? dateInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(dateInput))
        {
            return DateTime.Today;
        }

        if (DateTime.TryParseExact(
            dateInput,
            "yyyy-MM-dd",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateTime entryDate))
        {
            return entryDate;
        }

        Console.WriteLine("Invalid date format.");
    }
}

int ReadMoodRating()
{
    while (true)
    {
        Console.Write("Enter mood rating (1-10): ");
        string? moodInput = Console.ReadLine();

        if (int.TryParse(moodInput, out int moodRating) &&
            moodRating >= 1 &&
            moodRating <= 10)
        {
            return moodRating;
        }

        Console.WriteLine("Mood rating must be a number between 1 and 10.");
    }
}

double ReadSleepHours()
{
    while (true)
    {
        Console.Write("Enter hours of sleep: ");
        string? sleepInput = Console.ReadLine();

        if (double.TryParse(sleepInput, out double sleepHours) && sleepHours >= 0)
        {
            return sleepHours;
        }

        Console.WriteLine("Please enter a valid number of hours.");
    }
}

MoodEntry? GetEntryFromUser()
{
    List<MoodEntry> entries = tracker.GetAllEntries();

    if (entries.Count == 0)
    {
        Console.WriteLine("No entries available.");
        return null;
    }

    foreach (MoodEntry entry in entries)
    {
        Console.WriteLine($"ID: {entry.Id} | Date: {entry.EntryDate:yyyy-MM-dd}");
    }

    Console.WriteLine();
    Console.Write("Enter entry ID: ");
    string? input = Console.ReadLine();

    if (!int.TryParse(input, out int entryId))
    {
        Console.WriteLine("Invalid ID.");
        return null;
    }

    MoodEntry? selectedEntry = tracker.GetEntryById(entryId);

    if (selectedEntry is null)
    {
        Console.WriteLine("Entry not found.");
        return null;
    }

    return selectedEntry;
}

void DisplayEntry(MoodEntry entry)
{
    Console.WriteLine();
    Console.WriteLine($"ID: {entry.Id}");
    Console.WriteLine($"Date: {entry.EntryDate:yyyy-MM-dd}");
    Console.WriteLine($"Mood Rating: {entry.MoodRating}/10");
    Console.WriteLine($"Sleep Hours: {entry.SleepHours}");
    Console.WriteLine($"Activities: {entry.Activities}");
    Console.WriteLine($"Notes: {entry.Notes}");
}

void Pause()
{
    Console.WriteLine();
    Console.WriteLine("Press Enter to continue...");
    Console.ReadLine();
}