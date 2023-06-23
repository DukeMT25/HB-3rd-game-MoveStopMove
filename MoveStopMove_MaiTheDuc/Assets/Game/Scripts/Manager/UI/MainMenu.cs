using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : View
{
    [SerializeField] private Button _weaponButton;
    [SerializeField] private Button _playButton;

    public override void Initialize()
    {
        _weaponButton.onClick.AddListener(() => ViewManager.Show<WeaponMenu>() );

        _playButton.onClick.AddListener(() => ViewManager.Show<PlayView>() );
    }
}
