using UnityEngine;
using System;

/// <summary>
/// Controls the virtual pet's behavior, animations, and state
/// - Manages pet animations and transitions
/// - Handles pet emotions and reactions
/// - Controls pet movement and interactions
/// - Manages pet state (happy, thinking, sleeping, etc.)
/// </summary>
public class PetController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer petSprite;
    
    private string currentState = "Idle";
    private string currentEmotion = "Happy";
    
    private void Start()
    {
        InitializePet();
    }

    private void InitializePet()
    {
        UpdateAnimation("Idle");
        UpdateEmotion("Happy");
    }

    public void UpdateAnimation(string newState)
    {
        if (currentState == newState) return;
        
        animator.Play(newState);
        currentState = newState;
    }

    public void UpdateEmotion(string emotion)
    {
        currentEmotion = emotion;
        // Trigger emotion-specific animations or effects
    }

    public string GetCurrentEmotion()
    {
        return currentEmotion;
    }
}