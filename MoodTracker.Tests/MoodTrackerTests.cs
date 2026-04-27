using System;
using System.IO;
using Xunit;

public class MoodTrackerTests
{
    [Fact]
    public void AddEntry_AddsEntryToTracker()
    {
        string testFile = $"tracker_test_{Guid.NewGuid()}.json";
        var storage = new MoodStorage(testFile);
        var tracker = new MoodTracker(storage);

        tracker.AddEntry(new MoodEntry
        {
            EntryDate = DateTime.Today,
            MoodRating = 9,
            SleepHours = 8,
            Activities = "Gym",
            Notes = "Great day"
        });

        var entries = tracker.GetAllEntries();

        Assert.Single(entries);
        Assert.Equal(9, entries[0].MoodRating);

        File.Delete(testFile);
    }

    [Fact]
    public void UpdateEntry_UpdatesExistingEntry()
    {
        string testFile = $"tracker_test_{Guid.NewGuid()}.json";
        var storage = new MoodStorage(testFile);
        var tracker = new MoodTracker(storage);

        tracker.AddEntry(new MoodEntry
        {
            EntryDate = DateTime.Today,
            MoodRating = 5,
            SleepHours = 6,
            Activities = "Work",
            Notes = "Okay day"
        });

        var entry = tracker.GetAllEntries()[0];

        bool updated = tracker.UpdateEntry(new MoodEntry
        {
            Id = entry.Id,
            EntryDate = DateTime.Today,
            MoodRating = 10,
            SleepHours = 8,
            Activities = "Workout",
            Notes = "Updated"
        });

        Assert.True(updated);
        Assert.Equal(10, tracker.GetEntryById(entry.Id)?.MoodRating);

        File.Delete(testFile);
    }

    [Fact]
    public void DeleteEntry_RemovesExistingEntry()
    {
        string testFile = $"tracker_test_{Guid.NewGuid()}.json";
        var storage = new MoodStorage(testFile);
        var tracker = new MoodTracker(storage);

        tracker.AddEntry(new MoodEntry
        {
            EntryDate = DateTime.Today,
            MoodRating = 7,
            SleepHours = 7,
            Activities = "Reading",
            Notes = "Normal day"
        });

        int id = tracker.GetAllEntries()[0].Id;

        bool deleted = tracker.DeleteEntry(id);

        Assert.True(deleted);
        Assert.Empty(tracker.GetAllEntries());

        File.Delete(testFile);
    }
}