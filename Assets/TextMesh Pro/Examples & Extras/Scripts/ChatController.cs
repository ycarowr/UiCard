using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatController : MonoBehaviour
{
    public Scrollbar ChatScrollbar;


    public TMP_InputField TMP_ChatInput;

    public TMP_Text TMP_ChatOutput;

    private void OnEnable()
    {
        TMP_ChatInput.onSubmit.AddListener(AddToChatOutput);
    }

    private void OnDisable()
    {
        TMP_ChatInput.onSubmit.RemoveListener(AddToChatOutput);
    }


    private void AddToChatOutput(string newText)
    {
        // Clear Input Field
        TMP_ChatInput.text = string.Empty;

        var timeNow = DateTime.Now;

        TMP_ChatOutput.text += "[<#FFFF80>" + timeNow.Hour.ToString("d2") + ":" + timeNow.Minute.ToString("d2") + ":" +
                               timeNow.Second.ToString("d2") + "</color>] " + newText + "\n";

        TMP_ChatInput.ActivateInputField();

        // Set the scrollbar to the bottom when next text is submitted.
        ChatScrollbar.value = 0;
    }
}