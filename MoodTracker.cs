using System.Collections.Generic;
using System.Linq;

public class MoodTracker
{
    private readonly MoodStorage _storage;
    private List<MoodEntry> _entries;

    public MoodTracker(MoodStorage storage)
    {
        _storage = storage;
        _entries = _storage.LoadEntries();
    }

    public void AddEntry(MoodEntry entry)
    {
        entry.Id = _entries.Count == 0 ? 1 : _entries.Max(e => e.Id) + 1;
        _entries.Add(entry);
        Save();
    }

    public List<MoodEntry> GetAllEntries()
    {
        return _entries
            .OrderBy(e => e.EntryDate)
            .ToList();
    }

    public MoodEntry? GetEntryById(int id)
    {
        return _entries.FirstOrDefault(e => e.Id == id);
    }

    public bool UpdateEntry(MoodEntry updatedEntry)
    {
        MoodEntry? existingEntry = GetEntryById(updatedEntry.Id);

        if (existingEntry is null)
        {
            return false;
        }

        existingEntry.EntryDate = updatedEntry.EntryDate;
        existingEntry.MoodRating = updatedEntry.MoodRating;
        existingEntry.SleepHours = updatedEntry.SleepHours;
        existingEntry.Activities = updatedEntry.Activities;
        existingEntry.Notes = updatedEntry.Notes;

        Save();
        return true;
    }

    public bool DeleteEntry(int id)
    {
        MoodEntry? entry = GetEntryById(id);

        if (entry is null)
        {
            return false;
        }

        _entries.Remove(entry);
        Save();
        return true;
    }

    private void Save()
    {
        _storage.SaveEntries(_entries);
    }
}