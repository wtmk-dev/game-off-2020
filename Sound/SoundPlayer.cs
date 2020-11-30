using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip _StartGame, _MainTheme, _JumpScare, _ScareText;

    [SerializeField]
    private AudioSource OneShot, MainSource, JS;

    public void StartGame()
    {
        OneShot.clip = _StartGame;
        OneShot.loop = false;

        OneShot.Play();
    }

    public void MainTheme()
    {
        MainSource.loop = false;
        MainSource.DOFade(0, 1f);

        OneShot.clip = _MainTheme;
        OneShot.loop = true;

        OneShot.Play();
    }

    public void JumpScare()
    {
        JS.clip = _JumpScare;
        JS.Play();
    }

    public void ScareText()
    {
        JS.clip = _ScareText;
        //JS.loop = true;
        JS.Play();
    }

    public void EndPart()
    {
        JS.loop = true;
        JS.clip = _ScareText;
        JS.Play();
    }

    public void End()
    {
        OneShot.loop = false;
        OneShot.DOFade(0, 1f);
    }
}
