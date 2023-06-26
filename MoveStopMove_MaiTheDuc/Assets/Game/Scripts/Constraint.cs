using System.Collections;
using System.Collections.Generic;
using System.IO;
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
