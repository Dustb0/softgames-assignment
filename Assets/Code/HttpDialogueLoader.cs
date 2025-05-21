using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

/// <summary>
/// Implementation of the IDialogueLoader interface to load dialogue and avatars from a specific endpoint.
/// </summary>
public class HttpDialogueLoader : IDialogueLoader
{
    private readonly string endpoint;

    [Inject]
    public HttpDialogueLoader([Inject(Id = "DialogueEndpoint")] string endpoint)
    {
        this.endpoint = endpoint;
    }

    public IEnumerator LoadDialogue(Action<DialogueResponse> onSuccess, Action<string> onError)
    {
        // 1. Send Request
        using var www = UnityWebRequest.Get(endpoint);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke($"Error retrieving dialogue data: {www.error}");
            Debug.LogError(www.error);
            yield break;
        }

        // 2. Process Response
        try
        {
            var json = www.downloadHandler.text;
            var response = JsonUtility.FromJson<DialogueResponse>(json);
            onSuccess?.Invoke(response);
        }
        catch (Exception e)
        {
            Debug.LogError(www.error);
            onError?.Invoke($"Error parsing dialogue data: {e.Message}");
        }
    }

    public IEnumerator LoadAvatars(List<AvatarEntry> avatarEntries, Action<Dictionary<string, Texture2D>> onComplete)
    {
        var result = new Dictionary<string, Texture2D>();

        // Group by names if there are multiple entries for the same name
        // and take the first valid url
        var groups = avatarEntries.GroupBy(a => a.name);
        foreach (var group in groups)
        {
            Texture2D tex = null;
            
            foreach (var entry in group)
            {
                using var www = UnityWebRequestTexture.GetTexture(entry.url);
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    tex = DownloadHandlerTexture.GetContent(www);
                    break;
                }
                
            }

            result[group.Key] = tex;
        }

        onComplete?.Invoke(result);
    }
}