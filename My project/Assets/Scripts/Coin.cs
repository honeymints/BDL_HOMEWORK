using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<IPlayer>() is IPlayer player)
        {
            player.CountPoints(); // подсчет монеток
            anim.SetTrigger("coin pop");
        }
    }

    private void Pop() //будет выполнятся после окончания анимации взрыва (event)
    {
        Destroy(gameObject);
    }
    
}
