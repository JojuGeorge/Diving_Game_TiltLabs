using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _canJump;
    [SerializeField] private float _power;
    [Range(1,5)] [SerializeField] private float _maxPower;
    [SerializeField] private float _rotateAngle;
    
    [Range(1,5)] [SerializeField] private float _fallingThreshold;
    [Range(1,5)] [SerializeField] private float _gravityModifier;
    [Range(1, 50)] [SerializeField] private float _maxFallingVelocity;


    private Rigidbody _rb;
    private CheckGrounded _checkGrounded;

    public enum ButtonState { Off, Down, Held, Up}
    public ButtonState mouseButtonState = ButtonState.Off;

    public bool falling;
    public bool tuckedIn;



    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _checkGrounded = GetComponent<CheckGrounded>();
    }

    void Update()
    {

        // If grounded then we can power up our jump ; else jump = false
        if (_checkGrounded.Grounded)
        {
            JumpPowerUP();
            _canJump = true;
        }
        else if (!_checkGrounded.Grounded || falling) {
            _canJump = false;
            _power = 0f;  
        }

        // Checking if player is falling
        if (Mathf.Abs(_rb.velocity.y) > _fallingThreshold && !_checkGrounded.Grounded)
        {
            falling = true;
        }
        else
        {
            falling = false;
        }

        Debug.Log(Mathf.Abs(_rb.velocity.y));
    }

    private void FixedUpdate()
    {
        // If grounded and jump = true i.e button release then add force to player
        if ( mouseButtonState == ButtonState.Up && _canJump)
        {
            Jump();
        }

        if (!_checkGrounded.Grounded && falling && Input.GetMouseButton(0))
        {
            _rb.freezeRotation = false;
            TuckAndFlip();
            tuckedIn = true;
        }
        else {
            tuckedIn = false;
        }


        if (falling && tuckedIn)
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * (_gravityModifier - 1) * Time.fixedDeltaTime ;
            if (Mathf.Abs(_rb.velocity.y) >= _maxFallingVelocity) {
                _rb.velocity = new Vector3(0f, -_maxFallingVelocity, 0f);
            }
        }
        else if(falling && !tuckedIn) {
            _rb.velocity = Physics.gravity;
        }

        //Debug.Log(Physics.gravity);

    }


    // For powering up the jump
    // When jump power reaches max it will start from 0 again
    private void JumpPowerUP() {
        if (Input.GetMouseButton(0))
        {
            _power += Time.deltaTime;

            if (_power >= _maxPower)
            {
                _power = 0f;
            }

            mouseButtonState = ButtonState.Held;
        }

        if (Input.GetMouseButtonUp(0)) {
            mouseButtonState = ButtonState.Up;
        }
    }

    private void Jump() {
      
        _rb.AddForce(new Vector3(0f, 1f, 1f)* _jumpForce * _power, ForceMode.Force); 
    }


    private void TuckAndFlip() {
        Vector3 position = gameObject.GetComponentInChildren<Collider>().bounds.center;
        transform.RotateAround(position, Vector3.left, -_rotateAngle);
       
    }
}
