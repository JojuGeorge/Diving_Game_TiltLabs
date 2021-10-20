using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Transform _checkPoint;
    private Player _player;
    
    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            ResetPlayerPos();       
    }


    private void ResetPlayerPos() {
        _player.transform.position = _checkPoint.transform.position;
        _player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

}
