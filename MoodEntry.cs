using System;

public class MoodEntry
{
    public int Id { get; set; }
    public DateTime EntryDate { get; set; }
    public int MoodRating { get; set; }
    public double SleepHours { get; set; }
    public string Activities { get; set; } = "";
    public string Notes { get; set; } = "";
}