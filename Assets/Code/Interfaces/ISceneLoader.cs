using UnityEngine.SceneManagement;

public enum SceneRef
{
    AceOfShadows,
    MagicWords,
    PhoenixFlame
}

public interface ISceneLoader
{
    public void LoadScene(SceneRef scene);

    public void UnloadCurrentScene();
}