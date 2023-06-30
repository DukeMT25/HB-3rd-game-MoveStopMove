using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels;
    [SerializeField] int node = 45;
    [SerializeField] float offset = 18;
    public Player player;

    Level currentLevel;

    private List<AI> bots = new List<AI>();
    private List<AI> botsInGame = new List<AI>();
    private int levelIndex;
    private int botAmount;
    private int botInGame;
    private int botInStack;

    private void Awake()
    {
        levelIndex = PlayerPrefs.GetInt(Constraint.LEVEL, 0);
    }

    private void Start()
    {
        LoadLevel(levelIndex);
        OnInit();
    }

    private void FixedUpdate()
    {
        if (botInStack > 0)
        {
            for (int i = 0; i < bots.Count; i++)
            {
                if (!bots[i].gameObject.activeSelf && !bots[i].IsDead && botsInGame.Count < botInGame)
                {
                    bots[i].gameObject.SetActive(true);
                    //bots[i].ChangeState(new IdleState());
                    botsInGame.Add(bots[i]);
                    botInStack--;
                }
            }
        }
        else if (botsInGame.Count == 0)
        {
            //UIManager.Instance.OpenUI<Win>();
            //UIManager.Instance.CloseUI<InGame>();
        }
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
            //currentLevel.Onint();
        }
        else
        {

        }

        //currentLevel = Instantiate(levels[indexLevel - 1]);
    }

    public void OnInit()
    {
        player.OnInit();
        Debug.Log(levelIndex);
        botAmount = levels[levelIndex].GetBotAmount();
        Debug.Log(botAmount.ToString());
        botInGame = levels[levelIndex].GetBotInGame();
        Debug.Log(botInGame.ToString());
        GenerateBotAI(botAmount, GeneratePoolObjectPosition(transform.position, node));
        player.transform.position = levels[levelIndex].GetStartPoint().position;
    }

    private void GenerateBotAI(int index, List<Vector3> listPoolObjectPosition)
    {
        for (int i = 0; i < index; i++)
        {
            int randomIndex = Random.Range(0, listPoolObjectPosition.Count);
            while (IsDesAllCharacter(listPoolObjectPosition[randomIndex]))
            {
                randomIndex = Random.Range(0, listPoolObjectPosition.Count);
            }
            AI bot = SimplePool.Spawn<AI>(PoolType.Bot, listPoolObjectPosition[randomIndex], Quaternion.identity);
            bot.OnInit();
            //bot.UpdateInfo(GameManager.Instance.GetBotAIInfo(i), GameManager.Instance.GetAccessoriesDatas());

            //Indicator indicator = SimplePool.Spawn<Indicator>(PoolType.Indicator);
            //indicator.SetCharacter(Constant.Cache.GetCharacter(bot.gameObject));
            //indicator.gameObject.SetActive(false);
            //bot.Indicator = indicator;

            //CharacterInfo characterInfo = SimplePool.Spawn<CharacterInfo>(PoolType.CharacterInfo);
            //characterInfo.SetCharacter(Constant.Cache.GetCharacter(bot.gameObject));
            //characterInfo.gameObject.SetActive(false);
            //bot.CharacterInfo = characterInfo;
            //bot.gameObject.SetActive(false);
            //bots.Add(bot);
        }
    }

    private bool IsDesAllCharacter(Vector3 vector3)
    {
        bool isDesAllCharacter = false;
        isDesAllCharacter = Constraint.IsDes(GameManager.Instance.Player().transform.position, vector3, GameManager.Instance.Player().InGameAttackRange);
        for (int i = 0; i < bots.Count; i++)
        {
            if (Constraint.IsDes(bots[i].transform.position, vector3, bots[i].InGameAttackRange))
            {
                isDesAllCharacter = true;
                break;
            }
            else
            {

            }
        }
        return isDesAllCharacter;
    }

    protected List<Vector3> GeneratePoolObjectPosition(Vector3 a_root, int numCount)
    {
        List<Vector3> listPoolObjectPosition = new List<Vector3>();
        int Row = Mathf.CeilToInt(Mathf.Sqrt(numCount));
        int Column = Row;
        for (int i = 0; i < Row; i++)
        {
            for (int j = 0; j < Column; j++)
            {
                int index = Row * j + i;
                Vector3 objectPosition = new Vector3((j - (Row / 2)) + offset * j + a_root.x, 0.05f + a_root.y, ((Column / 2) - i) - offset * i + a_root.z);
                listPoolObjectPosition.Add(objectPosition);
            }
        }
        return listPoolObjectPosition;
    }

    public void OnStart()
    {
        LoadLevel(levelIndex);
        OnInit();
        GameManager.Instance.ChangeState(GameState.Gameplay);
    }

    public void OnFinish()
    {
        //UIManager.Instance.OpenFinishUI();
        GameManager.Instance.ChangeState(GameState.Finish);
    }

    public void NextLevel()
    {
        levelIndex++;
        LoadLevel(levelIndex);
    }

    public void OnReset()
    {
        SimplePool.CollectAll();
        bots.Clear();
        botsInGame.Clear();

        player.SetTransformPosition(levels[levelIndex].GetStartPoint());

        player.OnInit();
    }
}
