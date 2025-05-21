using UnityEngine;
using UnityEngine.UIElements;

public class AceMenu : BaseSubMenu
{
    private Label messageLabel;

    protected override void Start()
    {
        base.Start();
        messageLabel = uiDocument.rootVisualElement.Q<Label>("MessageLabel");
        messageLabel.visible = false;
    }

    public void OnCardAnimationFinish()
    {
        messageLabel.text = "All Cards have been moved!";
        messageLabel.visible = true;
    }
}
