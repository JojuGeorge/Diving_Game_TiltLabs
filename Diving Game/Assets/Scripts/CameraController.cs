using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smooting;

    [SerializeField] private float lowY;

    void Start()
    {
        _offset = transform.position - _target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, _smooting);


        if (transform.position.y < lowY) {
            transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
        }
    }
}
