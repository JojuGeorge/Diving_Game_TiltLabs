using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearWater : MonoBehaviour
{

    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isTriggered) {

            if(other.GetComponent<Player>().tuckedIn)
                Debug.Log("RELEASE TOUCHING BEFORE GOING TO IN WATER TO GET PERFECT SCORE");

           // other.GetComponent<SphereCollider>().enabled = false;
            isTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;
    }
}
