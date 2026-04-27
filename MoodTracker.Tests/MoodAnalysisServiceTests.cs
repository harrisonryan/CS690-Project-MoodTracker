using System.Collections.Generic;
using Xunit;

public class MoodAnalysisServiceTests
{
    [Fact]
    public void GetAverageMood_ReturnsCorrectAverage()
    {
        var service = new MoodAnalysisService();

        var entries = new List<MoodEntry>
        {
            new MoodEntry { MoodRating = 10, SleepHours = 8 },
            new MoodEntry { MoodRating = 6, SleepHours = 5 }
        };

        double result = service.GetAverageMood(entries);

        Assert.Equal(8, result);
    }

    [Fact]
    public void GetSleepMoodInsight_ReturnsComparisonMessage()
    {
        var service = new MoodAnalysisService();

        var entries = new List<MoodEntry>
        {
            new MoodEntry { MoodRating = 10, SleepHours = 8 },
            new MoodEntry { MoodRating = 3, SleepHours = 4 }
        };

        string result = service.GetSleepMoodInsight(entries);

        Assert.Contains("7 or more hours", result);
    }
}