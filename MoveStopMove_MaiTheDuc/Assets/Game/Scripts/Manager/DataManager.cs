using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static DataManager;

public class DataManager : MonoBehaviour
{
    string saveFile;
    private PlayerData playerData = new PlayerData();
    public PlayerData _PlayerData { get => playerData; set => playerData = value; }
    public string SaveFile { get => saveFile; set => saveFile = value; }
    public void ReadData()
    {
        saveFile = Constraint.GetStreamingAssetsPath("PlayerData.json");
        // Does the file exist?
        if (File.Exists(saveFile))
        {
            // Work with JSON
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);
            //  into a pattern matching the GameData class.
            _PlayerData = JsonUtility.FromJson<PlayerData>(fileContents);
        }
    }
    public void SaveData()
    {
        saveFile = Constraint.GetStreamingAssetsPath("PlayerData.json");
        string potion = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFile, potion);
    }

    public void GenerateData(WeaponData weaponData)
    {
        PlayerData playerData = new PlayerData();
        playerData.Player_Name = "You";
        playerData.weapons = new List<Weapon1>();

        for (int i = 0; i < weaponData.Weapon.Count; i++)
        {
            Weapon1 weapon = new Weapon1();
            //weapon.WeaponType = weaponData.Weapon[i].WeaponType;
            //weapon.WeaponName = weaponData.Weapon[i].WeaponName;
            weapon.Buyed = weaponData.Weapon[i].Buyed;
            weapon.Equipped = weaponData.Weapon[i].Equipped;

            playerData.weapons.Add(weapon);
        }
        _PlayerData = playerData;
        SaveData();
    }
    [System.Serializable]
    public class PlayerData
    {
        public string Player_Name;
        public List<Weapon1> weapons = new List<Weapon1>();

    }
    [System.Serializable]
    public class Weapon1
    {
        //[SerializeField] GameObject weaponPrefabs;
        [SerializeField] string weaponName;
        [SerializeField] WeaponType weaponType;
        [SerializeField] int weaponPrice;
        [SerializeField] private bool equipped = false;
        [SerializeField] private bool buyed = false;


        public string WeaponName { get => weaponName; set => weaponName = value; }
        public WeaponType WeaponType { get => weaponType; set => weaponType = value; }
        public int WeaponPrice { get => weaponPrice; set => weaponPrice = value; }
        public bool Equipped { get => equipped; set => equipped = value; }
        public bool Buyed { get => buyed; set => buyed = value; }
    }
}
