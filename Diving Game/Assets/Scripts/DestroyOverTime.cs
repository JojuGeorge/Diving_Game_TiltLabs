using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{

    [SerializeField] private float _destroyTimeDelay;

    private void Start()
    {
        Destroy(gameObject, _destroyTimeDelay);
    }
}
