using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

public class MoodStorageTests
{
    [Fact]
    public void SaveEntries_And_LoadEntries_ReturnsSavedEntries()
    {
        string testFile = $"test_entries_{Guid.NewGuid()}.json";
        var storage = new MoodStorage(testFile);

        var entries = new List<MoodEntry>
        {
            new MoodEntry
            {
                Id = 1,
                EntryDate = DateTime.Today,
                MoodRating = 8,
                SleepHours = 7,
                Activities = "Walking",
                Notes = "Good day"
            }
        };

        storage.SaveEntries(entries);
        var loadedEntries = storage.LoadEntries();

        Assert.Single(loadedEntries);
        Assert.Equal(8, loadedEntries[0].MoodRating);

        File.Delete(testFile);
    }

    [Fact]
    public void LoadEntries_WhenFileDoesNotExist_ReturnsEmptyList()
    {
        var storage = new MoodStorage("missing_test_file.json");

        var entries = storage.LoadEntries();

        Assert.Empty(entries);
    }
}