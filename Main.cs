using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Main : MonoBehaviour
{
    [SerializeField]
    private Player _Player;

    private void Update()
    {
        KeyboardInput();
    }

    private void KeyboardInput()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Debug.Log($"Mouse 0 down. Player {_Player.State}");
            _Player.ExecuteAction("0");

        }else if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            //Debug.Log($"Mouse 1 down . Player {_Player.State}");
            _Player.ExecuteAction("1");
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //Debug.Log($"Mouse 0 down. Player {_Player.State}");
            _Player.ExecuteAction("2");

        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            //Debug.Log($"Mouse 1 down . Player {_Player.State}");
            _Player.ExecuteAction("3");
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log($"space down . Player {_Player.State}");
            _Player.ExecuteAction("4");
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            //Debug.Log($"space up . Player {_Player.State}");
            _Player.ExecuteAction("5");
        }
    }
}
