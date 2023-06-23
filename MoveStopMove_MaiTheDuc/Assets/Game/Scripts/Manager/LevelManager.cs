using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new List<Level>();
    public Player player;
    Level currentLevel;

    int level = 1;

    private void Start()
    {
        LoadLevelX();
    }

    public void LoadLevelX()
    {
        LoadLevel(level);
        OnInit();
    }

    public void LoadLevel(int indexLevel)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        if (level < levels.Count)
        {
            currentLevel = Instantiate(levels[level]);
            //currentLevel.Onint();
        }
        else
        {

        }

        currentLevel = Instantiate(levels[indexLevel - 1]);
    }

    public void OnInit()
    {
        player.transform.position = currentLevel.startPoint.position;
        player.OnInit();
    }

    public void OnStart()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
    }

    public void OnFinish()
    {
        //UIManager.Instance.OpenFinishUI();
        GameManager.Instance.ChangeState(GameState.Finish);
    }

    public void NextLevel()
    {
        level++;
        LoadLevelX();
    }
}
