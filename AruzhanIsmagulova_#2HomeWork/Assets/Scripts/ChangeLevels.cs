using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevels : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<IPlayer>() is IPlayer player)
        {
            int playerIndex=col.gameObject.GetComponent<Player>().index; //берет индекс активной сценки в котором был игрок
            //  //не уничтожает игрока когда надо прогружать новую сценку
            ChangeLevelToSecond(playerIndex, player); 
        }
    }
    private void ChangeLevelToSecond(int index, IPlayer player)
    {
        SceneManager.LoadScene(index + 1);
        player.SpawnInitialPosition();
    }
    
}
