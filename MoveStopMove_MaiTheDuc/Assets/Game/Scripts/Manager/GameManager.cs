using UnityEngine;

public enum GameState { MainMenu, Gameplay, Finish }

public class GameManager : Singleton<GameManager>
{
    private GameState _state;

    [SerializeField] private Player player;

    public Player Player1 { get => player; set => player = value; }

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    private void Update()
    {
        //Debug.Log(_state.ToString());
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
        return Player1;
    }
}
