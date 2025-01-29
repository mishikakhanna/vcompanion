using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages pet customization features
/// - Handles accessory management
/// - Controls background changes
/// - Manages color themes
/// - Saves customization preferences
/// </summary>
public class CustomizationManager : MonoBehaviour
{
    [SerializeField] private PetController petController;
    [SerializeField] private SpriteRenderer backgroundRenderer;
    
    private Dictionary<string, Sprite> accessories = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> backgrounds = new Dictionary<string, Sprite>();
    private string currentBackground;
    private List<string> activeAccessories = new List<string>();

    private void Start()
    {
        LoadCustomizations();
    }

    private void LoadCustomizations()
    {
        // Load saved customizations
        currentBackground = PlayerPrefs.GetString("CurrentBackground", "default");
        string savedAccessories = PlayerPrefs.GetString("ActiveAccessories", "");
        if (!string.IsNullOrEmpty(savedAccessories))
        {
            activeAccessories = new List<string>(savedAccessories.Split(','));
        }

        ApplyCustomizations();
    }

    private void ApplyCustomizations()
    {
        // Apply background
        if (backgrounds.ContainsKey(currentBackground))
        {
            backgroundRenderer.sprite = backgrounds[currentBackground];
        }

        // Apply accessories
        foreach (string accessoryId in activeAccessories)
        {
            if (accessories.ContainsKey(accessoryId))
            {
                ApplyAccessory(accessoryId);
            }
        }
    }

    public void UnlockAccessory(string accessoryId)
    {
        PlayerPrefs.SetInt($"accessory_{accessoryId}", 1);
    }

    public void UnlockBackground(string backgroundId)
    {
        PlayerPrefs.SetInt($"background_{backgroundId}", 1);
    }

    public void ToggleAccessory(string accessoryId)
    {
        if (!accessories.ContainsKey(accessoryId)) return;

        if (activeAccessories.Contains(accessoryId))
        {
            activeAccessories.Remove(accessoryId);
            RemoveAccessory(accessoryId);
        }
        else
        {
            activeAccessories.Add(accessoryId);
            ApplyAccessory(accessoryId);
        }

        SaveCustomizations();
    }

    public void SetBackground(string backgroundId)
    {
        if (!backgrounds.ContainsKey(backgroundId)) return;

        currentBackground = backgroundId;
        backgroundRenderer.sprite = backgrounds[backgroundId];
        SaveCustomizations();
    }

    private void ApplyAccessory(string accessoryId)
    {
        // Apply accessory to pet
    }

    private void RemoveAccessory(string accessoryId)
    {
        // Remove accessory from pet
    }

    private void SaveCustomizations()
    {
        PlayerPrefs.SetString("CurrentBackground", currentBackground);
        PlayerPrefs.SetString("ActiveAccessories", string.Join(",", activeAccessories));
    }
}