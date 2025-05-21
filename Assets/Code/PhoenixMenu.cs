using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PhoenixMenu : MonoBehaviour
{
    public Animator FireController;

    private UIDocument uiDocument;
    private Button updateFireButton;

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();

        updateFireButton = uiDocument.rootVisualElement.Q<Button>("UpdateFireButton");
        updateFireButton.RegisterCallback<ClickEvent>(OnClickUpdateFireButton);
    }

    private void OnClickUpdateFireButton(ClickEvent evt)
    {
        FireController.SetTrigger("ColorChange");
    }

}
