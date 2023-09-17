using System;
using System.Collections;
using System.Collections.Generic;
using OpenAI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConversationManager : MonoBehaviour
{
    [SerializeField] private Button sendButton;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text conversationText;

    private OpenAIApi openai = new OpenAIApi("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");
    private List<ChatMessage> messages = new List<ChatMessage>();

    void Awake() {
        messages.Add(new ChatMessage() {
            Role = "system",
			Content = "You are a Ready Player Me avatar, who exists in the metaverse.",
        });
    }

    public async void OnButtonClicked() {
        var message = inputField.text;
        conversationText.text = $"You: {message}{Environment.NewLine}";
        inputField.text = string.Empty;

        messages.Add(new ChatMessage()
		{
			Role = "user",
			Content = message,
		});

		var resp = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
		{
			Messages = messages,
			Model = "gpt-3.5-turbo",
			MaxTokens = 32,
		});

		var reply = resp.Choices[0].Message.Content;

		messages.Add(new ChatMessage()
		{
			Role = "assistant",
			Content = reply,
		});

    conversationText.text += $"AI: {reply}{Environment.NewLine}";
    sendButton.interactable = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
