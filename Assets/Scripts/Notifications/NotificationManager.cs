using UnityEngine;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// Manages in-game notifications
/// - Handles notification display
/// - Manages notification queue
/// - Controls notification animations
/// - Handles notification interactions
/// </summary>
public class NotificationManager : MonoBehaviour
{
    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private Transform notificationContainer;
    [SerializeField] private float notificationDuration = 3f;
    
    private Queue<GameObject> activeNotifications = new Queue<GameObject>();
    private const int MAX_NOTIFICATIONS = 3;

    public void ShowRewardNotification(Reward reward)
    {
        string message = $"New Reward Unlocked: {reward.Name}!";
        ShowNotification(message, NotificationType.Reward);
    }

    public void ShowActivityNotification(string message)
    {
        ShowNotification(message, NotificationType.Activity);
    }

    public void ShowPetNotification(string message)
    {
        ShowNotification(message, NotificationType.Pet);
    }

    private void ShowNotification(string message, NotificationType type)
    {
        // Remove old notifications if we're at max
        while (activeNotifications.Count >= MAX_NOTIFICATIONS)
        {
            var oldNotification = activeNotifications.Dequeue();
            Destroy(oldNotification);
        }

        // Create new notification
        GameObject notification = Instantiate(notificationPrefab, notificationContainer);
        notification.GetComponentInChildren<TMP_Text>().text = message;
        
        // Style based on type
        StyleNotification(notification, type);
        
        // Add to active notifications
        activeNotifications.Enqueue(notification);
        
        // Schedule removal
        Destroy(notification, notificationDuration);
    }

    private void StyleNotification(GameObject notification, NotificationType type)
    {
        var image = notification.GetComponent<UnityEngine.UI.Image>();
        switch (type)
        {
            case NotificationType.Reward:
                image.color = new Color(1f, 0.92f, 0.016f, 0.9f); // Gold
                break;
            case NotificationType.Activity:
                image.color = new Color(0f, 0.7f, 1f, 0.9f); // Blue
                break;
            case NotificationType.Pet:
                image.color = new Color(1f, 0.5f, 0.7f, 0.9f); // Pink
                break;
        }
    }
}

public enum NotificationType
{
    Reward,
    Activity,
    Pet
}