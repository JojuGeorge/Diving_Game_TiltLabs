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
    [Range(8, 12)] [SerializeField] private float _gravityModifierNormal;       // Ideally put normal falling velocity less than the tuckedIn
    [Range(12, 20)] [SerializeField] private float _gravityModifierTuckedIn;

    [SerializeField] private float _playerColliderHeightTuckedIn;


    private Rigidbody _rb;
    private CheckGrounded _checkGrounded;
    private CapsuleCollider _capsuleCollider;
    private float _playerColliderHeight = 20.8f;

    public enum ButtonState { Off, Down, Held, Up}                  // For getting input from Update() and move obj based on input in FixedUpdate();
    public ButtonState mouseButtonState = ButtonState.Off;

    public bool falling;
    public bool tuckedIn;



    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _checkGrounded = GetComponent<CheckGrounded>();
        _capsuleCollider = GetComponentInChildren<CapsuleCollider>();
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


        // When tuckedin increase fall velocity
        if (falling && tuckedIn)
        {
            _rb.velocity = Vector3.Lerp(_rb.velocity, new Vector3(0f, -_gravityModifierTuckedIn, 0f), .5f);
            _capsuleCollider.height = _playerColliderHeightTuckedIn;

        }
        else if (falling && !tuckedIn)
        {

            _rb.velocity = Vector3.Lerp(_rb.velocity, new Vector3(0f, -_gravityModifierNormal, 0f), .2f);
            _capsuleCollider.height = _playerColliderHeight;

        }
        else if (_checkGrounded.Grounded || !falling) {
            _capsuleCollider.height = _playerColliderHeight;
        }


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

        // To get mouse button state to make chnages on physics obj in the FixedUpdate()
        if (Input.GetMouseButtonUp(0)) {
            mouseButtonState = ButtonState.Up;
        }
    }

    private void Jump() {
      
        _rb.AddForce(new Vector3(0f, 1f, 1f)* _jumpForce * _power, ForceMode.Force); 
    }


    // On tucking in slowly rotate the body
    private void TuckAndFlip() {
        Vector3 position = gameObject.GetComponentInChildren<Collider>().bounds.center;
        transform.RotateAround(position, Vector3.left, -_rotateAngle);
       
    }
}
