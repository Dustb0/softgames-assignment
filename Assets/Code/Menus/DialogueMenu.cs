using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class DialogueMenu : BaseSubMenu
{
    [Inject]
    private IDialogueLoader loader;

    private Label statusLabel;
    private VisualElement dialogueContainer;

    private DialogueResponse dialogueData;
    private Dictionary<string, Texture2D> avatarData;

    protected override void Start()
    {
        base.Start();
        statusLabel = uiDocument.rootVisualElement.Q<Label>("StatusLabel");
        dialogueContainer = uiDocument.rootVisualElement.Q<VisualElement>("DialogueContainer");
        dialogueContainer.visible = false;

        StartCoroutine(loader.LoadDialogue(OnDialogueLoaded, OnDialogueError));
    }

    private void OnDialogueError(string message)
    {
        statusLabel.text = message;
    }

    private string ProcessInlineEmojis(string input)
    {
        return input.Replace("{satisfied}", "😊")
                    .Replace("{intrigued}", "👀")
                    .Replace("{neutral}", "😐")
                    .Replace("{affirmative}", "✅")
                    .Replace("{laughing}", "🤣")
                    .Replace("{win}", "🏆");
    }

    private void OnDialogueLoaded(DialogueResponse response)
    {
        dialogueData = response;
        StartCoroutine(loader.LoadAvatars(dialogueData.avatars.ToList(), OnAvatarsLoaded));
    }

    private void OnAvatarsLoaded(Dictionary<string, Texture2D> avatarTextures)
    {
        statusLabel.visible = false;
        avatarData = avatarTextures;
        BuildDialogueTree();
    }

    private void BuildDialogueTree()
    {
        // Build dialogue structure
        dialogueContainer.Clear();
        dialogueContainer.visible = true;

        foreach (var entry in dialogueData.dialogue)
        {
            var dialogueElement = new VisualElement();
            dialogueElement.AddToClassList("Dialogue");

            var dialogueText = new Label();
            dialogueText.text = $"{entry.name}: {ProcessInlineEmojis(entry.text)}";

            var avatarImage = new VisualElement();
            avatarImage.AddToClassList("Avatar");
            if (avatarData.ContainsKey(entry.name))
            {
                avatarImage.style.backgroundImage = new StyleBackground(avatarData[entry.name]);
            }
            else
            {
                avatarImage.style.backgroundColor = Color.gray;
            }


            dialogueElement.Add(avatarImage);
            dialogueElement.Add(dialogueText);
            dialogueContainer.Add(dialogueElement);
        }
    }
}
