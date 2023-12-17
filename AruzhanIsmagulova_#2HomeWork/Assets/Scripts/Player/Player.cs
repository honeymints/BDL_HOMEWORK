using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IPlayer
{
    public PlayerMovement PlayerController { get; private set; }
    public InputAction Movement { get; private set; }
    public static Player _Player { get; private set; }
    
    public int index; //индекс сценки
    public int points; //число очков
    
    private Vector3 _inputMovement;
    [SerializeField] private float speed;
    [SerializeField] private Transform _checkpoint;
    private int facingDirectionX = 1;

    private void Awake()
    {
        Debug.Log("awake has been ");
        PlayerController = new PlayerMovement();
        _Player = this;
    }

    void Update()
    {
        Move();
        Flip();
    }
    
    private void Move()
    {
        _inputMovement = Movement.ReadValue<Vector2>();
        Vector2 playerMovingPos = Vector2.MoveTowards(transform.position, transform.position + _inputMovement,Time.deltaTime*speed);
        transform.position = playerMovingPos;
    }

    private void Flip()
    {
        // проверяем если его input значения по оси x не совпадают с scale x и если игрок двигается
        if (Movement.WasPressedThisFrame() && _inputMovement.x!=transform.localScale.x && _inputMovement.x!=0) 
        {
            facingDirectionX *= -1; //то меняем его скейл
        }
        Vector2 scale = transform.localScale; 
        transform.localScale =  new Vector3(facingDirectionX, (int)scale.y);

    }
    private void OnEnable()
    {
        Movement = PlayerController.Player.Move;
        Movement.Enable();
        SceneManager.sceneLoaded += OnSceneLoad; //проверяет в какой сцене находиться игрок
    }

    void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex.Equals(0))  //если это меню игрок станвоится невидимым
        {
            gameObject.SetActive(false);
        }
        else
        {
            DontDestroy();  //если это другая сцена, то игрок не уничтажается, и становится видимым
            gameObject.SetActive(true); 
        }

        InitializePlayerData();
    }

    private void InitializePlayerData()
    {
        if (DataSaver.DataSave.WasDataSaved())
        {
            index = DataSaver.DataSave.data.SceneIndex;
            points = DataSaver.DataSave.data.Points;
            transform.position = DataSaver.DataSave.data.Position;
        }
        else
        {
            index = SceneManager.GetActiveScene().buildIndex;
            points = 0;
            SpawnInitialPosition();
        }
    }

    private void OnDisable()
    {
       Movement.Disable();
    }

    public void CountPoints()
    {
        points += 1;
    }
    
    public void DontDestroy()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SpawnInitialPosition()
    {
        transform.position = _checkpoint.position;
    }
}
