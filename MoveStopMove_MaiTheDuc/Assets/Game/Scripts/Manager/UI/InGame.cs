using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : UICanvas
{
    public void SettingButton()
    {
        Debug.Log("Pause");
        UIManager.Instance.OpenUI<Pause>();

        Close();
    }
}
