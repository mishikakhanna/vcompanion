using System;

/// <summary>
/// Represents a chat message in the conversation
/// - Stores message content and metadata
/// - Tracks message ownership
/// - Manages timestamps
/// </summary>
public class ChatMessage
{
    public string Content { get; private set; }
    public bool IsUser { get; private set; }
    public DateTime Timestamp { get; private set; }

    public ChatMessage(string content, bool isUser)
    {
        Content = content;
        IsUser = isUser;
        Timestamp = DateTime.Now;
    }
}