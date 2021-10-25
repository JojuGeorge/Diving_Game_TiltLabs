using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private int _coinAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            other.GetComponent<Player>().AddCoins(_coinAmount);
            Destroy(gameObject);
        }
    }
}
