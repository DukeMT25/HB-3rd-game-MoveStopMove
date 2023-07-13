using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revive : UICanvas
{
    public void ReviveButton()
    {
        LevelManager.Instance.OnRevive();
        Close();
    }

    public void GiveUp()
    {
        LevelManager.Instance.OnFinish();
        UIManager.Instance.OpenUI<Retry>();
        Close();
    }
}
