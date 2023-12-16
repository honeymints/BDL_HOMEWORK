using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataSaver : MonoBehaviour
{
    public static DataSaver DataSave;
    private void Awake()
    {
        DataSave = this;
    }

    public Data data = new Data();
    // Start is called before the first frame update
    public void SaveData()
    {
        Vector3 position = Player._Player.transform.position;
        int index = Player._Player.index;
        int points = Player._Player.points;
        data = new Data(position, index, points);

        string json = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.streamingAssetsPath, "PLAYERDATA.json");
        
        File.WriteAllText(path, json);
        Debug.Log("saved new json:" + json);
    }

    public bool WasDataSaved()
    {
        if (File.Exists(Path.Combine(Application.streamingAssetsPath, "PLAYERDATA.json")))
        {
            Debug.Log("Was saved");
            return true;
        }
        Debug.Log("Wasn't saved");
        return false;
    }
    public void LoadData()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "PLAYERDATA.json");
        data = JsonUtility.FromJson<Data>(File.ReadAllText(path));
        
    }
}
public class Data
{
    public Vector3 Position; 
    public int SceneIndex;
    public int Points;
    public Data() {}

    public Data(Vector3 position, int sceneIndex, int points)
    {
        Position = position;
        SceneIndex = sceneIndex;
        Points = points;
    }
    
};
    
