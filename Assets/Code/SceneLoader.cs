using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

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
