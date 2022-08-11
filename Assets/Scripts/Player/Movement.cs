using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField]
    private float _maxSpeed;

    public float currentSpeed {
        get => _rb.velocity.magnitude;
    }

    public float maxSpeed
    {
        get => _maxSpeed;
    }

    private Rigidbody _rb;
    private Vector3 _inputAxis;
    private Vector3 _direction;

    private void OnEnable()
    {
        InputManager.onGroundMovement += GetInput;
    }

    private void OnDisable()
    {
        InputManager.onGroundMovement -= GetInput;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        _direction = (transform.forward * _inputAxis.z) + (transform.right * _inputAxis.x);
        _rb.velocity = _direction * _maxSpeed;

        if (!IsGrounded())
        {
            //player fall
            _rb.velocity -= new Vector3(0, 9.81f, 0);
            
        }
    }

    //gets the input 
    private void GetInput(Vector2 axis)
    {
        _inputAxis = new Vector3(axis.x , 0, axis.y);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -transform.up, 1.0f);
    }
}
