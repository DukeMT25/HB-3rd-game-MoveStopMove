using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : GameUnit
{
    [SerializeField] private TMPro.TextMeshProUGUI CharacterName;
    [SerializeField] private TMPro.TextMeshProUGUI CharacterLevel;
    [SerializeField] private RawImage imageLevelBG;
    private Character character;
    public void SetCharacter(Character character)
    {
        this.character = character;
    }
    public void UpdateData()
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void OnInit()
    {

    }

    public override void OnDespawn()
    {

    }
}
