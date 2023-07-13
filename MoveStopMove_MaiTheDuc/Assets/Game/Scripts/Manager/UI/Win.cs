using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : UICanvas
{
    public void HomeButton()
    {
        LevelManager.Instance.ReturnHome();
        Close();
    }

    public void NextButton()
    {
        LevelManager.Instance.OnNextLevel();
        Close();
    }
}
