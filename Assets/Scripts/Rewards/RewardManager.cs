using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages the reward system
/// - Tracks available and unlocked rewards
/// - Handles reward unlocking conditions
/// - Manages reward notifications
/// - Controls reward animations
/// </summary>
public class RewardManager : MonoBehaviour
{
    [SerializeField] private NotificationManager notificationManager;
    [SerializeField] private CustomizationManager customizationManager;
    
    private List<Reward> rewards = new List<Reward>();

    private void Start()
    {
        InitializeRewards();
    }

    private void InitializeRewards()
    {
        rewards.Add(new Reward("dance_animation", "Happy Dance", "New dance animation", 50, RewardType.Animation));
        rewards.Add(new Reward("party_hat", "Party Hat", "A cute party hat accessory", 100, RewardType.Accessory));
        rewards.Add(new Reward("park_background", "Park Background", "A beautiful park setting", 200, RewardType.Background));
        
        LoadUnlockedRewards();
    }

    private void LoadUnlockedRewards()
    {
        foreach (var reward in rewards)
        {
            string key = $"reward_{reward.Id}";
            if (PlayerPrefs.HasKey(key))
            {
                reward.Unlock();
            }
        }
    }

    public void CheckRewards(int points)
    {
        foreach (var reward in rewards)
        {
            if (!reward.IsUnlocked && points >= reward.PointsRequired)
            {
                UnlockReward(reward);
            }
        }
    }

    private void UnlockReward(Reward reward)
    {
        reward.Unlock();
        PlayerPrefs.SetInt($"reward_{reward.Id}", 1);
        
        // Apply reward
        switch (reward.Type)
        {
            case RewardType.Animation:
                // Unlock new animation
                break;
            case RewardType.Accessory:
                customizationManager.UnlockAccessory(reward.Id);
                break;
            case RewardType.Background:
                customizationManager.UnlockBackground(reward.Id);
                break;
        }

        // Show notification
        notificationManager.ShowRewardNotification(reward);
    }

    public List<Reward> GetUnlockedRewards()
    {
        return rewards.FindAll(r => r.IsUnlocked);
    }
}