using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages input across different platforms
/// </summary>
public class InputManager : MonoBehaviour
{
    private PlayerControls controls;
    private static InputManager instance;

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("InputManager");
                instance = go.AddComponent<InputManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeControls();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeControls()
    {
        controls = new PlayerControls();
        
        // Setup UI interactions
        controls.UI.Click.performed += ctx => OnClick();
        controls.UI.PetInteraction.performed += ctx => OnPetInteraction(ctx.ReadValue<Vector2>());
        
        controls.Enable();
    }

    private void OnClick()
    {
        // Handle click events
    }

    private void OnPetInteraction(Vector2 position)
    {
        // Handle pet interaction
    }

    private void OnDestroy()
    {
        if (controls != null)
        {
            controls.Disable();
            controls = null;
        }
    }
}