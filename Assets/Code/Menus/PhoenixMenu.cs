using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PhoenixMenu : BaseSubMenu
{
    public Animator FireController;

    private Button updateFireButton;

    protected override void Start()
    {
        base.Start();
        updateFireButton = uiDocument.rootVisualElement.Q<Button>("UpdateFireButton");
        updateFireButton.RegisterCallback<ClickEvent>(OnClickUpdateFireButton);
    }

    private void OnClickUpdateFireButton(ClickEvent evt)
    {
        FireController.SetTrigger("ColorChange");
    }

}
