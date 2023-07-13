using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Constraint : MonoBehaviour
{
    public static int danceName = Animator.StringToHash("Dance");
    public static int idleName = Animator.StringToHash("Idle");
    public static int runName = Animator.StringToHash("Run");
    public static int atkName = Animator.StringToHash("Attack");
    public static int ultiName = Animator.StringToHash("Ulti");
    public static int winName = Animator.StringToHash("Win");
    public static int deadName = Animator.StringToHash("Dead");

    public const string SELECTED_WEAPON = "SelectedWeapon";
    public const string COIN = "Coin";

    public const string LEVEL = "Level";
    public const string LAYOUT_WALL = "Wall";
    public const float RAYCAST_HIT_RANGE_WALL = 1.0f;

    private static List<string> names = new List<string>()
    {
        "Ailen",
        "Lakshmana",
        "Æthelthryth",
        "Fido",
        "Kajal",
        "Ankur",
        "Chelsea",
        "Sulaiman",
        "Yarognev",
        "Faustina",
        "Ferkó",
        "Séraphine",
        "Zhaleh",
        "Bojana",
        "Nancy",
        "Madhukar",
        "Vimal",
        "Quinctilianus",
        "Mahine",
        "Lacie",
    };

    public static List<string> GetNames(int amount)
    {
        var list = names.OrderBy(d => System.Guid.NewGuid());
        return list.Take(amount).ToList();
    }

    public static string GetRandomName()
    {
        return names[Random.Range(0, names.Count)];
    }

    public static bool isWall(GameObject a, LayerMask _layerMask)
    {
        RaycastHit hit;
        bool isWall = false;
        if (Physics.Raycast(a.transform.position, a.transform.TransformDirection(Vector3.forward), out hit, Constraint.RAYCAST_HIT_RANGE_WALL, _layerMask))
        {
            isWall = true;
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
        }
        else
        {
            isWall = false;
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
        return isWall;
    }

    public static bool IsDes(Vector3 a, Vector3 b, float range)
    {

        float distance = Vector3.Distance(a, b);
        return distance < range;
    }

    public static string GetStreamingAssetsPath(string fileName)
    {
        string dbPath;

#if UNITY_EDITOR
        dbPath = string.Format(@"Assets/StreamingAssets/{0}", fileName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, fileName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->
#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + fileName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + fileName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + fileName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + fileName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	    var loadDb = Application.dataPath + "/StreamingAssets/" + fileName;  // this is the path to your StreamingAssets in iOS
	    // then save to Application.persistentDataPath
	    File.Copy(loadDb, filepath);

#endif
            Debug.Log("Database written");
        }
        dbPath = filepath;
#endif

        return dbPath;
    }
}
