using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleMenu : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button phoenixButton;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();

        phoenixButton = uiDocument.rootVisualElement.Q<Button>("PhoenixButton");
        phoenixButton.RegisterCallback<ClickEvent>(OnClickPhoenixButton);
    }

    private void OnClickPhoenixButton(ClickEvent evt)
    {
        SceneLoader.LoadScene(SceneLoader.Scene.PhoenixFlame);
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
}
