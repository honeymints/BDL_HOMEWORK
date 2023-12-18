using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class ButtonEventHandler : MonoBehaviour
{
    [SerializeField] private Button _buttonSettings;
    [SerializeField] private GameObject panel;
    private void OnEnable()
    {
        _buttonSettings.onClick.AddListener(EnableMenuPanel);
    }
    private void OnDisable()
    {
        _buttonSettings.onClick.RemoveAllListeners();
    }
    

    private void EnableMenuPanel()
    {
        panel.SetActive(true); //включает панельку, с которым можно вернуться в игру или выйти на главное меню
    }

    public void DisableMenuPanel()
    {
        panel.SetActive(false); 
    }
    
    //кнопка, с помощью которой можно сохранить данные и вернуться на главное меню  
    public void SaveAndReturnToMenu()
    {
        DataSaver.DataSave.SaveData(); 
        LevelLoadManager.LoadManager.ExitToMenu();
    }
}
