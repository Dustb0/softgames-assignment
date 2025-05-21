using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        Debug.Log("Installing Bindings");
        Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        Container.Bind<IDialogueLoader>().To<HttpDialogueLoader>().AsSingle();
        Container.Bind<IMenuService>().To<MenuService>().AsSingle();

        Container.Bind<string>().WithId("DialogueEndpoint").FromInstance("https://private-624120-softgamesassignment.apiary-mock.com/v3/magicwords");
    }
}