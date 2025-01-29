using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Manages daily activities and achievements
/// - Generates daily activities
/// - Tracks activity completion
/// - Manages rewards and points
/// - Handles activity notifications
/// </summary>
public class ActivityManager : MonoBehaviour
{
    [SerializeField] private ActivityUI activityUI;
    
    private List<Activity> dailyActivities = new List<Activity>();
    private int totalPoints = 0;
    private int streakDays = 0;

    private void Start()
    {
        GenerateDailyActivities();
    }

    private void GenerateDailyActivities()
    {
        dailyActivities.Clear();
        
        // Add sample activities
        dailyActivities.Add(new Activity("Quick Dance", "Dance to your favorite song", 10));
        dailyActivities.Add(new Activity("Stretch Break", "Do some simple stretches", 5));
        dailyActivities.Add(new Activity("Kind Message", "Send a kind message to someone", 8));
        
        activityUI.UpdateActivityList(dailyActivities);
    }

    public void CompleteActivity(string activityId)
    {
        Activity activity = dailyActivities.Find(a => a.Id == activityId);
        if (activity == null || activity.IsCompleted) return;

        activity.Complete();
        totalPoints += activity.Points;
        UpdateStreak();
        
        activityUI.UpdateActivityList(dailyActivities);
        CheckRewards();
    }

    private void UpdateStreak()
    {
        if (PlayerPrefs.HasKey("LastActivityDate"))
        {
            DateTime lastActivity = DateTime.Parse(PlayerPrefs.GetString("LastActivityDate"));
            if (DateTime.Now.Date == lastActivity.Date.AddDays(1))
            {
                streakDays++;
            }
            else if (DateTime.Now.Date != lastActivity.Date)
            {
                streakDays = 1;
            }
        }
        else
        {
            streakDays = 1;
        }

        PlayerPrefs.SetString("LastActivityDate", DateTime.Now.ToString());
        PlayerPrefs.SetInt("StreakDays", streakDays);
    }

    private void CheckRewards()
    {
        // Implement reward checking logic
    }
}