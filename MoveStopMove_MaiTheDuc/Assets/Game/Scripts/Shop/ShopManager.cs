using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public int currentWeaponIndex;
    public GameObject[] Weapons;

    void Start()
    {
        currentWeaponIndex = PlayerPrefs.GetInt("SelectedWeapon", 0);

       foreach(GameObject weapon in Weapons) 
            weapon.SetActive(false);

        Weapons[currentWeaponIndex].SetActive(true);
    }

    void Update()
    {
        
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

        PlayerPrefs.SetInt("SlectedWeapon", currentWeaponIndex);
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

        PlayerPrefs.SetInt("SlectedWeapon", currentWeaponIndex);
    }
}
