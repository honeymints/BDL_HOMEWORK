using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private float _movementSpeed;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 movementInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        movementInput = new Vector3(horizontalInput, 0, verticalInput);

        Vector3 movement = Vector3.Lerp(transform.position, transform.position + movementInput,
            Time.deltaTime * _movementSpeed);
        transform.position = movement;
    }
}
