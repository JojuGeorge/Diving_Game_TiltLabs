using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatObjectScript : MonoBehaviour
{

    [SerializeField] private float _waterLevel = 0.0f;
    [SerializeField] private float _floatThreshold = 2f;
    [SerializeField] private float _waterDensity = 0.125f;
    [SerializeField] private float _downForce = 4f;

    [SerializeField] private float _forceFactor;
    [SerializeField] private Vector3 _floatForce;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        _forceFactor = 1f - ((transform.position.y - _waterLevel) / _floatThreshold);

        if (_forceFactor > 0f) {
            _floatForce = -Physics.gravity * (_forceFactor - GetComponent<Rigidbody>().velocity.y * _waterDensity);
            _floatForce += new Vector3(0f, -_downForce, 0f);
            GetComponent<Rigidbody>().AddForceAtPosition(_floatForce, transform.position);
        }
    }
}
