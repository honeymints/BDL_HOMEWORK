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
        PlayerController = new PlayerMovement();
        //поняла почему не работал singleton, теперь он работает, вроде.
        //если игрок существует и он не относится к оригинальному классу, то уничтожить
        if (_Player != null && _Player!=this) 
        {
            Destroy(gameObject); 
        }
        else 
        {
            _Player = this;
            DontDestroyOnLoad(_Player);
        }
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
        SceneManager.sceneLoaded += OnSceneLoad;
    }
    
    private void OnDisable()
    {
        Movement.Disable();
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex.Equals(0))  //если это меню, то игрока унижтожаем
        {
            Destroy(this.gameObject);
        }
        else
        {
            InitializePlayerData();  //если нет то инициализируем данные
        }
    }
    private void InitializePlayerData()
    {
        if (DataSaver.DataSave.WasDataSaved()) //если игра была сохранена, то игрок присвает сохраненные значения
        {
            Debug.Log("was saved");
            index = DataSaver.DataSave.data.SceneIndex;
            points = DataSaver.DataSave.data.Points;
            transform.position = DataSaver.DataSave.data.Position;
        }
        else //если игра новая, то игрок получает изначальные данные
        {
            index = SceneManager.GetActiveScene().buildIndex; 
            points = 0;
            SpawnInitialPosition();
        }
    }

    public void CountPoints()
    {
        points += 1;
    }

    public void SpawnInitialPosition()
    {
        transform.position = _checkpoint.position; //при первом спавне игрок будет спавниться на указанных координатах
    }
}
