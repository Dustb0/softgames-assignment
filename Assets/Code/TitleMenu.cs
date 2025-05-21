using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleMenu : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button phoenixButton;

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

    public static void ShowMenu()
    {
        instance.gameObject.SetActive(true);
    }
}
