using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Febucci.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField]
    private TextAnimatorPlayer _Title, _Tutorial;

    [SerializeField]
    private Button _Start;

    private AnimationRelay _Animator;
    private Player _Player;
    private Ball _Ball;

    private IEnumerator _TutoralRoutine;

    void Awake()
    {
        _Animator = GameObject.FindObjectOfType<AnimationRelay>();
        _Player = GameObject.FindObjectOfType<Player>();
        _Ball = GameObject.FindObjectOfType<Ball>();

        _Start.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        /*
        _Title.ShowText("<fade>Brandon Khan & Noah Farber\nFeaturing Unity Chan!\n-WTMK-\n Prestige Gaming</fade>");
        */

        _Title.ShowText("<fade>Staring... Unity Chan!</fade>");

        if(_TutoralRoutine != null)
        {
            StopCoroutine(_TutoralRoutine);
            _TutoralRoutine = null;
        }

        _TutoralRoutine = TutorialRoutine();
        StartCoroutine(_TutoralRoutine);
    }

    private IEnumerator TutorialRoutine()
    {
        _Start.gameObject.SetActive(false);

        _Player.SetSpeach("<fade>ok ok... NO need to <wiggle>YELL!</wiggle></fade>");
        yield return new WaitForSeconds(3f);

        _Ball.SetSpeach("<fade>Let's get to the moon already...</fade>");
        yield return new WaitForSeconds(3f);
        //_Player.SetSpeach("<fade><wiggle>BALL...</wiggle>We keep failing why the moon again...</fade>");

        _Animator.SetBool("Active", true);
        _Player.IsActive = true;

        _Tutorial.ShowText("use A and D to move");
        bool canMove = false;

        while(!canMove)
        {
            if(Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
            {
                canMove = true;
            }
            yield return null;
        }

        //_Ball.SetSpeach("<fade>If you get me to the moon ill grant you one wish!</fade>");

        _Tutorial.ShowText("hold SPACEBAR to start a jump");
        bool canJump = false;

        while (!canJump)
        {
           if(Input.GetKey(KeyCode.Space))
            {
                canJump = true;
            }

            yield return null;
        }

        yield return new WaitForSeconds(3f);

        _Ball.Rigidbody.useGravity = true;

        _Tutorial.ShowText("hold SPACEBAR while near BALL to start a throw");

        bool canTrhow = false;

        while(!canTrhow)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                canTrhow = true;
            }

            yield return null;
        }

        _Tutorial.ShowText("use A and D to aim your shot");
        yield return new WaitForSeconds(7f);

        _Tutorial.ShowText("the moon is 2380 mi away...");
        _Ball._Distance.gameObject.SetActive(true);
        yield return new WaitForSeconds(7f);

        _Tutorial.ShowText("<fade> in times like this i find its best to say ~\\*.*/~ GOOD LUCK!</fade>");
    }

}
