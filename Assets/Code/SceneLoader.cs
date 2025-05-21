using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private static AsyncOperationHandle<SceneInstance> handle;

    public enum Scene
    {
        AceOfShadows,
        MagicWords,
        PhoenixFlame
    }

    public static void LoadScene(Scene scene)
    {
        var sceneAddress = "";

        switch (scene)
        {
            case Scene.PhoenixFlame:
                sceneAddress = "Assets/Scenes/PhoenixScene.unity";
                break;
        }

        handle = Addressables.LoadSceneAsync(sceneAddress, LoadSceneMode.Additive);
    }

    public static void UnloadCurrentScene()
    {
        if (handle.IsValid())
        {
            Addressables.UnloadSceneAsync(handle);
        }
    }
}
