using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

/// <summary>
/// The Main Menu from which all other sub menus / scenes are called.
/// </summary>
public class TitleMenu : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button phoenixButton;
    private Button wordsButton;
    private Button aceButton;

    [Inject]
    private ISceneLoader sceneLoader;

    [Inject]
    private IMenuService menuService;

    void Start()
    {
        // Setup and Bind UI-Elements
        uiDocument = GetComponent<UIDocument>();
        RegisterUIElements();

        menuService.OnReturnToTitle += OnBackToTitle;
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

    public void OnBackToTitle()
    {
        sceneLoader.UnloadCurrentScene();
        gameObject.SetActive(true);
        
        // Elements have to be re-registered because they get destoryed on disable
        RegisterUIElements();
    }
}
