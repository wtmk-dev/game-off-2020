using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRelay : MonoBehaviour
{
    private Animator _Animator;

    private void Awake()
    {
        _Animator = GetComponent<Animator>();
    }

    public void SetFloat(string name, float value)
    {
        _Animator.SetFloat(name, value);
    }

    public void SetBool(string name, bool state)
    {
        _Animator.SetBool(name, state);
    }
}
