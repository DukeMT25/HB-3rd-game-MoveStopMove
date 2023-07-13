using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    [Header("Choose Button:")]
    [SerializeField] private Button _EquippedButton;
    [SerializeField] private Button _SelectButton;
    [SerializeField] private Button _BuyButton;
    [SerializeField] private Button _UnBuyButton;


    [Header("Index:")]
    public int currentWeaponIndex;
    public GameObject[] Weapons;

    public Player _player;
    public GameManager _gameManager;

    [Header("Hand:")]
    public GameObject[] Weapons2;

    void Start()
    {
        _gameManager = GameManager.Instance;
        _player = _gameManager.Player1;
        

        foreach (GameObject weapon in Weapons)
            weapon.SetActive(false);

        Weapons[currentWeaponIndex].SetActive(true);

        ShowBtn();
    }

    void Update()
    {
        Weapons2[currentWeaponIndex].SetActive(true);
    }

    public void ChangeNext()
    {
        Weapons[currentWeaponIndex].SetActive(false);

        currentWeaponIndex++;
        if(currentWeaponIndex == Weapons.Length)
        {
            currentWeaponIndex = 0;
        }

        Weapons[currentWeaponIndex].SetActive(true);
        HideWeaponsOnHand();
        ShowBtn();
    }

    public void ChangePrev()
    {
        Weapons[currentWeaponIndex].SetActive(false);

        currentWeaponIndex--;
        if(currentWeaponIndex == -1)
        {
            currentWeaponIndex = Weapons.Length - 1;
        }

        Weapons[currentWeaponIndex].SetActive(true);
        HideWeaponsOnHand();
        ShowBtn();
    }

    public void SetWeapon()
    {
        PlayerPrefs.SetInt(Constraint.SELECTED_WEAPON, currentWeaponIndex);
        PlayerPrefs.Save();
        Weapons[currentWeaponIndex].GetComponent<Weapon>().Equipped = true;

        _player.UpdateWeapon();
        HideWeaponsOnHand();
        ShowBtn();
    }

    public void BuyWeapon()
    {
        HideWeaponsOnHand();
        SetWeapon();
        Weapons[currentWeaponIndex].GetComponent<Weapon>().Buyed = true;
    }

    private void ShowBtn()
    {
        if (Weapons[currentWeaponIndex].GetComponent<Weapon>().Equipped)
        {
            foreach (GameObject weapon in Weapons)
            {
                if (weapon != Weapons[currentWeaponIndex])
                {
                    weapon.GetComponent<Weapon>().Equipped = false;
                    weapon.GetComponent<Weapon>().Select = true;
                }
            }

            HideAll();
            _EquippedButton.gameObject.SetActive(true);
        }
        else if (!Weapons[currentWeaponIndex].GetComponent<Weapon>().Buyed && Weapons[currentWeaponIndex - 1].GetComponent<Weapon>().Buyed)
        {
            HideAll();
            _BuyButton.gameObject.SetActive(true);
        }
        else if (!Weapons[currentWeaponIndex].GetComponent<Weapon>().Buyed && !Weapons[currentWeaponIndex - 1].GetComponent<Weapon>().Buyed)
        {
            HideAll();
            _UnBuyButton.gameObject.SetActive(true);
        }
        else
        {
            HideAll();
            _SelectButton.gameObject.SetActive(true);
        }
    }

    private void HideAll()
    {
        _EquippedButton.gameObject.SetActive(false);
        _SelectButton.gameObject.SetActive(false);
        _BuyButton.gameObject.SetActive(false);
        _UnBuyButton.gameObject.SetActive(false);
    }

    private void HideWeaponsOnHand()
    {
        foreach (GameObject weapon in Weapons2)
        {
            if (weapon != Weapons2[currentWeaponIndex])
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }
}
