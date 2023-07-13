using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : UICanvas
{
    public override void Open()
    {
        Time.timeScale = 0;
        base.Open();
    }

    public override void Close()
    {
        Time.timeScale = 1;
        base.Close();
    }

    public void ContinueButton()
    {
        Debug.Log("Continue");
        UIManager.Instance.OpenUI<InGame>();
        Close();
    }

    public void HomeButton()
    {
        LevelManager.Instance.ReturnHome();
        Close();
    }
}
