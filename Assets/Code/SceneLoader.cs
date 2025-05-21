using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/// <summary>
/// Implementation ISceneLoader interface to handle Scene transitions & unloading.
/// </summary>
public class SceneLoader : ISceneLoader
{
    private AsyncOperationHandle<SceneInstance> handle;

    public void LoadScene(SceneRef scene)
    {
        string address = scene switch
        {
            SceneRef.MagicWords => "Assets/Scenes/WordsScene.unity",
            SceneRef.PhoenixFlame => "Assets/Scenes/PhoenixScene.unity",
            SceneRef.AceOfShadows => "Assets/Scenes/AceScene.unity",
            _ => throw new System.NotImplementedException(),
        };

        handle = Addressables.LoadSceneAsync(address, LoadSceneMode.Additive);
    }

    public void UnloadCurrentScene()
    {
        if (handle.IsValid())
        {
            Addressables.UnloadSceneAsync(handle);
        }
    }
}
