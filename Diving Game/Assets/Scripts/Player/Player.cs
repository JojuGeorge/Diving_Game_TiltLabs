using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _jump;
    [SerializeField] private float _power;
    [SerializeField] private float _maxPower;



    private Rigidbody _rb;
    private CheckGrounded _checkGrounded;

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
        }
        else {
            _jump = false;
            _power = 0f;
        }
    }

    private void FixedUpdate()
    {
        // If grounded and jump = true i.e button release then add force to player
        if (_checkGrounded.Grounded && _jump)
        {
            Jump();
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
        }

        // After powering up and releasing the button, set jump = true to add force to player
        if (Input.GetMouseButtonUp(0))
        {
            _jump = true;
        }
    }

    private void Jump() {
      
        _rb.AddForce(new Vector3(1f, 1f, 0f)* _jumpForce * _power, ForceMode.Force); 
    }

    
}
