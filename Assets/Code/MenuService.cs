using System;
using System.Collections;
using UnityEngine;

public class MenuService : IMenuService
{
    public event Action OnReturnToTitle;

    public void BackToTitleMenu()
    {
        OnReturnToTitle.Invoke();
    }
}