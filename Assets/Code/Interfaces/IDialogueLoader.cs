using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueEntry
{
    public string name;
    public string text;
}

[Serializable]
public class AvatarEntry
{
    public string name;
    public string url;
    public string position;
}

[Serializable]
public class DialogueResponse
{
    public DialogueEntry[] dialogue;
    public AvatarEntry[] avatars;
}

public interface IDialogueLoader 
{
    IEnumerator LoadDialogue(Action<DialogueResponse> onSuccess, Action<string> onError);

    IEnumerator LoadAvatars(List<AvatarEntry> avatarEntries, Action<Dictionary<string, Texture2D>> onComplete);
}
