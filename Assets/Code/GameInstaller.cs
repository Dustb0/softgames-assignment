using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("Installing Bindings");
        Container.Bind<ISceneLoader>()
         .To<SceneLoader>()
         .AsSingle();
    }
}