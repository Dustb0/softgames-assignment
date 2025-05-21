using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class TitleMenu : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button phoenixButton;
    
    [Inject]
    private ISceneLoader sceneLoader;

    private static TitleMenu instance;

    void Start()
    {
        instance = this;
        uiDocument = GetComponent<UIDocument>();

        phoenixButton = uiDocument.rootVisualElement.Q<Button>("PhoenixButton");
        phoenixButton.RegisterCallback<ClickEvent>(OnClickPhoenixButton);
    }

    private void OnClickPhoenixButton(ClickEvent evt)
    {
        sceneLoader.LoadScene(SceneRef.PhoenixFlame);
        CloseMenu();
    }

    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        phoenixButton.UnregisterCallback<ClickEvent>(OnClickPhoenixButton);
    }

    public static void BackToTitle()
    {
        instance.sceneLoader.UnloadCurrentScene();
        instance.gameObject.SetActive(true);
    }
}
