using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNoFocusCamara : MonoBehaviour
{
    public CamaraController camaraController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            camaraController.camaraFocus = false;
        }
    }
}
