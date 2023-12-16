using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private float _jumpForce;
    private int facingDirectionX = 1;
    
    
    private void Awake()
    {
        PlayerController = new PlayerMovement();
        _Player = this;
    }
    
    void Start()
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
        if (Movement.WasPressedThisFrame() && _inputMovement.x!=transform.localScale.x && _inputMovement.x!=0)
        {
            facingDirectionX *= -1;
        }
        Vector2 scale = transform.localScale;
        transform.localScale =  new Vector3(facingDirectionX, (int)scale.y);

    }
    private void OnEnable()
    {
        Movement = PlayerController.Player.Move;
        Movement.Enable();
    }

    private void OnDisable()
    {
       Movement.Disable();
    }

    public void CountPoints()
    {
        points += 1;
    }

    public void MakeDamage()
    {
        throw new NotImplementedException();
    }
    
}
