using UnityEngine;

/// <summary>
/// Represents a reward that can be unlocked
/// - Stores reward information
/// - Tracks unlock status
/// - Manages reward types
/// </summary>
public class Reward
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int PointsRequired { get; private set; }
    public RewardType Type { get; private set; }
    public bool IsUnlocked { get; private set; }

    public Reward(string id, string name, string description, int pointsRequired, RewardType type)
    {
        Id = id;
        Name = name;
        Description = description;
        PointsRequired = pointsRequired;
        Type = type;
        IsUnlocked = false;
    }

    public void Unlock()
    {
        IsUnlocked = true;
    }
}

public enum RewardType
{
    Animation,
    Accessory,
    Background,
    Achievement
}