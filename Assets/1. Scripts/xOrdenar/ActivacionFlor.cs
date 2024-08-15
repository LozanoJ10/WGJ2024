using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivacionFlor : MonoBehaviour
{
    public AccionSilvido accionSilvido;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        { 
            accionSilvido.sePuedeActivarVoz = true;        
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Jugador"))
        {
            accionSilvido.sePuedeActivarVoz = false;
        }
    }
}
