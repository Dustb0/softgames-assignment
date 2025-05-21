using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class TitleMenu : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button phoenixButton;
    private Button wordsButton;
    private Button aceButton;

    [Inject]
    private ISceneLoader sceneLoader;

    private static TitleMenu instance;

    void Start()
    {
        instance = this;

        // Setup and Bind UI-Elements
        uiDocument = GetComponent<UIDocument>();
        RegisterUIElements();
    }

    private void RegisterUIElements()
    {
        phoenixButton = uiDocument.rootVisualElement.Q<Button>("PhoenixButton");
        phoenixButton.RegisterCallback<ClickEvent>(OnClickPhoenixButton);

        wordsButton = uiDocument.rootVisualElement.Q<Button>("WordsButton");
        wordsButton.RegisterCallback<ClickEvent>(OnClickWordsButton);

        aceButton = uiDocument.rootVisualElement.Q<Button>("AceButton");
        aceButton.RegisterCallback<ClickEvent>(OnClickAceButton);
    }

    public void OnClickPhoenixButton(ClickEvent evt)
    {
        sceneLoader.LoadScene(SceneRef.PhoenixFlame);
        CloseMenu();
    }

    public void OnClickWordsButton(ClickEvent evt)
    {
        sceneLoader.LoadScene(SceneRef.MagicWords);
        CloseMenu();
    }

    public void OnClickAceButton(ClickEvent evt)
    {
        sceneLoader.LoadScene(SceneRef.AceOfShadows);
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
        
        // Elements have to be re-registered because they get destoryed on disable
        instance.RegisterUIElements();
    }
}
