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
        DivePose();
    }

    private void IdleAnim() {
        _anim.SetBool("Grounded", _checkGrounded.Grounded);
    }

    private void StrechedAnim() {
        _anim.SetBool("Falling", _player.falling);
        _anim.SetBool("InWater", _player.inWater);
    }

    private void TuckedInAnim() {
        _anim.SetBool("TuckedIn", _player.tuckedIn);
    }

    private void DivePose() {
        _anim.SetBool("DivePose", _player.divePose);
    }
}
