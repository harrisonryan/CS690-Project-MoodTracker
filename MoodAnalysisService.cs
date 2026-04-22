using System;
using System.Collections.Generic;
using System.Linq;

public class MoodAnalysisService
{
    public double GetAverageMood(List<MoodEntry> entries)
    {
        if (entries.Count == 0)
        {
            return 0;
        }

        return Math.Round(entries.Average(e => e.MoodRating), 2);
    }

    public double GetAverageSleep(List<MoodEntry> entries)
    {
        if (entries.Count == 0)
        {
            return 0;
        }

        return Math.Round(entries.Average(e => e.SleepHours), 2);
    }

    public MoodEntry? GetHighestMoodEntry(List<MoodEntry> entries)
    {
        if (entries.Count == 0)
        {
            return null;
        }

        int highestMood = entries.Max(e => e.MoodRating);
        return entries.FirstOrDefault(e => e.MoodRating == highestMood);
    }

    public MoodEntry? GetLowestMoodEntry(List<MoodEntry> entries)
    {
        if (entries.Count == 0)
        {
            return null;
        }

        int lowestMood = entries.Min(e => e.MoodRating);
        return entries.FirstOrDefault(e => e.MoodRating == lowestMood);
    }

    public string GetSleepMoodInsight(List<MoodEntry> entries)
    {
        if (entries.Count == 0)
        {
            return "No entries available for analysis.";
        }

        List<MoodEntry> highSleepEntries = entries
            .Where(e => e.SleepHours >= 7)
            .ToList();

        List<MoodEntry> lowSleepEntries = entries
            .Where(e => e.SleepHours < 7)
            .ToList();

        if (highSleepEntries.Count == 0 || lowSleepEntries.Count == 0)
        {
            return "More sleep data is needed to compare mood patterns.";
        }

        double highSleepMood = highSleepEntries.Average(e => e.MoodRating);
        double lowSleepMood = lowSleepEntries.Average(e => e.MoodRating);

        if (highSleepMood > lowSleepMood)
        {
            return $"Your average mood is higher on days with 7 or more hours of sleep ({highSleepMood:F2}) than on days with less than 7 hours ({lowSleepMood:F2}).";
        }

        if (lowSleepMood > highSleepMood)
        {
            return $"Your average mood is higher on days with less than 7 hours of sleep ({lowSleepMood:F2}) than on days with 7 or more hours ({highSleepMood:F2}).";
        }

        return $"Your average mood is the same for both sleep groups ({highSleepMood:F2}).";
    }

    public string GetSummaryReport(List<MoodEntry> entries)
    {
        if (entries.Count == 0)
        {
            return "No entries available to summarize.";
        }

        double averageMood = GetAverageMood(entries);
        double averageSleep = GetAverageSleep(entries);
        MoodEntry? highest = GetHighestMoodEntry(entries);
        MoodEntry? lowest = GetLowestMoodEntry(entries);

        string highestText = highest is null
            ? "N/A"
            : $"{highest.EntryDate:yyyy-MM-dd} with mood {highest.MoodRating}/10";

        string lowestText = lowest is null
            ? "N/A"
            : $"{lowest.EntryDate:yyyy-MM-dd} with mood {lowest.MoodRating}/10";

        return
            $"Total Entries: {entries.Count}\n" +
            $"Average Mood: {averageMood:F2}/10\n" +
            $"Average Sleep: {averageSleep:F2} hours\n" +
            $"Highest Mood Day: {highestText}\n" +
            $"Lowest Mood Day: {lowestText}";
    }
}