using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retry : UICanvas
{
    public void RetryButton()
    {
        LevelManager.Instance.OnStart();
        UIManager.Instance.OpenUI<InGame>();
        Close();
    }

    public void HomeButton()
    {
        LevelManager.Instance.ReturnHome();
        Close();
    }
}
