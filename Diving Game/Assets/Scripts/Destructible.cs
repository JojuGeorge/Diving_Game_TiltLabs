using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{

    [SerializeField] private GameObject _damagedVersion;
    [SerializeField] private int _coinAmount;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        // GameObject player = collision.gameObject;
        Player player;

        if (other.gameObject.tag == "Player") {
            player = other.gameObject.GetComponent<Player>();
            if (player.tuckedIn)
            {
               // gameObject.GetComponent<Rigidbody>().isKinematic = false;
                Instantiate(_damagedVersion, transform.position, transform.rotation);
                player.AddCoins(_coinAmount);
                Destroy(gameObject);
            }
        }
    }
}
