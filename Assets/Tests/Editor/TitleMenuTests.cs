using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using Zenject;

[TestFixture]
public class TitleMenuTests
{
    private class MockSceneLoader : ISceneLoader
    {
        public bool LoadCalled;
        public bool UnloadCalled;

        public void LoadScene(SceneRef scene) => LoadCalled = true;
        public void UnloadCurrentScene() => UnloadCalled = true;
    }

    private GameObject hostObject;
    private TitleMenu menu;
    private MockSceneLoader mockedSceneLoader;
    private DiContainer container;

    [SetUp]
    public void SetUp()
    {
        // Create instance to inject, so we can assert its state later
        mockedSceneLoader = new MockSceneLoader();

        container = new DiContainer();
        container.Bind<ISceneLoader>().FromInstance(mockedSceneLoader).AsSingle();

        // Setup base object
        hostObject = new GameObject("TitleMenuGO");
        menu = hostObject.AddComponent<TitleMenu>();
        container.Inject(menu);

        hostObject.SetActive(true);
    }

    [Test]
    public void OnClickPhoenixButton_DeactivatesAndLoads()
    {
        menu.OnClickPhoenixButton(null);

        // Assertions
        Assert.IsTrue(mockedSceneLoader.LoadCalled, "LoadScene should have been called.");
        Assert.IsFalse(hostObject.activeSelf, "Menu should deactivate itself.");
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(hostObject);
    }
}
