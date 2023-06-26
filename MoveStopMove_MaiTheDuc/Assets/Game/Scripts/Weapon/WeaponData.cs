using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField] List<Weapon1> weapon;
    public List<Weapon1> Weapon { get => weapon; set => weapon = value; }

    [System.Serializable]
    public class Weapon1
    {
        //[SerializeField] GameObject weaponPrefabs;
        [SerializeField] string weaponName;
        [SerializeField] WeaponType weaponType;
        [SerializeField] int weaponPrice;
        [SerializeField] private bool equipped = false;
        [SerializeField] private bool buyed = false;

        [SerializeField] private Material _mat;


        public string WeaponName { get => weaponName; set => weaponName = value; }
        public WeaponType WeaponType { get => weaponType; set => weaponType = value; }
        public int WeaponPrice { get => weaponPrice; set => weaponPrice = value; }
        public bool Equipped { get => equipped; set => equipped = value; }
        public bool Buyed { get => buyed; set => buyed = value; }
        public Material Mat { get => _mat; set => _mat = value; }
    }
}

