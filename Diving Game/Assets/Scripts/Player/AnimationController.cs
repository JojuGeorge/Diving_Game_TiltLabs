using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    private Player _player;
    private CheckGrounded _checkGrounded;
    private Animator _anim;

    void Start()
    {
        _player = GetComponent<Player>();
        _checkGrounded = GetComponent<CheckGrounded>();
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        IdleAnim();
        StrechedAnim();
        TuckedInAnim();
    }

    private void IdleAnim() {
        _anim.SetBool("Grounded", _checkGrounded.Grounded);
    }

    private void StrechedAnim() {
        _anim.SetBool("Falling", _player.falling);
    }

    private void TuckedInAnim() {
        _anim.SetBool("TuckedIn", _player.tuckedIn);
    }
}
