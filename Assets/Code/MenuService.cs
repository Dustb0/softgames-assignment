using System;

public class MenuService : IMenuService
{
    public event Action OnReturnToTitle;

    public void BackToTitleMenu()
    {
        OnReturnToTitle.Invoke();
    }
}