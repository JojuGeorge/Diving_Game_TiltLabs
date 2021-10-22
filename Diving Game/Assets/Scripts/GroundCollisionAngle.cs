using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollisionAngle : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 x = Vector3.up;
        Vector3 y = transform.forward;
        Debug.Log(" dot prod = " + Vector3.Dot(x, y));
    }


}
