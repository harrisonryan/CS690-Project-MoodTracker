using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class MoodTracker
{
    private const string FilePath = "mood_entries.json";
    private List<MoodEntry> _entries = new();

    public MoodTracker()
    {
        LoadEntries();
    }

    public void AddEntry(MoodEntry entry)
    {
        entry.Id = _entries.Count == 0 ? 1 : _entries.Max(e => e.Id) + 1;
        _entries.Add(entry);
        SaveEntries();
    }

    public List<MoodEntry> GetAllEntries()
    {
        return _entries.OrderBy(e => e.EntryDate).ToList();
    }

    public MoodEntry? GetEntryById(int id)
    {
        return _entries.FirstOrDefault(e => e.Id == id);
    }

    private void LoadEntries()
    {
        try
        {
            if (!File.Exists(FilePath))
            {
                _entries = new List<MoodEntry>();
                return;
            }

            string json = File.ReadAllText(FilePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                _entries = new List<MoodEntry>();
                return;
            }

            _entries = JsonSerializer.Deserialize<List<MoodEntry>>(json) ?? new List<MoodEntry>();
        }
        catch
        {
            _entries = new List<MoodEntry>();
        }
    }

    private void SaveEntries()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(_entries, options);
        File.WriteAllText(FilePath, json);
    }
}