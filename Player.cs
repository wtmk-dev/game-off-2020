using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public bool IsGrounded;
    public bool HasBall;
    public bool Inverted;
    public float JumpStrength;

    public void ExecuteAction(string actionName)
    {
        // if the mouse button was pressed or released
        //Debug.Log(actionName);

        if(actionName =="4")
        {
            SpaceDown();
        }
        else if (actionName == "5")
        {
            SpaceUp();
        }
    }

    [SerializeField]
    private Rigidbody _Rigidbody;
    [SerializeField]
    private Ball _Ball;
    [SerializeField]
    private Transform _BallAnchor;
    private GameData _GameData;
    [SerializeField]
    private Image _JumpPowerBar;

    private bool _IsGrounded;
    private bool _HasBall;

    private PlayerState CurrentState;

    private void Awake()
    {
        _GameData = new GameData();
        _GameData.Strength = 15;
        _GameData.Agility = 75;

        CurrentState = PlayerState.Idel;
    }

    private float _Horizontal;
    private float _Vertical;
    private float _BallRotation = 0f;
    private Vector3 _MousePosition;

    private float _DistanceFromGround = 0.1f;
    private int _GroundMask = 1 << 8;
    private RaycastHit _GroundCheckRay;

    private void Update()
    {
        _Horizontal = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if(CurrentState == PlayerState.Idel)
        {
            if (_Horizontal < 0)
            {
                _Rigidbody.AddForce(Vector3.left, ForceMode.Impulse);
            }
            else if (_Horizontal > 0)
            {
                _Rigidbody.AddForce(-Vector3.left, ForceMode.Impulse);
            }

            if (_Ball.IsGrounded)
            {
                _Rigidbody.useGravity = true;
            }
        }

        if(CurrentState == PlayerState.Aiming)
        {
            
            if (_Horizontal < 0)
            {
                _BallRotation += 0.9f;

                if (_BallRotation > 90)
                {
                    _BallRotation = 90f;
                }

                Vector3 rotateLeft = new Vector3(0f, 0f, _BallRotation);
                _Ball.transform.DORotate(rotateLeft, 0.1f);
            }
            else if (_Horizontal > 0)
            {
                _BallRotation -= 0.9f;

                if (_BallRotation < -90f)
                {
                    _BallRotation = -90f;
                }

                Vector3 rotateLeft = new Vector3(0f, 0f, _BallRotation);
                _Ball.transform.DORotate(rotateLeft, 0.1f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string trigger = other.gameObject.name;
        GameObject obj = other.gameObject;
        //Debug.Log($"Enter {trigger}");

        switch (trigger)
        {
            case "FloorTrigger":
                IsGrounded = true;
                break;
            case "Ball":
                Catachable ball = obj.GetComponent<Catachable>();

                if(ball == null)
                {
                    return;
                }
                ball.Catch();
                HasBall = true;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string trigger = other.gameObject.name;
        GameObject obj = other.gameObject;
        //Debug.Log($"Exit {trigger}");

        switch (trigger)
        {
            case "FloorTrigger":
                IsGrounded = false;
                break;
            case "Ball":
                HasBall = false;
                break;
        }
    }

    private void SpaceDown()
    {
        Debug.Log(CurrentState);

        if(HasBall && CurrentState == PlayerState.Idel)
        {
            CurrentState = PlayerState.Aiming;

/* AIM Jump
            _Rigidbody.velocity = Vector3.zero;
            _Rigidbody.useGravity = false;
            _Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
*/

            _BallRotation = 0f;
            _Ball.transform.position = _BallAnchor.transform.position;
            _Ball.PickUp();

           if (_SlowRoutine != null)
            {
                StopCoroutine(_SlowRoutine);
                _SlowRoutine = null;
            }

            _SlowRoutine = SlowTime(.6f, 1f);
            StartCoroutine(_SlowRoutine);
        }else if(CurrentState == PlayerState.Idel)
        {
            CurrentState = PlayerState.Jumping;

            if(_AimJump != null)
            {
                StopCoroutine(_AimJump);
                _AimJump = null;
            }

            _AimJump = AimJump();
            StartCoroutine(_AimJump);
        }
    }

    private void SpaceUp()
    {
        Debug.Log(CurrentState);

        if (CurrentState == PlayerState.Aiming)
        {
            CurrentState = PlayerState.Idel;
            _Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            _Rigidbody.useGravity = true;

            if (_SlowRoutine != null)
            {
                StopCoroutine(_SlowRoutine);
                _SlowRoutine = null;
                Time.timeScale = 1f;
            }

            ThrowBall();
        }

        if(CurrentState == PlayerState.Jumping)
        {
            CurrentState = PlayerState.Idel;
            _JumpPowerBar.fillAmount = 0;

            if (_AimJump != null)
            {
                StopCoroutine(_AimJump);
                _AimJump = null;
            }

            //Debug.LogError(JumpStrength); // will be power...

            if(JumpStrength > .7 && JumpStrength < 1)
            {
                JumpStrength = 10f;
            }else if (JumpStrength >= 1)
            {
                JumpStrength = 0f;
            }
            else if (JumpStrength <= .1)
            {
                JumpStrength = 5f;
            }
            else if (JumpStrength <= .2)
            {
                JumpStrength = 7f;
            }
            else if (JumpStrength <= .3)
            {
                JumpStrength = 9f;
            }
            else if (JumpStrength <= .4)
            {
                JumpStrength = 11f;
            }
            else if (JumpStrength <= .5)
            {
                JumpStrength = 12f;
            }
            else if (JumpStrength <= .6)
            {
                JumpStrength = 13f;
            }
            else if (JumpStrength <= .7)
            {
                JumpStrength = 18f;
            }

            _Rigidbody.velocity = Vector3.zero;
            _Rigidbody.useGravity = true;
            _Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            _Rigidbody.AddForce(Vector3.up * (JumpStrength * 6), ForceMode.Impulse);
        }
    }

    private void ThrowBall()
    {
        //Debug.Log("ball thrown");

        if (_SlowRoutine != null)
        {
            StopCoroutine(_SlowRoutine);
            _SlowRoutine = null;
        }

        _Ball.Throw(_GameData.Strength);

        _HasBall = false;
    }

    private ParticleSystem _Select;

    private float _StartPosition;
    private float _EndPosition;
    private float _ElapsedTime;
    private IEnumerator _AimJump;

    private IEnumerator AimJump()
    {
        //Debug.Log("AimJump");
        _JumpPowerBar.fillAmount = 0f;
        _Rigidbody.useGravity = false;
        _Rigidbody.velocity = Vector3.zero;
        _Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        //_StartPosition = Input.mousePosition.y

        _ElapsedTime = Time.timeSinceLevelLoad;

        JumpStrength = 0f;

        while (JumpStrength < 1f)
        {
            JumpStrength = Mathf.Abs(_ElapsedTime - Time.timeSinceLevelLoad);
            //Debug.Log(JumpStrength);
            if (_JumpPowerBar.fillAmount != JumpStrength)
            {
                _JumpPowerBar.fillAmount = JumpStrength;
            }

            //Debug.LogError(JumpStrength);

            yield return null;
        }

        _Rigidbody.useGravity = true;
    }

    private IEnumerator _TossRoutine;
    private IEnumerator _CalculateJumpAngel;
    private IEnumerator _JumpRoutine;
    private IEnumerator _SlowRoutine;
   
    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private IEnumerator SlowTime(float slowDown, float waitTime)
    {
        //Debug.Log("time is slowing");
        float currentTimeScale = Time.timeScale;
        Time.timeScale = slowDown;
        yield return new WaitForSeconds(waitTime);
        //Debug.Log("time is back to normal");
        Time.timeScale = currentTimeScale;
    }
}
