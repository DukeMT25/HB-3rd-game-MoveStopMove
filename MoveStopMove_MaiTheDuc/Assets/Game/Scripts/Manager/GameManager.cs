using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, Gameplay, Finish }

public class GameManager : Singleton<GameManager>
{
    private GameState _state;

    [SerializeField] List<ObjectPool> weaponObjectPool;
    [SerializeField] WeaponSpawner _weaponspawner;
    [SerializeField] GameObject weaponHolder;

    [SerializeField] List<ObjectPool> _aiObjectPool;
    [SerializeField] AISpawner _aISpawner;
    [SerializeField] GameObject aiHolder;

    public List<ObjectPool> WeaponObjectPool { get => weaponObjectPool; set => weaponObjectPool = value; }
    public WeaponSpawner Weaponspawner { get => _weaponspawner; set => _weaponspawner = value; }
    public GameObject WeaponHolder { get => weaponHolder; set => weaponHolder = value; }

    public List<ObjectPool> AiObjectPool { get => _aiObjectPool; set => _aiObjectPool = value; }
    public AISpawner AISpawner { get => _aISpawner; set => _aISpawner = value; }
    public GameObject AiHolder { get => aiHolder; set => aiHolder = value; }


    void Awake()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState gameState)
    {
        _state = gameState;
    }

    public bool IsState(GameState gameState)
    {
        return _state == gameState;
    }
}
