using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMechanics : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            Debug.Log("(In WaterMechanics.cs )Player dived into the water");
            Rigidbody playerRB = other.gameObject.GetComponent<Rigidbody>();

         //   playerRB.mass = .3f;
        }
    }
}
