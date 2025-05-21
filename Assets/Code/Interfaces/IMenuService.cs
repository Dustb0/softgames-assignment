using System;
using System.Collections.Generic;
using UnityEngine;

public interface IMenuService
{
    event Action OnReturnToTitle;

    void BackToTitleMenu(); 
}
