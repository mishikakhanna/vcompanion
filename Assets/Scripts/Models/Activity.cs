using UnityEngine;
using System;

/// <summary>
/// Represents a daily activity that the user can complete
/// - Stores activity information
/// - Tracks completion status
/// - Manages points and rewards
/// </summary>
public class Activity
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Points { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime? CompletionTime { get; private set; }

    public Activity(string name, string description, int points)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Description = description;
        Points = points;
        IsCompleted = false;
    }

    public void Complete()
    {
        if (IsCompleted) return;
        
        IsCompleted = true;
        CompletionTime = DateTime.Now;
    }
}