using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    public int currentWeaponIndex;
    public GameObject[] Weapons;

    void Start()
    {
        currentWeaponIndex = PlayerPrefs.GetInt("SelectedWeapon", 0);

        foreach (GameObject weapon in Weapons)
            weapon.SetActive(false);

        Weapons[currentWeaponIndex].SetActive(true);
    }
}
