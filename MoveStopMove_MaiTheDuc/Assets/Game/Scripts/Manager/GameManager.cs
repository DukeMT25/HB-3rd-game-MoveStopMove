using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public enum GameState { MainMenu, Gameplay, Finish }

public class GameManager : Singleton<GameManager>
{
    private GameState _state;

    [SerializeField] private Player player;
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

    public Player Player()
    {
        return player;
    }
}
