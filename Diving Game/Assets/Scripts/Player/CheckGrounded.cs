using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGrounded : MonoBehaviour
{
    [SerializeField] private Transform _groundCheckTransform;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private LayerMask _groundLayerMask;

    private bool _grounded;
    public bool Grounded { get { return _grounded; } }

    void Start()
    {
        
    }

    void Update()
    {
        CheckIfGrounded();
    }

    private void CheckIfGrounded() {
        _grounded = Physics.Raycast(_groundCheckTransform.position, Vector3.down, _groundCheckDistance, _groundLayerMask);
        Debug.DrawRay(_groundCheckTransform.position, Vector3.down * _groundCheckDistance, Color.green);
    }
}
