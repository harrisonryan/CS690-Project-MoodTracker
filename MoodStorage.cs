using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class MoodStorage
{
    private readonly string _filePath;

    public MoodStorage(string filePath = "mood_entries.json")
    {
        _filePath = filePath;
    }

    public List<MoodEntry> LoadEntries()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                return new List<MoodEntry>();
            }

            string json = File.ReadAllText(_filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<MoodEntry>();
            }

            return JsonSerializer.Deserialize<List<MoodEntry>>(json) ?? new List<MoodEntry>();
        }
        catch
        {
            return new List<MoodEntry>();
        }
    }

    public void SaveEntries(List<MoodEntry> entries)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(entries, options);
        File.WriteAllText(_filePath, json);
    }
}