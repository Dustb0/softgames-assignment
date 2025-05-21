using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseSubMenu : MonoBehaviour
{
    protected UIDocument uiDocument;
    private Button backToTitleButton;

    protected virtual void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        backToTitleButton = uiDocument.rootVisualElement.Q<Button>("BackToTitleButton");
        backToTitleButton.RegisterCallback<ClickEvent>(OnClickTitleButton);
    }

    private void OnClickTitleButton(ClickEvent evt)
    {
        SceneLoader.UnloadCurrentScene();
        TitleMenu.ShowMenu();
    }

   
}
