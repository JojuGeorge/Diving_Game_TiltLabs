using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _canJump;
    [SerializeField] private float _power;
    [Range(1,2)] [SerializeField] private float _minPower;
    [Range(4,6)] [SerializeField] private float _maxPower;
    [SerializeField] private float _rotateAngle;
    
    [Range(1,5)] [SerializeField] private float _fallingThreshold;
    [Range(8, 12)] [SerializeField] private float _gravityModifierNormal;       // Ideally put normal falling velocity less than the tuckedIn
    [Range(12, 20)] [SerializeField] private float _gravityModifierTuckedIn;

    //[SerializeField] private float _playerColliderHeightTuckedIn;


    private Rigidbody _rb;
    private CheckGrounded _checkGrounded;
    private BoxCollider _boxCollider;
    private SphereCollider _sphereCollider;

    public enum ButtonState { Off, Down, Held, Up}                  // For getting input from Update() and move obj based on input in FixedUpdate();
    public ButtonState mouseButtonState = ButtonState.Off;

    public bool falling;
    public bool tuckedIn;
    public bool inWater;
    public int coins;
    
    public float maxMouseHoldDownDelay;
    private float _mouseHoldDownDelay;


    private void OnEnable()
    {
        CoinMultiplierEvent.OnDiving += DiveAngleCoinMultiplier;
    }


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _checkGrounded = GetComponent<CheckGrounded>();
        _boxCollider = GetComponent<BoxCollider>();
        _sphereCollider = GetComponent<SphereCollider>();
        _power = _minPower;
    }

    void Update()
    {

        // If grounded then we can power up our jump ; else jump = false
        if (_checkGrounded.Grounded)
        {
            JumpPowerUP();
            _canJump = true;
            inWater = false;
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

        if (Input.GetMouseButton(0) && !_checkGrounded.Grounded && falling && !inWater)
        {
            //_rb.freezeRotation = false;
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

            // When player is in squat pos, enable sphere to resemble with model and disable the box collider
            _boxCollider.enabled = false;
            _sphereCollider.enabled = true;

        }
        else if (falling && !tuckedIn)
        {

            _rb.velocity = Vector3.Lerp(_rb.velocity, new Vector3(0f, -_gravityModifierNormal, 0f), .2f);
            _boxCollider.enabled = true;
            _sphereCollider.enabled = false;

        }
        else if (_checkGrounded.Grounded || !falling) {
            _boxCollider.enabled = true;
            _sphereCollider.enabled = false;
        }


    }


    // For powering up the jump while grounded
    // When jump power reaches max it will start from 0 again
    private void JumpPowerUP() {
        if (Input.GetMouseButton(0))
        {
            _mouseHoldDownDelay += Time.deltaTime;

            if(_mouseHoldDownDelay >= maxMouseHoldDownDelay)
                _power += Time.deltaTime;

            if (_power >= _maxPower)
            {
                _power = _minPower;
            }

            mouseButtonState = ButtonState.Held;
        }

        // To get mouse button state to make chnages on physics obj in the FixedUpdate()
        if (Input.GetMouseButtonUp(0)) {
            if (_mouseHoldDownDelay >= maxMouseHoldDownDelay)
            {
                mouseButtonState = ButtonState.Up;
                _mouseHoldDownDelay = 0f;
            }
            else {
                _mouseHoldDownDelay = 0f;
            }
        }
    }


    // Adds force to player for Jump
    private void Jump() {
      
        _rb.AddForce(new Vector3(0f, 1f, 1f)* _jumpForce * _power, ForceMode.Force); 
    }


    // On tucking in slowly rotate the body
    private void TuckAndFlip() {
        
        Vector3 position = gameObject.GetComponentInChildren<Collider>().bounds.center;
        transform.RotateAround(position, Vector3.left, -_rotateAngle);
        
    }


    public void AddCoins(int amount) {
        coins += amount;
    }

    // Coin multiplier based on diving angle - calc done in CoinMultiplierEvent.cs
    private void DiveAngleCoinMultiplier(string score, int coinMul) {
        Debug.Log("(in player.cs)coin multiplier = " + score + " coin multiplied by = " + coinMul);
        coins *= coinMul;
    }


}
