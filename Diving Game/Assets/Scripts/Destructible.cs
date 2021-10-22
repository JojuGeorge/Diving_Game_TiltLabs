using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{

    [SerializeField] private GameObject _damagedVersion;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;

        if (player.tag == "Player") {
            if (player.GetComponent<Player>().tuckedIn)
            {
                Instantiate(_damagedVersion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
