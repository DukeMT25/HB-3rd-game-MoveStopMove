using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels;

    public Player player;
    private Level currentLevel;

    //Pooling list
    private List<Weapon> weapons = new List<Weapon>();
    private List<AI> bots = new List<AI>();
    //Level
    private int levelIndex;
    private int botAmount;
    private int botInLevel;
    //
    private int botsInGame;

    

    public List<Weapon> Weapons { get => weapons; set => weapons = value; }
    public List<AI> Bots { get => bots; set => bots = value; }
    public int BotsInGame { get => botsInGame; set => botsInGame = value; }

    private void Awake()
    {
        //PlayerPrefs.SetInt(Constraint.LEVEL, 0);
        //levelIndex = PlayerPrefs.GetInt(Constraint.LEVEL, 0);
        levelIndex = 0;
    }

    private void Start()
    {
        LoadLevel(levelIndex);
        OnInit();
        UIManager.Instance.OpenUI<MainMenu>();
    }

    public void OnInit()
    {

        player.OnInit();

        if (levels[levelIndex] == null) botInLevel = 20;
        else botInLevel = levels[levelIndex].GetBotInGame();

        BotsInGame = 0;


        player.transform.position = levels[levelIndex].GetStartPoint().position;
    }

    public void GenerateBotAI(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int xPos;
            int zPos;
            int j = Random.Range(0, 4);
            if (j == 0)
            {
                xPos = Random.Range(-40, -5);
                zPos = Random.Range(-20, 5);

                SpawnAI(xPos, zPos);
            }
            else if (j == 1)
            {
                xPos = Random.Range(5, 40);
                zPos = Random.Range(-20, 5);

                SpawnAI(xPos, zPos);
            }
            else if (j == 2)
            {
                xPos = Random.Range(-40, -5);
                zPos = Random.Range(15, 40);

                SpawnAI(xPos, zPos);
            }
            else if (j == 3)
            {
                xPos = Random.Range(5, 40);
                zPos = Random.Range(15, 40);

                SpawnAI(xPos, zPos);
            }            
        }
    }

    public void SpawnAI(int xPos, int zPos)
    {
        AI ai = SimplePool.Spawn<AI>(PoolType.Bot, new Vector3(xPos, 0.5f, zPos), Quaternion.identity);
        ai.OnInit();
        Bots.Add(ai);

        BotsInGame++;
    }

    public void LoadLevel(int indexLevel)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        if (levelIndex < levels.Count)
        {
            currentLevel = Instantiate(levels[levelIndex]);
            //currentLevel.OnInit();
            
        }
        else
        {

        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay) && BotsInGame < botInLevel)
        {
            GenerateBotAI(1);
        }
    }

    public void OnStart()
    {
        OnReset();
        //levelIndex = PlayerPrefs.GetInt(Constraint.LEVEL, 0);
        LoadLevel(levelIndex);
        OnInit();
        GameManager.Instance.ChangeState(GameState.Gameplay);
    }

    public void OnFinish()
    {
        OnReset();
        UIManager.Instance.CloseUI<InGame>();
        GameManager.Instance.ChangeState(GameState.Finish);
    }

    public void OnRevive()
    {
        player.hp = 1;
        player.StateMachine.Initialize(player.IdleState);
        UIManager.Instance.OpenUI<InGame>();
    }

    public void OnNextLevel()
    {
        OnReset();
        levelIndex++;
        PlayerPrefs.SetInt(Constraint.LEVEL, levelIndex);
        BotsInGame = 0;
        
        LoadLevel(levelIndex);
        OnInit();
        UIManager.Instance.OpenUI<InGame>();
        GameManager.Instance.ChangeState(GameState.Gameplay);
    }

    public void OnReset()
    {
        SimplePool.CollectAll();
        Bots.Clear();
        CameraFollow.Instance.ResetOffset();
        player.SetTransformPosition(levels[levelIndex].GetStartPoint());
        player.OnInit();
    }

    public void ReturnHome()
    {
        OnReset();
        UIManager.Instance.OpenUI<MainMenu>();
        GameManager.Instance.ChangeState(GameState.MainMenu);
    }
}
