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

    private OpenAIApi openai;
    private string openAIKey;
    private List<ChatMessage> messages = new List<ChatMessage>();

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
        openAIKey = JsonReader.Instance.openAIKeyValue;
        openai = new OpenAIApi(openAIKey);
        Debug.Log(openAIKey);
        messages.Add(new ChatMessage() {
            Role = "system",
			Content = "You are a Ready Player Me avatar, who exists in the metaverse.",
        });
    }
}
