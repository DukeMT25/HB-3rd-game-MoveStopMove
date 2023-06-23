using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayView : View
{
    [SerializeField] private Button _settingButton;

    public override void Initialize()
    {
        _settingButton.onClick.AddListener(() => ViewManager.ShowLast());
    }
}
