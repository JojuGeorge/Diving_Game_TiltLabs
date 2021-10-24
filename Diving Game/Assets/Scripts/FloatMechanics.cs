using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatMechanics : MonoBehaviour
{

    private Player _player;
    private Rigidbody _rb;

    void Start()
    {
        _player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        if (_player.inWater)
        {

            Vector3 targetPos = new Vector3(0f, -35f, 0f);

            transform.position = Vector3.Lerp(transform.position, targetPos, .01f);

            if (transform.position.y >= targetPos.y)
            {
                _rb.velocity = Vector3.zero;
                _rb.useGravity = false;
            }

            if (transform.rotation != Quaternion.Euler(0f, 0f, 0f))
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), .02f);
        }
        else {
            _rb.useGravity = true;
        }
    }
}
