using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoadManager : MonoBehaviour
{
    public static LevelLoadManager LoadManager;
    [SerializeField] private Button newGame;
    [SerializeField] private Button continueGame;
    
    private void Awake()
    {
        LoadManager = this;
    }

    private void OnEnable()
    {
        newGame.onClick.AddListener(delegate { LoadScene(true); });
        continueGame.onClick.AddListener(delegate { LoadScene(false); });
    }

    public void LoadScene(bool isNewGame)
    {
        if (isNewGame)
        {
            SceneManager.LoadScene(1);
            File.Delete(Path.Combine(Application.streamingAssetsPath, "PLAYERDATA.json"));
        }
        else
        {
            DataSaver.DataSave.LoadData();
            SceneManager.LoadScene(DataSaver.DataSave.data.SceneIndex);   
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
