using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void PlayButton()
    {
        Debug.Log("Play");
        LevelManager.Instance.OnStart();

        UIManager.Instance.OpenUI<InGame>();
        Close();
    }

    public void WeaponButton()
    {
        Debug.Log("Shop");
        UIManager.Instance.OpenUI<WeaponShop>();
        Close();
    }
}
