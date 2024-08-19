using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class TriggerNoFocusCamara : MonoBehaviour
{
    public FocusCamManager focus;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            focus.haciendoFocus = false;
        }
    }
}
