using Cinemachine;
using System.Collections;
using UnityEngine;

public class FocusCamLoop : MonoBehaviour
{
    public FocusCamManager focus;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador"))
        { 
            focus.haciendoFocus = true;
        }
    }
}
