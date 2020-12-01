using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public bool PlayerScored;
    public bool BallScored;
    [SerializeField]
    private EndScreen _EndScreen;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogWarning(collision);
    }

    public void Update()
    {
        if(PlayerScored && BallScored)
        {
            _EndScreen.SetActive(true);
        }
    }


}
