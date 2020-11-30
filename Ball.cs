using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;

public class Ball : MonoBehaviour , Catachable , Throwable
{
    public Rigidbody Rigidbody;
    public bool IsGrounded;

    public void Catch()
    {
        //Debug.Log("Caught");
    }

    public void Throw()
    {
        //Debug.Log("Throw");
    }

    public void Throw(float strength)
    {
        Rigidbody.isKinematic = false;
        Rigidbody.useGravity = true;
        _Guide.gameObject.SetActive(false);
        Rigidbody.AddForce(transform.TransformDirection(Vector3.up) * strength, ForceMode.Impulse);
    }

    public void PickUp()
    {
        Rigidbody.useGravity = false;
        Rigidbody.isKinematic = true;
        _Guide.gameObject.SetActive(true);
    }

    public void SetSpeach(string text)
    {
        _Speach.ShowText(text);
    }

    private float _GroundCheckThreshold = 6f;
    private float _DistanceFromGround;

    [SerializeField]
    private Transform  _GroundPosition, _Guide;

    private Vector3 _Forward;
    private Vector3 _CurrentPosition;

    [SerializeField]
    public TextMeshProUGUI _Distance;
    [SerializeField]
    private TextAnimatorPlayer _Speach;

    private void Awake()
    {
        _Guide.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        //Debug.Log(IsGrounded);
        //Debug.Log(IsCatachable);

        _Forward = transform.TransformDirection(Vector3.up) * 1000;
        Debug.DrawRay(transform.position, _Forward, Color.green);
       
        _DistanceFromGround = Vector3.Distance(_GroundPosition.position, _CurrentPosition);
        //Debug.Log(DistanceFromGround);

        if (_Distance.gameObject.activeSelf)
        {
            if(_CurrentPosition != transform.position)
            {
                _CurrentPosition = transform.position;
                _DistanceFromGround = Vector3.Distance(_GroundPosition.position, _CurrentPosition);
                _Distance.SetText(_DistanceFromGround + ": mi");
            }
        }
    }
}
