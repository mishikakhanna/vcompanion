using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

/// <summary>
/// Manages the chat interface and conversation system
/// - Handles message display and history
/// - Manages user input and responses
/// - Controls chat UI elements
/// - Integrates with LLM service
/// </summary>
public class ChatManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField messageInput;
    [SerializeField] private Button sendButton;
    [SerializeField] private Transform messageContainer;
    [SerializeField] private GameObject userMessagePrefab;
    [SerializeField] private GameObject petMessagePrefab;
    
    private LLMService llmService;
    private List<ChatMessage> messageHistory = new List<ChatMessage>();

    private void Start()
    {
        llmService = new LLMService();
        InitializeChat();
    }

    private void InitializeChat()
    {
        sendButton.onClick.AddListener(OnSendMessage);
        AddPetMessage("Hi! I'm your friendly pet companion. How are you feeling today?");
    }

    public async void OnSendMessage()
    {
        if (string.IsNullOrEmpty(messageInput.text)) return;

        string userMessage = messageInput.text;
        AddUserMessage(userMessage);
        messageInput.text = "";

        // Generate pet response
        string response = await llmService.GenerateResponse(userMessage);
        AddPetMessage(response);
    }

    private void AddUserMessage(string message)
    {
        GameObject messageObj = Instantiate(userMessagePrefab, messageContainer);
        messageObj.GetComponentInChildren<TMP_Text>().text = message;
        messageHistory.Add(new ChatMessage(message, true));
    }

    private void AddPetMessage(string message)
    {
        GameObject messageObj = Instantiate(petMessagePrefab, messageContainer);
        messageObj.GetComponentInChildren<TMP_Text>().text = message;
        messageHistory.Add(new ChatMessage(message, false));
    }
}