using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

/// <summary>
/// Baseclass for all menus in the sub scenes / games. 
/// Provides functionality such as returning to the Title Screen or displaying FPS.
/// </summary>
public abstract class BaseSubMenu : MonoBehaviour
{
    protected UIDocument uiDocument;
    private Button backToTitleButton;
    private Label fpsLabel;

    [Inject]
    private IMenuService menuService;

    int frameCount;
    float fpsElapsedTime;
    float fpsUpdateInterval = 1;

    protected virtual void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        backToTitleButton = uiDocument.rootVisualElement.Q<Button>("BackToTitleButton");
        backToTitleButton.RegisterCallback<ClickEvent>(OnClickTitleButton);

        fpsLabel = uiDocument.rootVisualElement.Q<Label>("FPSLabel");
        fpsLabel.text = "";
    }

    protected virtual void Update() 
    {
        // Only update FPS display in certain intervals for better readability 
        frameCount++;
        fpsElapsedTime += Time.unscaledDeltaTime;
        if (fpsElapsedTime >= fpsUpdateInterval)
        {
            var fps = Mathf.FloorToInt(frameCount / fpsElapsedTime);
            fpsLabel.text = $"FPS: {fps}";
            frameCount = 0;
            fpsElapsedTime = 0;
        }     
    }

    private void OnClickTitleButton(ClickEvent evt)
    {
        menuService.BackToTitleMenu();
    }
   
}
