using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private float _Time = 0.5f, _Distance = 0.1f;
    [SerializeField]
    private bool _IsHorizontal, _ActiveOnAwake = true;

    private Transform _Transform;

    // Start is called before the first frame update
    void Start()
    {
        _Transform = transform;

        if(!_ActiveOnAwake)
        {
            return;
        }

        if(_IsHorizontal)
        {
            PingPong(_Transform.localPosition.x, _Transform.localPosition.x + _Distance);
        }
        else
        {
            PingPong(_Transform.localPosition.y, _Transform.localPosition.y + _Distance);
        }
        
    }
    
    private void PingPong(float from, float to)
    {
        if(_IsHorizontal)
        {
            transform.DOLocalMoveX(to, _Time).OnComplete(() => PingPong(to, from));
        } else {
            transform.DOLocalMoveY(to, _Time).OnComplete(() => PingPong(to, from));
        }
        
    }

    public void Move(float time, float to, bool horizontal)
    {
        if(horizontal)
        {
            transform.DOLocalMoveX(to, time);
        } else
        {
            transform.DOLocalMoveY(to, time);
        }
    }

}
