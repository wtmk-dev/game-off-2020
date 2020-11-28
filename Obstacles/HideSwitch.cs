using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject ObjectToHide;
    [SerializeField]
    private bool _PlayerRequired, _BallRequired, _IsActive;
    private bool _PlayerOn, _BallOn;

    private void OnCollisionStay(Collision collision)
    { 
        if(_PlayerRequired)
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if(player != null)
            {
                _PlayerOn = true;
            }
        }

        if (_BallRequired)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();

            if (ball != null)
            {
                _BallOn = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball == null)
        {
            _BallOn = false;
        }

        Player player = collision.gameObject.GetComponent<Player>();

        if (player == null)
        {
            _PlayerOn = false;
        }
    }

    private void Update()
    {
        if (!_PlayerRequired && !_BallRequired)
        {
            ObjectToHide.SetActive(_IsActive);
        }
        else if (_PlayerRequired && _BallRequired && _PlayerOn && _BallOn)
        {
            ObjectToHide.SetActive(_IsActive);
        }
        else if (_PlayerRequired && !_BallRequired && _PlayerOn)
        {
            ObjectToHide.SetActive(_IsActive);
        }
        else if (!_PlayerRequired && _BallRequired && _BallOn)
        {
            ObjectToHide.SetActive(_IsActive);
        }
    }

}
