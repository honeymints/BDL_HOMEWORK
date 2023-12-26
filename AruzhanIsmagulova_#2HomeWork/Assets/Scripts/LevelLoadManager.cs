using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
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
        //проверяет если игра новая
        if (isNewGame)
        {
            Debug.Log("new game");
            SceneManager.LoadScene(1); //если игра новая, то загружает первый уровень
            File.Delete(Path.Combine(Application.streamingAssetsPath, "PLAYERDATA.json")); // и удаляет последние загруженные данные
            AssetDatabase.Refresh(); 
        }
        else
        {
            DataSaver.DataSave.LoadData(); // если игрок решил продолжить игру, то он загруживает сохраненные данные 
            SceneManager.LoadScene(DataSaver.DataSave.data.SceneIndex);// возвращается на ту сцену с помощью индекса  
            
        }

    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
