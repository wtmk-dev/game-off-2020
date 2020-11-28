using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public event Action OnFloorContact;
    public event Action OnFloorExit;

    private void OnTriggerEnter(Collider other)
    {
        FloorTrigger floor = other.gameObject.GetComponent<FloorTrigger>();

        if (floor == null)
        {
            return;
        }

        OnFloorContact?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        FloorTrigger floor = other.gameObject.GetComponent<FloorTrigger>();

        if (floor == null)
        {
            return;
        }

        OnFloorExit?.Invoke();
    }
}
