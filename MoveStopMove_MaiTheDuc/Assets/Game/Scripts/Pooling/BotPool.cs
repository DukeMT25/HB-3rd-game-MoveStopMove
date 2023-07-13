using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPool : MonoBehaviour
{
    [SerializeField] AI AI_prefab;

    public List<AI> List_AI = new List<AI>();

    private void Start()
    {
        //SpawnAI();
    }

    private void Update()
    {
        SpawnAI();
    }

    public void SpawnAI()
    {
        for (int i = 0; i < 20; i++)
        {
            int xPos = UnityEngine.Random.Range(-55, 55);
            int zPos = UnityEngine.Random.Range(-55, 55);
            AI _ai = SimplePool.Spawn<AI>(AI_prefab, new Vector3(xPos, 0, zPos), Quaternion.identity);
            List_AI.Add(_ai);
        }
    }

    internal void RemoveBot(AI _ai)
    {
        List_AI.Remove(_ai);
        SimplePool.Release(_ai);
    }
}
